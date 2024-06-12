using CQRS_Lib.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_Lib.Repos
{
    public interface IItems
    {
        public List<Items> GetItems();
        public Items GetItem(int id);

        public void AddItem(Items items);
        public void RemoveItem(int id);

        public void UpdateItem(Items items);
    }
}
