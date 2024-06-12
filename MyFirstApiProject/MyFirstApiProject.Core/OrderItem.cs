﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstApiProject.Core
{
    public class OrderItem
    {
        public int Id { get; set; }
        
        public int OrderId { get; set; }
        public Order order { get; set; }
        public int ItemId { get; set; }
        public Item item { get; set; }
    }
}
