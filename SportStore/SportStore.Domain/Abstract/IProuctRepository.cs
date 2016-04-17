using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportStore.Domain.Entities;

namespace SportStore.Domain.Abstract
{
    public interface IProuctRepository
    {
        IQueryable<Product> Products { get; }
    }
}
