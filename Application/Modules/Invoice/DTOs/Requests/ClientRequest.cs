namespace Application.Modules.Invoice.DTOs.Requests;

public class ClientRequest
{
    public string DocumentNumber { get; set; } 
    public string FullName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    
}