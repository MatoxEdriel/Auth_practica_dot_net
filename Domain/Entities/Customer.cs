namespace Domain.Entities;

public class Customer
{
    public int Id { get; private set; }
    public string FullName { get; private set; }
    public string Email { get; private set; }
    
    //POR QUE PROTECTED 
    //MI EXPLICACION:  
    /*
     *
     *Protected le asegura el acceso de la data
     * solamente a los que se heredan en si 
     *
     
     */
    protected Customer() { }

    public Customer(string fullName, string email)
    {
        FullName =fullName;
        Email = email; 
    }
}