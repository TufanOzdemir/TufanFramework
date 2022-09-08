namespace TufanFramework.Common.Configuration
{
    public class AuthenticationConfig : BaseConfig
    {
        public string Secret { get; set; }
        public int ExpireMinutes { get; set; }
        public string ValidIssuer { get; set; }
        public override string ConfigSection { get => "Authentication"; }
    }
}