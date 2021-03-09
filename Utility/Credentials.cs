using System;
using Microsoft.Extensions.Configuration;

namespace cs_asp_backend_server.Utility
{
    public sealed class Credentials
    {
        // https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-2.2&tabs=linux#access-a-secret
        private Credentials() { }

        private static readonly Lazy<Credentials> _instance = new Lazy<Credentials>(() => new Credentials());

        public static Credentials Instance { get { return _instance.Value; } }

        public void Initialize(IConfiguration configuration)
        {
            TOKEN_KEY = configuration["CS_ASP_BACKEND_SERVER:TOKEN_KEY"];
            SQL_CONNECTION_STRING = configuration["CS_ASP_BACKEND_SERVER:SQL_CONNECTION_STRING"];

        }
        public string TOKEN_KEY = null;
        public string SQL_CONNECTION_STRING = null;
    }
}