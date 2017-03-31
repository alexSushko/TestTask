using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsOwners.Models
{
    public partial class Pet
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Enter pet name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The length of the string must be between 3 and 50 characters")]
        [RegularExpression(@"[a-zA-Z]", ErrorMessage = "Only leters")]
        public string Name { get; set; }
        public long OwnerId { get; set; }
        
        public virtual Owner Owner { get; set; }
    }
}
