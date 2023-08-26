using GW.DataAccess.Data;
using GW.DataAccess.Repository.IRepository;
using GW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW.DataAccess.Repository
{
    public class ProductRepository: Repository<Product>,IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context): base(context)
        {
            _context = context;
        }
        public void Update(Product obj)
        {
            var objFromDb = _context.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
                objFromDb.Price = obj.Price;    
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.ListPrice = obj.ListPrice;
                if(obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
               
            }
        }
    }
}
