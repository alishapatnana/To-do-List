using ItemList.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemListController : ControllerBase
    {
       static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(ItemListController));
        private readonly IItemRepository ItemRepo;
        public ItemListController(IItemRepository Item)
        {
            this.ItemRepo = Item;
        }
        [HttpGet]
        //[Route("api/ItemList/GetList")]
        public async Task<IActionResult> GetList()
        {
            _log4net.Info("API initiated");
            try
            {
                _log4net.Info(" Http GET is accesed");
                var listOfItems = await ItemRepo.GetItemList(); //dbContext.ItemList.ToList();
                return Ok(listOfItems);

            }
            catch
            {
                _log4net.Error("Error in Get request");
                return NotFound();
            }
            

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _log4net.Info(" Http GET is accesed");
                var listOfItems = await ItemRepo.GetById(id);
                return Ok(listOfItems);
            }
            
           catch
            {
                _log4net.Error("Error in Get request");
                return NotFound();
               // return Ok(null);
            }
           

        }

    }
}
