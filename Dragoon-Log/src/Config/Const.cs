public class Config
{
    public static string DATABASE_URI
    {
        get
        {
            return "mongodb+srv://recharge:recharge%402022@cluster0.ri2aos7.mongodb.net/?retryWrites=true&w=majority";
        }
    }
    public static string DATABASE   { get { return Environment.GetEnvironmentVariable("DATABASE"); } }
    public static int SOCKET_PORT   { get { return int.Parse(Environment.GetEnvironmentVariable("SOCKET_PORT")); } }
    public static string PASSWORD   { get { return Environment.GetEnvironmentVariable("PASSWORD"); } }
}