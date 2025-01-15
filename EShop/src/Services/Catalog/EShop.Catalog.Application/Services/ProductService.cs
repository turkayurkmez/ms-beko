using EShop.Catalog.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Catalog.Application.Services
{
    public  class ProductService
    {

        //Bir use-case : bir fonksiyon.
        public IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>()
            {
                new Product("Şemsiye","Yağmura şemsiye",100,1000,null),
                new Product("Kalem","Kırmızı kalem",10,100,null),
                new Product("Defter","A5 defter",20,200,null),
                new Product("Bisiklet","Bisiklet",1000,10,null)
            };

            return products;
        }
    }
}
