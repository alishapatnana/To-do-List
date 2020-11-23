using ItemList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemList.Repository
{
    public interface IItemRepository
    {
        public Task<List<ItemListModel>> GetItemList();
        public Task<List<ItemListModel>> GetById(int id);
    }
}