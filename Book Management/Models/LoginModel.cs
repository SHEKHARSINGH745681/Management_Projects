namespace Book_Management.Models
{
    public class LoginModel
    {
        public int Id { get; set; }  // Primary key
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }


}
