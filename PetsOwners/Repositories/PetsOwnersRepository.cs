using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetsOwners.Models;
using PetsOwners.RespondData;
using AutoMapper;
using PetsOwners.Models.ModelsView;
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
        public bool UpdateOwner(Owner owner)
        {
            Owner o = context.Owners.Single(t => t.Id == owner.Id);
            o = owner;
            context.SaveChanges();
            return true;
        }

        public bool DeleteOwner(int id)
        {
            Owner o = GetOwner(id);
            if (o != null)
            {
                context.Owners.Remove(o);
                context.SaveChanges();
                return true;
            }
            else return false;
        }
        public Owner GetOwner(int id)
        {
            Owner owner = context.Owners.Find(id);
            return owner;
        }



        public OwnersRespondData GetOwners(int page, int items, string filter = "Id-Down")
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Owner, OwnerView>()
                .ForMember("PetsCount", opt => opt.MapFrom(c => c.Pets.Count())));
            int ownersCount = GetOwnersCount();
            int pageCount = RepositoryHelper.GetPageCount(ownersCount, items);
            var data = context.Owners.OwnerFilter(filter).OnPage(page, items);
            var dataview = Mapper.Map<IEnumerable<Owner>, IEnumerable<OwnerView>>(data);
            var ownerRespond = new OwnersRespondData()
            {
                countOfPages = pageCount,
                itemsOnPage = items,
                ownersCount = ownersCount,
                currentPage = page,
                list = dataview
            };
            return ownerRespond;
        }
        public OwnersRespondData FindOwners(string name, int page, int items, string filter = "Id-Down")
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Owner, OwnerView>()
               .ForMember("PetsCount", opt => opt.MapFrom(c => c.Pets.Count())));
            int ownersCount = GetOwnersCount(name);
            int pageCount = RepositoryHelper.GetPageCount(ownersCount, items);
            var data = context.Owners.Where(t => t.Name.Contains(name)).OwnerFilter(filter).OnPage(page, items);
            var dataview = Mapper.Map<IEnumerable<Owner>, IEnumerable<OwnerView>>(data);
            var ownerRespond = new OwnersRespondData()
            {
                countOfPages = pageCount,
                itemsOnPage = items,
                ownersCount = ownersCount,
                currentPage = page,
                list = dataview
            };
            return ownerRespond;
        }


        public IEnumerable<Pet> GetOwnerPets(int ownerId)
        {
            IEnumerable<Pet> pets = GetOwner(ownerId).Pets;
            return pets;
        }



        public bool isOwnerNameExist(string name)
        {
            return context.Owners.Any(t => t.Name == name);
        }


        private int GetOwnersCount()
        {
            return context.Owners.Count();
        }

        private int GetOwnersCount(string name)
        {
            return context.Owners.Where(t => t.Name.Contains(name)).Count();
        }
        //
        //Pet
        //

        public void AddPet(Pet owner)
        {
            context.Pets.Add(owner);
            context.SaveChanges();
        }

        public bool DeletePet(int id)
        {
            Pet o = GetPet(id);
            context.Pets.Remove(o);
            context.SaveChanges();
            return true;
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
            return (pets.Count() - 1) / itemsOnPageCount + 1;

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

            IEnumerable<Pet> pets = context.Pets.Where(t => t.OwnerId == ownerId && t.Name.Contains(name)).PetFilter(filter).Skip((page - 1) * items).Take(items);
            return pets;
        }
        public int PetsCount(int ownerId)
        {
            return context.Pets.Where(t => t.OwnerId == ownerId).Count();
        }

        public bool UpdatePet(Pet id)
        {
            throw new NotImplementedException();
        }
    }
}