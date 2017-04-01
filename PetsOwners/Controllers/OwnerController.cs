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
    public class OwnerController : ApiController
    {
        private IRepository _repo;

        public OwnerController(IRepository repo)
        {
            _repo = repo;
        }
        
        
        public OwnersRespondData Get(int page, int items,string filter)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Owner, OwnerView>()
                .ForMember("PetsCount", opt => opt.MapFrom(c => c.Pets.Count())));
            var ownCount = _repo.OwnersCount();
            var countofpages = _repo.GetOwnerPageCount(items);
            var data = _repo.GetOwners(page,items,filter);
            var dataview = Mapper.Map<IEnumerable<Owner>, IEnumerable<OwnerView>>(data);
            OwnersRespondData respond = new OwnersRespondData { countOfPages = countofpages,itemsOnPage = items, currentPage = page,ownersCount = ownCount , list = dataview};
            return respond;
        }
        [HttpGet]
        public OwnersRespondData Find(string name,int page, int items,string filter)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Owner, OwnerView>()
                .ForMember("PetsCount", opt => opt.MapFrom(c => c.Pets.Count())));
            var data = _repo.FindOwners(name, page, items,filter);
            var countofpages = _repo.GetOwnerPageCount(data, items);
           
            var dataview = Mapper.Map<IEnumerable<Owner>, IEnumerable<OwnerView>>(data);
            OwnersRespondData respond = new OwnersRespondData { countOfPages = countofpages, itemsOnPage = items, currentPage = page, list = dataview };
            return respond;
        }
        
        public IHttpActionResult Put(Owner owner)
        {
            if (owner == null)
                return BadRequest();
            if (_repo.isOwnerNameExist(owner.Name))
                if (owner.Id == 0)
                    ModelState.AddModelError("Name", "Owner whith this name is exist");
                else _repo.UpdateOwner(owner);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repo.AddOwner(owner);
            return Ok();
        }
        
        public void Delete(int id)
        {
            
        }


 
    }

  
}
