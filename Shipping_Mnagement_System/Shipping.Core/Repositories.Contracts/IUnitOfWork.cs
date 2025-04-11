using Shipping.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Core.Repositories.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        IAppUserRepository AppUsers { get; }
        IGenericRepository<T> Repository<T>() where T : class;//BaseModel;
        Task<int> CompleteAsync();
    }

}
