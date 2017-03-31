using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetsOwners.Models;
namespace PetsOwners.Repositories
{
    public class PetsOwnersRepository : IRepository
    {
        private PetsownersdbContext context;
        
        public PetsOwnersRepository()
        {
            context = new PetsownersdbContext();
        }
        
        //
        //Owner
        //

        public void AddOwner(Owner owner)
        {
            context.Owners.Add(owner);
            context.SaveChanges();
        }
        public void UpdateOwner(Owner owner)
        {

        }

        public Owner GetOwner(int id)
        {
            Owner owner = context.Owners.Find(id);
            return owner;
        }

        public IEnumerable<Owner> GetOwners()
        {
            
            return context.Owners;
        }
     
        public IEnumerable<Owner> GetOwners(int page, int items, string filter="Id-Down")
        {

            IEnumerable<Owner> owners = context.Owners.OwnerFilter(filter).Skip((page - 1) * items).Take(items);
            return owners;
        }
        public IEnumerable<Owner> FindOwners(string name, int page, int items, string filter = "Id-Down")
        {
            IEnumerable<Owner> owners = context.Owners.Where(t => t.Name.Contains(name)).OwnerFilter(filter).Skip((page - 1) * items).Take(items);
            return owners;
        }

 
        public IEnumerable<Pet> GetOwnerPets(int ownerId)
        {
            IEnumerable<Pet> pets = GetOwner(ownerId).Pets;
            return pets;
        }

        public int GetOwnerPageCount(int itemsOnPageCount)
        {
            return (context.Owners.Count()-1) / itemsOnPageCount + 1;
        }
        public int GetOwnerPageCount(IEnumerable<Owner> owners , int itemsOnPageCount)
        {
            return (owners.Count() - 1) / itemsOnPageCount + 1;
        }
        public bool isOwnerNameExist(string name)
        {
            return context.Owners.Any(t=>t.Name==name);
        }
        public int OwnersCount()
        {
            return context.Owners.Count();
        }

        //
        //Pet
        //

        public void AddPet(Pet owner)
        {
            context.Pets.Add(owner);
            context.SaveChanges();
        }

        public void UpdatePet(Pet owner)
        {
            throw new NotImplementedException();
        }

        public bool isPetNameExist(string name, int ownerId)
        {
            return context.Pets.Any(t => t.OwnerId == ownerId && t.Name == name);
        }

        public Pet GetPet(int id)
        {
            Pet pet = context.Pets.Find(id);
            return pet;
        }

        public int GetPetPageCount(int ownerId, int itemsOnPageCount)
        {

            return (context.Pets.Where(t => t.Id == ownerId).Count() - 1) / itemsOnPageCount + 1;
            
        }

        public int GetPetPageCount(IEnumerable<Pet> pets, int itemsOnPageCount)
        {
            return (pets.Count()-1) / itemsOnPageCount + 1;

        }

        public IEnumerable<Pet> GetPets(int ownerId)
        {
            
            return context.Pets;
        }

        public IEnumerable<Pet> GetPets(int ownerId, int page, int items, string filter = "Id-Down")
        {
            IEnumerable<Pet> pets = context.Pets.PetFilter(filter).Skip((page - 1) * items).Take(items);
            return pets;
        }

        public IEnumerable<Pet> FindPets(int ownerId, string name, int page, int items, string filter = "Id-Down")
        {
           
            IEnumerable<Pet> pets = context.Pets.Where(t => t.OwnerId==ownerId && t.Name.Contains(name)).PetFilter(filter).Skip((page - 1) * items).Take(items);
            return pets;
        }
        public int PetsCount(int ownerId)
        {
            return context.Pets.Where(t => t.OwnerId == ownerId).Count();
        }
    }
}