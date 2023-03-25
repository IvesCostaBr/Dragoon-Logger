using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Dragoon_Log.DTO;

public class ResponseCreateAuth
{
    public String ClientSecret { get; set; }
    public String ClientId { get; set; }
}

public class RegisterClient
{
    [Display(Name = "Client Name")]
    public String ClientName { get; set; }
    
    [Display(Name = "Description")]
    public String Description { get; set; }
    
}
public class Client
{   
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public String Id { get; set; }
    
    [Required]
    [Display(Name = "Client Name")]
    public String ClientName { get; set; }
    
    [Display(Name = "Description")]
    public String Description { get; set; }
    
    [Display(Name = "Client Id")]
    public String ClientId { get; set; }

    [Display(Name = "Client Secret")]
    public String ClientSecret { get; set; }
    
    [Display(Name = "Modify data")]
    public DateTime ModifiedAt { get; set; }
}


public class ClientResponse
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("Id")]
    public String Id { get; set; }
    
    [Required]
    [BsonElement("ClientName")]
    public String ClientName { get; set; }
    
    [BsonElement("Description")]
    public String Description { get; set; }
    
    [BsonElement("ClientId")]
    public String ClientId { get; set; }
    
    [BsonElement("ModifiedAt")]
    public DateTime ModifiedAt { get; set; }
}