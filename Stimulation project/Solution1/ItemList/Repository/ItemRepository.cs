using ItemList.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemList.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext dbContext;
        public ItemRepository(AppDbContext context)
        {
            this.dbContext = context;
        }

        public async Task<List<ItemListModel>> GetById(int id)
        {
            return await dbContext.ItemList.Where(p => p.Id == id).ToListAsync(); ;
        }

        public async Task<List<ItemListModel>> GetItemList()
        {
            return await dbContext.ItemList.ToListAsync();
        }
    }
}
