namespace Domain.Exception;
using System;

public abstract class DomainException: Exception
{
    //obligamos que toda exception de logica de negocio tenga mensaje claro 
    protected DomainException(string message) : base(message) { }
    
    
}