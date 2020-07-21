﻿using JRestaurant.WebApi.Models;
using JWT;
using JWT.Serializers;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace JRestaurant.WebApi.AuthAttributes
{
    public class ApiAuthorizeAttribute: AuthorizeAttribute
    {
        /// <summary>
        /// 指示指定的控件是否已获得授权
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            //前端请求api时会将token存放在名为"auth"的请求头中
            var authHeader = from t in actionContext.Request.Headers where t.Key == "X-Token" select t.Value.FirstOrDefault();
            if (authHeader != null)
            {
                const string secretKey = "JRestaurant";//加密秘钥
                string token = authHeader.FirstOrDefault();//获取token
                if (!string.IsNullOrEmpty(token))
                {
                    try
                    {
                        byte[] key = Encoding.UTF8.GetBytes(secretKey);
                        IJsonSerializer serializer = new JsonNetSerializer();
                        IDateTimeProvider provider = new UtcDateTimeProvider();
                        IJwtValidator validator = new JwtValidator(serializer, provider);
                        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                        IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                        //解密
                        var json = decoder.DecodeToObject<AuthInfo>(token, key, verify: true);
                        if (json != null)
                        {
                            //判断口令过期时间
                            if (json.ExpiryDateTime < DateTime.Now)
                            {
                                return false;
                            }
                            actionContext.RequestContext.RouteData.Values.Add("X-Token", json);
                            return true;
                        }
                        return false;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 处理授权失败的请求
        /// </summary>
        /// <param name="actionContext"></param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var erModel = new
            {
                Success = "false",
                ErrorCode = "401"
            };
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, erModel, "application/json");
        }
    }
}