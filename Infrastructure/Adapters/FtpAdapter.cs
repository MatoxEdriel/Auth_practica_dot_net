using Application.Modules.FileServe;
using Application.Modules.FileServe.Models;
using FluentFTP;
using FluentFTP.Helpers;

namespace Infrastructure.Adapters;

public class FtpAdapter:IFileTransferService
{
    //repasando patron aplicado
    /*
     * entonces aqui  yo llamaria la configuracion
     * 
     */

    private readonly FileTransferSettings _settings;
    
    public FtpAdapter(FileTransferSettings settings)
    {
        _settings = settings;
    }
    
    public async Task<bool> UploadFileAsync(Stream fileStream, string remoteFilePath)
    {
        using var client = new AsyncFtpClient(_settings.Host, _settings.Username, _settings.Password, _settings.Port);
        await client.Connect();
            
        var result = await client.UploadStream(fileStream, remoteFilePath);
        return result.IsSuccess();
    }
    
    public async Task<Stream> DownloadFileAsync(string remoteFilePath)
    {
        var client = new AsyncFtpClient(_settings.Host, _settings.Username, _settings.Password, _settings.Port);
        await client.Connect();
            
        var memoryStream = new MemoryStream();
        await client.DownloadStream(memoryStream, remoteFilePath);
        memoryStream.Position = 0;
        return memoryStream;
    }
}