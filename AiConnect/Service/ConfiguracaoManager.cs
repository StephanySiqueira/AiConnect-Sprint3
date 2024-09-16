using System;

namespace AiConnect.Service
{
    public class ConfiguracaoManager
    {
        private static ConfiguracaoManager _instance;
        private static readonly object _lock = new object();

        // Propriedades de configuração
        public string ConnectionString { get; private set; }
        public int MaxUploadFileSize { get; private set; }

        // Construtor privado para evitar a criação de instâncias fora da classe
        private ConfiguracaoManager()
        {
            // Carregar as configurações aqui
            // Por exemplo, de um arquivo JSON ou de variáveis de ambiente
            // Simulação de carregamento
            ConnectionString = "SuaStringDeConexaoAqui";
            MaxUploadFileSize = 10 * 1024 * 1024; // 10 MB
        }

        // Método público para obter a instância única
        public static ConfiguracaoManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ConfiguracaoManager();
                    }
                    return _instance;
                }
            }
        }

        // Métodos para acessar as configurações
        public string GetConnectionString()
        {
            return ConnectionString;
        }

        public int GetMaxUploadFileSize()
        {
            return MaxUploadFileSize;
        }
    }
}
