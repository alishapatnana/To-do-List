using CreateList.Models;
using CreateList.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateList.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext dbContext;
        public ItemRepository(AppDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<int> AddItem(ItemListViewModel model)
        {
            var alreadyExist = await dbContext.ItemList.Where(x => x.Name == model.Name).FirstOrDefaultAsync();
            if (alreadyExist != null)
            {
                return -1;
            }
            else
            {
                ItemListModel temp = new ItemListModel()
                {
                    Name = model.Name,
                    Category = model.Category,
                };
                dbContext.ItemList.Add(temp);
                var row = dbContext.SaveChanges();
                return row;
            }
        }
    }
}
