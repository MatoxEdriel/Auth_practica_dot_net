namespace Application.Modules.FileServe.Models;

// confirguracion del fileserver como es una logica o configuracion de negocio pues va directo a application
public class FileTransferSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Protocol { get; set; }
}