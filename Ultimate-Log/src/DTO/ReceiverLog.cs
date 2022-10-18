using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ultimate_Log.DTO
{
    public class ReceiverLog
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public String Id { get; set; }
        
        [Required]
        [Display(Name = "Function")]
        public String Function { get; set; }
        
        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        
        [Required]
        [Display(Name = "User")]
        public String User { get; set; }
        
        [Required]
        [Display(Name = "Status")]
        public String Status { get; set; }
        
        [Required]
        [Display(Name = "Response")]
        public String Response { get; set; }
        
        [Required]
        [Display(Name = "Sender")]
        public String Sender { get; set; }
        
        [Required]
        [Display(Name = "Error")]
        public String Error { get; set; }
        
        [Required]
        [Display(Name = "Payload")]
        public String Payload { get; set; }
    }
}