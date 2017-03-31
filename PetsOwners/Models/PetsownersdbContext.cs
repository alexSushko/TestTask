using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace PetsOwners.Models
{
    public partial class PetsownersdbContext : DbContext
    {
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }

        public PetsownersdbContext() : base("DefaultConnection") { }

        
    }
}