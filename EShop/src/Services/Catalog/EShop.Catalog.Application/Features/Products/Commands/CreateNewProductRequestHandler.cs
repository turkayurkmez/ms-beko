using EShop.Catalog.Application.Features.Products.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Catalog.Application.Features.Products.Commands
{
    public class CreateNewProductRequestHandler
    {
        public Task<Guid> Handle(CreateNewProductRequest request)
        {
            //request'i al ve db'ye kaydet

            throw new NotImplementedException();
        }
    }
}
