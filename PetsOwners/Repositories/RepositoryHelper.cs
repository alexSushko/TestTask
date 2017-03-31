using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetsOwners.Models;
namespace PetsOwners.Repositories
{
    public static class RepositoryHelper
    {
        public static IEnumerable<Owner> OwnerFilter(this IEnumerable<Owner> owners, string filter)
        {
            var ownList = owners.ToList();
            string filterPart = filter.Split('-')[0].ToLower();
            string reversPart;
            try
            {
                reversPart = filter.Split('-')[1].ToLower();
            }
            catch (IndexOutOfRangeException e) { reversPart = "down"; }
            switch (filterPart)
            {
                case "name":
                    ownList.OrderBy(t => t.Name);
                    break;
                case "count":
                    ownList.OrderBy(t => t.Pets.Count);
                    break;
                default:
                    ownList.OrderBy(t => t.Id);
                    break;
            }
            if (reversPart == "up") ownList.Reverse();
            
            return ownList.AsEnumerable();
        }
        public static IEnumerable<Pet> PetFilter(this IEnumerable<Pet> pets, string filter)
        {
            var ownList = pets.ToList();
            string filterPart = filter.Split('-')[0].ToLower();
            string reversPart;
            try
            {
                reversPart = filter.Split('-')[1].ToLower();
            }
            catch (IndexOutOfRangeException e) { reversPart = "down"; }
            switch (filterPart)
            {
                case "name":
                    ownList.OrderBy(t => t.Name);
                    break;                
                default:
                    ownList.OrderBy(t => t.Id);
                    break;
            }
            if (reversPart == "up") ownList.Reverse();

            return ownList.AsEnumerable();
        }
    }
}