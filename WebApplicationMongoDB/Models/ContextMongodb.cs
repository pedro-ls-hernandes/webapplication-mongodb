using MongoDB.Driver;

namespace WebApplicationMongoDB.Models
{
    public class ContextMongodb
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get;}
        public static bool IsSSL { get; set; }
        private IMongoDatabase _database { get; }

        public ContextMongodb()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));

                if(IsSSL)
                {
                    //setar protocolo de segurança
                    settings.SslSettings = new SslSettings()
                    {
                        //keep data safe when transferred over a network
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    };
                }
            }
            catch(Exception)
            {
                throw new Exception("Não foi possível estabelecer uma conexão com o banco de dados");
            }
        }
        public IMongoCollection<Usuario> Usuarios {
            get
            {
                return _database.GetCollection<Usuario>("Usuario");
            }
                
        }
    }
}
