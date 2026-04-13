namespace Bugo_api.Services
{
    public interface IAuthService
    {
        Task<AuthResult> Login(string email, string senha);
        bool IsAuthenticated { get; }
        string CurrentUserEmail { get; }
        void signOut();
    }

    public sealed class authResult
    {
        public bool Success { get; }
        public string? Token { get; }
        public string? ErrorMessage { get; }
        private authResult(bool success, string? token = null, string? errorMessage = null)
        {
            Success = success;
            Token = token;
            ErrorMessage = errorMessage;
        }
        public static authResult Ok(string token) => new authResult(true, token);
        public static authResult Fail(string errorMessage) => new authResult(false, null, errorMessage);
    }
}
