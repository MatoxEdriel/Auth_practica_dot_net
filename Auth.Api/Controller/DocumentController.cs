using Application.Modules.FileServe;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class DocumentosController : ControllerBase
{
    private readonly IFileTransferService _fileTransferService;
    
    public DocumentosController(IFileTransferService fileTransferService)
    {
        _fileTransferService = fileTransferService;
    }
    
    [HttpPost("subir")]
    public async Task<IActionResult> SubirArchivo(IFormFile archivo)
    {
        if (archivo == null || archivo.Length == 0)
            return BadRequest("Archivo no válido");

        using var stream = archivo.OpenReadStream();
        var rutaRemota = $"/uploads/{archivo.FileName}";

        var exito = await _fileTransferService.UploadFileAsync(stream, rutaRemota);

        if (exito)
            return Ok(new { Succeeded = true, Message = "Archivo subido correctamente." });
        
        return StatusCode(500, "Error al subir el archivo.");
    }
    
}