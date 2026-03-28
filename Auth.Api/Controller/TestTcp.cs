using System.Net;
using System.Net.Sockets;
using System.Text;
using Org.BouncyCastle.Crypto.Engines;

namespace Auth.Api.Controller;

public class TestTcp: BackgroundService
{
    
    private async Task HandleClientAsync(TcpClient client)
    {
        using (client)
        using (var stream = client.GetStream())
        using (var reader = new StreamReader(stream, Encoding.UTF8))
        using (var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true })
        {
            var mensajeRecibido = await reader.ReadLineAsync();
            Console.WriteLine($"Recibido del Gateway: {mensajeRecibido}");

            var respuesta = $"PROCESADO_OK: {mensajeRecibido}";

            await writer.WriteLineAsync(respuesta);
        }
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        
        //Con esto lo que trato de hacer es que haya una comunicacion 
        //dicho puerto seria el actual que tenemos el auth 
        var listener = new TcpListener(IPAddress.Any, 5055);
        listener.Start();
        
        
        //bucle 
        while (!stoppingToken.IsCancellationRequested)
        {
            
            var client = await listener.AcceptTcpClientAsync(stoppingToken);
            _ = HandleClientAsync(client);
        }

        
        



    }
}