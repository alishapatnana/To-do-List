using CreateList.Models.ViewModel;
using CreateList.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateListController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(CreateListController));
        private readonly IItemRepository ItemRepo;
        public CreateListController(IItemRepository Item)
        {
            this.ItemRepo = Item;
        }
        [HttpPost]
        //[Route("api/CreateList/AddItem")]
        public async Task<IActionResult> AddItem([FromBody] ItemListViewModel model)
        {
            _log4net.Info("API initiated");
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var ItemAdded = await ItemRepo.AddItem(model);
            if (ItemAdded == -1)
            {
                _log4net.Error("Error in Post request");
                return Conflict("you cannot add more than one request");
            }
            else if (ItemAdded == 1)
            {
                _log4net.Info(" Http Post is accesed");
                return new StatusCodeResult(201);
            }
            else
            {
                return new StatusCodeResult(500);
            }
        }

    }
}
