using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetsOwners.Models;
using PetsOwners.RespondData; 
namespace PetsOwners.Repositories
{
    public interface IRepository
    {
       //Owners
        void AddOwner(Owner owner);
        bool UpdateOwner(Owner id);
        bool DeleteOwner(int id);
        bool isOwnerNameExist(string name);

        OwnersRespondData GetOwners(int page, int items, string filter="Id-Down");
        OwnersRespondData FindOwners(string name, int page, int items, string filter="Id-Down");
        

        //Pets
        void AddPet(Pet owner);
        bool UpdatePet(Pet id);
        bool DeletePet(int id);

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
