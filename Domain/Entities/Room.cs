namespace Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }

    
    //Dapper 
    // Dapper necesita instanciar un objeto para el mapeo interno
    // que tiene entre la base de dato y las entidades
    protected Room()
    {
    }


    public Room(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new System.Exception("El nombre de la sala es obligatorio."); 
                
        Name = name;
    }
}