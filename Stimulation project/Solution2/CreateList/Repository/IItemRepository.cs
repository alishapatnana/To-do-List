using CreateList.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateList.Repository
{
    public interface IItemRepository
    {
        public Task<int> AddItem(ItemListViewModel model);
    }
}
