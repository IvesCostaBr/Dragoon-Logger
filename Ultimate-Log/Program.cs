using System;
using Ultimate_Log.Server;

try
{
    var server  = new ServerSocket();
    server.StartServer();
}
catch (Exception e)
{
    Console.WriteLine(e);
}
