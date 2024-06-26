namespace sacmy.Client.Services
{
    public class ConfigurationService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationRoot _configurationRoot;

        public ConfigurationService(IConfiguration configuration, IConfigurationRoot configurationRoot)
        {
            _configuration = configuration;
            _configurationRoot = configurationRoot;
        }

        public string GetBaseApiUrl() => _configuration["BaseApiUrl"];

        public void SetBaseApiUrl(string newUrl)
        {
            _configurationRoot.Providers.First().Set("BaseApiUrl", newUrl);
            _configurationRoot.Reload();
        }
    }
}
