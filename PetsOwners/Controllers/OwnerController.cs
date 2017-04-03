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
using PetsOwners.RespondData;
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

            OwnersRespondData respond = _repo.GetOwners(page, items, filter);
            return respond;
        }
        [HttpGet]
        public OwnersRespondData Find(string name,int page, int items,string filter)
        {
            OwnersRespondData respond = _repo.FindOwners(name,page, items, filter);
            return respond;
        }
        [HttpPost]
        public IHttpActionResult Post(Owner owner)
        {
            if (owner == null)
                return BadRequest();
            if (_repo.isOwnerNameExist(owner.Name))
                if (owner.Id == 0)
                    ModelState.AddModelError("Name", "Owner whith this name exists");
                //else _repo.UpdateOwner(owner);
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _repo.AddOwner(owner);
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (_repo.DeleteOwner(id))
            {
                return Ok();
            }
            else
            {
                ModelState.AddModelError("id", "Owner whith this id does not exist");
                return BadRequest(ModelState);
            }
        }


 
    }

  
}
