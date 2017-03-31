using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PetsOwners.Models.ModelsView
{
    public class OwnerView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int PetsCount { get; set; }
    }
}