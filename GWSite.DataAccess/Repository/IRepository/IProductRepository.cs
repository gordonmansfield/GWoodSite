using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW.Models;

namespace GW.DataAccess.Repository.IRepository
{
    public interface IProductRepository: IRepository<Product>
    {
            void Update(Product obj);
    }
}
