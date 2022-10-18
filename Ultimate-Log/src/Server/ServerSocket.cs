using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Ultimate_Log.DTO;
using Ultimate_Log.Logger;

namespace Ultimate_Log.Server
{
    public class ServerSocket
    {
        private Socket connection;

        private Thread tipoThread;
        
        private NetworkStream socketStream;
        
        private BinaryReader reader;

        private Connection _dbConnection;

        public ServerSocket()
        {
            _dbConnection = new Connection();
        }
        
        public void StartServer()
        {
            tipoThread = new Thread(new ThreadStart(RunServer));
            tipoThread.Start();
        }
        
        protected void Server_Closing()
        {
            Environment.Exit(Environment.ExitCode);
        }
        
        public void RunServer()
        {
            try
            {
                TcpListener listener = new TcpListener(15000 );
                listener.Start();

                while ( true )
                {
                    Console.WriteLine("Aguardando Conexoes");
                    connection = listener.AcceptSocket();
                    socketStream = new NetworkStream(connection);
                    reader = new BinaryReader(socketStream, Encoding.UTF8, false);
                    
                    string response;
                    do
                    {
                        try
                        {
                            var bytes = reader.ReadBytes(15000);
                            response = Encoding.ASCII.GetString(bytes);
                            var obj = JsonConvert.DeserializeObject<ReceiverLog>(response);
                            var resp = _dbConnection.SaveLogInDatabse(obj);
                            if(!resp) Console.WriteLine("erro ao gravar o log");
                        }
                        catch (Exception ex)
                        {
                            break;
                        }
                        reader.Close();
                        socketStream.Close();
                        connection.Close();
                    }while (connection.Connected);
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error - {error}");
                Server_Closing();
            }
        }
        
    }
}