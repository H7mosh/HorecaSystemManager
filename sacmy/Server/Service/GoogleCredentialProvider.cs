﻿using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;

namespace sacmy.Server.Service
{
    public class GoogleCredentialProvider
    {
        private readonly IConfiguration _configuration;

        public GoogleCredentialProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetAccessTokenAsync(string configKey)
        {
            var firebaseConfig = _configuration.GetSection(configKey);
            var credentialJson = JsonConvert.SerializeObject(new
            {
                type = firebaseConfig["type"],
                project_id = firebaseConfig["project_id"],
                private_key_id = firebaseConfig["private_key_id"],
                private_key = firebaseConfig["private_key"].Replace("\\n", "\n"),
                client_email = firebaseConfig["client_email"],
                client_id = firebaseConfig["client_id"],
                auth_uri = firebaseConfig["auth_uri"],
                token_uri = firebaseConfig["token_uri"],
                auth_provider_x509_cert_url = firebaseConfig["auth_provider_x509_cert_url"],
                client_x509_cert_url = firebaseConfig["client_x509_cert_url"]
            });

            var credential = GoogleCredential.FromJson(credentialJson)
                .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

            return await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        }
    }
}
