﻿using System;
using Ultimate_Log.Server;

try
{
    var server  = new ServerSocket();
    Console.WriteLine($" [ {DateTime.Now} ]  Starting Dragoon Logger : Latest");
    server.StartServer();
}
catch (Exception e)
{
    Console.WriteLine(e);
}
