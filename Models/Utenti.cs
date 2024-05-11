namespace ECommerce.Models;

public class Utente
{
    public int Id { get; set; }
    public string? Nome {get; set;}
    public string? Cognome {get; set;}
    public string? Username {get; set;}
    public string? Password {get; set;}

    public string? Tipo { get; set; }
}
