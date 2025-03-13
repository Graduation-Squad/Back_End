using Shipping.Core.Models;
using Shipping.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        IAppUserRepository AppUsers { get; }   
        IGenericRepository<T> Repository<T>() where T : BaseModel;
        Task<int> CompleteAsync();
    }

}
