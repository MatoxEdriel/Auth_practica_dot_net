using Application.Modules.FileServe;
using Application.Modules.FileServe.Models;
using Renci.SshNet;
namespace Infrastructure.Adapters;

public class SftpAdapter : IFileTransferService
{
    private readonly FileTransferSettings _settings;

    public SftpAdapter(FileTransferSettings settings)
    {
        _settings = settings;
    }
    public async Task<bool> UploadFileAsync(Stream fileStream, string remoteFilePath)
    {
      
        using var client = new SftpClient(_settings.Host, _settings.Port, _settings.Username, _settings.Password);
        client.Connect();
  
        await Task.Run(() => client.UploadFile(fileStream, remoteFilePath));
            
        client.Disconnect();
        return true;
    }

    public async Task<Stream> DownloadFileAsync(string remoteFilePath)
    {
        var client = new SftpClient(_settings.Host, _settings.Port, _settings.Username, _settings.Password);
        client.Connect();

        var memoryStream = new MemoryStream();
        await Task.Run(() => client.DownloadFile(remoteFilePath, memoryStream));
        memoryStream.Position = 0;
            
        client.Disconnect();
        return memoryStream;
    }
}