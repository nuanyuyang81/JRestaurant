namespace JRestaurant.WebApi.Models
{
    public class LoginRequest
    {
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
    }
}