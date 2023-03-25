using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using Dragoon_Log.DTO;
using Dragoon_Log.Repository;

namespace Dragoon_Log.Server
{
    public class ServerSocket: BackgroundService
    {
        private Socket connection;

        private Thread tipoThread;
        
        private NetworkStream socketStream;
        
        private BinaryReader reader;

        private LogRepository _repository = new LogRepository();
        
        protected void Server_Closing()
        {
            Environment.Exit(Environment.ExitCode);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    TcpListener listener = new TcpListener(Config.SOCKET_PORT);
                    listener.Start();

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Aguardando Conexoes");
                        connection = await listener.AcceptSocketAsync();
                        socketStream = new NetworkStream(connection);
                        while (!stoppingToken.IsCancellationRequested && connection.Connected)
                        {
                            string response;
                            do
                            {
                                try
                                {
                                    reader = new BinaryReader(socketStream, Encoding.UTF8, false);
                                    var bytes = reader.ReadBytes(15000);
                                    response = Encoding.ASCII.GetString(bytes);
                                    var obj = JsonConvert.DeserializeObject<ReceiverLog>(response);
                                    var resp = await _repository.Save(obj);
                                    if (!resp) Console.WriteLine($"error ao salvar - {obj}");
                                }
                                catch
                                {
                                    socketStream.Close();
                                    connection.Close();
                                    break;
                                }

                                reader.Close();
                            } while (!stoppingToken.IsCancellationRequested &&
                                     connection.Connected);
                        }
                    }
                }
                Console.WriteLine("Finalizando Worker");
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error - {error}");
                Server_Closing();
            }
        }
    }
}