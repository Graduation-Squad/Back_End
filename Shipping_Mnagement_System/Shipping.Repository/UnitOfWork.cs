using Shipping.Core.DomainModels;
using Shipping.Core.Repositories.Contracts;
using Shipping.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShippingContext _context;
        private readonly Dictionary<Type, object> _repositories = new();

        public IAppUserRepository AppUsers { get; }  

        public UnitOfWork(ShippingContext context, IAppUserRepository appUserRepository)
        {
            _context = context;
            AppUsers = appUserRepository;  
        }

        public IGenericRepository<T> Repository<T>() where T : BaseModel
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new GenericRepository<T>(_context);
                _repositories[type] = repositoryInstance;
            }

            return (IGenericRepository<T>)_repositories[type];
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("🔥 SaveChangesAsync failed: " + ex.InnerException?.Message ?? ex.Message);
                throw;
            }
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }


}
