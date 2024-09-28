using ExtModule.API.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtModule.API.Application.Factory
{
    public interface IRepositoryFactory
    {
        IRepository CreateRepository(string type);
    }
}
