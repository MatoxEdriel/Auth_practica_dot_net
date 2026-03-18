namespace Application.Modules.FileServe;

public interface IFileTransferService
{
    
    //Concepto de la clase Stream 
    /*
     *Stream es una clase de base abstracta que representa un flujo de datos
     * un ejemplo perfecto seria tipo el transcurso de archivo ---> /red
     *
     * tambien seria el objeto que identifica la forma en la que lee el archivo .net
     * 
     *MemoryStream : trabajar con ram
     *FileStream: para trabajr con archivos fisicos 
     * 
     */
    Task<bool> UploadFileAsync(Stream fileStream, string remoteFilePath);

    Task<Stream> DownloadFileAsync(string remoteFilePath);
}