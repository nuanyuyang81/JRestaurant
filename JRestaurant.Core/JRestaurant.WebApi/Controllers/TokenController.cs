using JRestaurant.DAL;
using JRestaurant.DAL.Admin;
using JRestaurant.WebApi.Models;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Text;
using System.Web.Http;

namespace JRestaurant.WebApi.Controllers
{
    [RoutePrefix("token")]
    public class TokenController : ApiController
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public TokenInfo Login([FromBody] LoginRequest loginRequest)
        {
            TokenInfo tokenInfo = new TokenInfo();
            if (loginRequest != null)
            {
                string phone = loginRequest.PhoneNumber;
                string password = loginRequest.PasswordHash;

                DataSet ds = AdminHandler.LogAttempt(phone, password);
                JObject loginResult = CommonHandler.TransferObjFromDS(ds);

                if (loginResult != null)
                {
                    AuthInfo authInfo = JsonConvert.DeserializeObject<AuthInfo>(loginResult.ToString());
                    authInfo.ExpiryDateTime = DateTime.Now.AddDays(1);
                    const string secretKey = "JRestaurant";//口令加密秘钥
                    try
                    {
                        byte[] key = Encoding.UTF8.GetBytes(secretKey);
                        IJwtAlgorithm algorithm = new HMACSHA256Algorithm();//加密方式
                        IJsonSerializer serializer = new JsonNetSerializer();//序列化Json
                        IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();//base64加解密
                        IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);//JWT编码
                        var token = encoder.Encode(authInfo, key);//生成令牌
                                                                  //口令信息
                        tokenInfo.Success = true;
                        tokenInfo.Token = token;
                        tokenInfo.Message = "OK";
                    }
                    catch (Exception ex)
                    {
                        tokenInfo.Success = false;
                        tokenInfo.Message = ex.Message.ToString();
                    }
                }
                else
                {
                    tokenInfo.Success = false;
                    tokenInfo.Message = "用户密码错误";
                }
            }
            else
            {
                tokenInfo.Success = false;
                tokenInfo.Message = "用户信息为空";
            }
            return tokenInfo;
        }
    }
}
