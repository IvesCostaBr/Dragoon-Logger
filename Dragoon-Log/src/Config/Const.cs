public class Config
{
    public static string DATABASE_URI   { get { return Environment.GetEnvironmentVariable("DATABASE_URI"); } }
    public static string DATABASE   { get { return Environment.GetEnvironmentVariable("DATABASE"); } }
    public static string COLLECTION   { get { return Environment.GetEnvironmentVariable("COLLECTION"); } }
    public static int SOCKET_PORT   { get { return int.Parse(Environment.GetEnvironmentVariable("SOCKET_PORT")); } }
    public static string PASSWORD   { get { return Environment.GetEnvironmentVariable("PASSWORD"); } }
  
}