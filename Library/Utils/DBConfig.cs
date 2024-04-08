using System.Data.SqlClient;
using Library;

class DBConfig
{
    private static string ConnectionString = "Server=localhost\\SQLEXPRESS;Database=BookShop;Trusted_Connection=True;";
    private static DBConfig instance;
    private static SqlConnection connection;
    public static DBConfig GetInstance()
    {
        if (instance == null)
        {
            instance = new DBConfig();
        }
        return instance;
    }
    private DBConfig()
    {
        connection = new SqlConnection(ConnectionString);
        connection.Open();
    }
    public SqlConnection GetConnection()
    {
        return connection;
    }
    public bool ExecuteCommand(SqlCommand cmd)
    {
        try
        {
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }
}
