namespace Dragoon_Log.Utils;

public class Responses
{
    public static Dictionary<String, String> Forbiden
    {
        get
        {
            return new Dictionary<String, String>()
            {
                {"error" , "Could not authenticate user"}
            };
        }
    }
}