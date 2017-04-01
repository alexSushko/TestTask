using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetsOwners.Models.ModelsView;
namespace PetsOwners.RespondData
{
   
        public class OwnersRespondData
        {
            public int countOfPages;
            public int itemsOnPage;
            public int currentPage;
            public int ownersCount;
            public IEnumerable<OwnerView> list;
        }
 
}