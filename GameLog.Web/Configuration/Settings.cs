namespace GameLog.Web.Configuration;

public class Settings
{
    public DatabaseSettings Database { get; set; }
    
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
    }
}