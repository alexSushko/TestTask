using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PetsOwners.Repositories;
using PetsOwners.Models;
using PetsOwners.Models.ModelsView;
using AutoMapper;

namespace PetsOwners.Controllers
{
    public class PetController : ApiController
    {
        private IRepository _repo;

        public PetController(IRepository repo)
        {
            _repo = repo;
        }


        public PetsRespondData Get(int ownerId, int page, int items, string filter)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Pet, PetView>()
                .ForMember("OwnerName", opt => opt.MapFrom(c => c.Owner.Name)));
            var countofpages = _repo.GetPetPageCount(ownerId,items);
            var data = _repo.GetPets(ownerId,page, items, filter);
            var dataview = Mapper.Map<IEnumerable<Pet>, IEnumerable<PetView>>(data);
            PetsRespondData respond = new PetsRespondData { countOfPages = countofpages, itemsOnPage = items, currentPage = page, list = dataview };
            return respond;
        }
        [HttpGet]
        public PetsRespondData Find(string name,int ownerId, int page, int items, string filter)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Pet, PetView>()
                .ForMember("OwnerName", opt => opt.MapFrom(c => c.Owner.Name)));
            var data = _repo.FindPets(ownerId,name, page, items, filter);
            var countofpages = _repo.GetPetPageCount(data, items);

            var dataview = Mapper.Map<IEnumerable<Pet>, IEnumerable<PetView>>(data);
            PetsRespondData respond = new PetsRespondData { countOfPages = countofpages, itemsOnPage = items, currentPage = page, list = dataview };
            return respond;
        }

        public IHttpActionResult Put(int ownerId, Pet pet)
        {
            if (pet == null)
                return BadRequest();
            if (_repo.isPetNameExist(pet.Name,ownerId))
                if (pet.Id == 0)
                    ModelState.AddModelError("Name", "Pet whith this name exist");
                else _repo.UpdatePet(pet);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repo.AddPet(pet);
            return Ok();
        }

        public void Delete(int id)
        {

        }



    }
    public class PetsRespondData
    {
        public int countOfPages;
        public int itemsOnPage;
        public int currentPage;
        public int petsCount;
        public IEnumerable<PetView> list;
    }

}

