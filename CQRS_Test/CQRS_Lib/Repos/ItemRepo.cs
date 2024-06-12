using CQRS_Lib.Data;
using CQRS_Lib.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_Lib.Repos
{
    public class ItemRepo : IItems
    {
        private  DBData dB;

        public ItemRepo( DBData dB)
        {
            this.dB = dB;
        }

        public void AddItem(Items items)
        {
            
           dB.items.Add(items);
            dB.SaveChanges();
        }

        public Items GetItem(int id)
        {
            return dB.items.Where(x=>x.Id==id).FirstOrDefault();
        }

        public List<Items> GetItems()
        {
            return dB.items.ToList();
        }

        public void RemoveItem(int id)
        {
            var item = dB.items.Where(item=>item.Id==id).FirstOrDefault();
            dB.items.Remove(item);
            dB.SaveChanges();
        }

        public void UpdateItem(Items items)
        {
            dB.items.Update(items);
        }
    }
}
