namespace TufanFramework.Common
{
    public class ConstVariables
    {
        public struct JWT
        {
            public const string ApplicationLevelClaimType = "ApplicationLevel";
            public const string UserLevelClaimType = "UserLevel";
            public const string SecurityLevelClaimType = "SecurityLevel";
            public const string AuthenticationIssuer = "AuthenticationService";
        }

        public enum SecurityLevel
        {
            Application = 0,
            UserAndApplication = 1
        }
    }
}