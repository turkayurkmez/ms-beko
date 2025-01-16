﻿using EShop.Shared.SharedLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Catalog.Domain.Aggregates
{
    public class Category: AggregateRoot<int>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Category()
        {
                
        }

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Category(int id, string name, string description) : this(name, description)
        {
            Id = id;
        }
        
        public IQueryable<Product> Products { get; set; }
    }
}
