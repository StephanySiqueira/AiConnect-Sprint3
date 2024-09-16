using Microsoft.Extensions.Configuration;

namespace AiConnect.Services
{
    public class AppConfigurationManager
    {
        private static AppConfigurationManager _instance;
        private static readonly object _lock = new object();

        public string ConnectionString { get; private set; }
        public int MaxUploadFileSize { get; private set; }

        private AppConfigurationManager(IConfiguration configuration)
        {
            // Inicializa as configurações a partir do arquivo de configuração
            ConnectionString = configuration.GetConnectionString("OracleConnection");
            MaxUploadFileSize = int.Parse(configuration["MaxUploadFileSize"] ?? "10485760"); 
        }

        public static AppConfigurationManager Instance { get; private set; }

        public static void Initialize(IConfiguration configuration)
        {
            if (Instance == null)
            {
                lock (_lock)
                {
                    if (Instance == null)
                    {
                        Instance = new AppConfigurationManager(configuration);
                    }
                }
            }
        }
    }
}
