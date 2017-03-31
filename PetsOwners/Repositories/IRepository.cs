using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetsOwners.Models;
namespace PetsOwners.Repositories
{
    public interface IRepository
    {
       //Owners
        void AddOwner(Owner owner);
        void UpdateOwner(Owner owner);

        bool isOwnerNameExist(string name);
        Owner GetOwner(int id);
        int GetOwnerPageCount(int itemsOnPageCount);
        int GetOwnerPageCount(IEnumerable<Owner> owners, int itemsOnPageCount);
        IEnumerable<Owner> GetOwners();

        IEnumerable<Owner> GetOwners(int page, int items, string filter="Id-Down");
        IEnumerable<Owner> FindOwners(string name, int page, int items, string filter="Id-Down");
        int OwnersCount();

        //Pets
        void AddPet(Pet owner);
        void UpdatePet(Pet owner);

        bool isPetNameExist(string name,int ownerId);
        Pet GetPet(int id);
        int GetPetPageCount(int ownerId, int itemsOnPageCount);
        int GetPetPageCount(IEnumerable<Pet> pets, int itemsOnPageCount);
        IEnumerable<Pet> GetPets(int ownerId);

        IEnumerable<Pet> GetPets(int ownerId, int page, int items, string filter = "Id-Down");
        IEnumerable<Pet> FindPets(int ownerId, string name, int page, int items, string filter = "Id-Down");
        int PetsCount(int ownerId);
    }
}
