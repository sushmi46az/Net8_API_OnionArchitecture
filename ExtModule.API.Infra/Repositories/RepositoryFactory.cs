using ExtModule.API.Application.Factory;
using ExtModule.API.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtModule.API.Infrastructure.Repositories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IRepository CreateRepository(string type) 
        {
            switch (type)
            {
                case "ERP":
                    return (IRepository)_serviceProvider.GetService(typeof(IERPRepository));
                case "CRM":
                    return (IRepository)_serviceProvider.GetService(typeof(ICRMRepository));
                default:
                    throw new ArgumentException("Invalid type", nameof(type));
            }
        }
    }
}
