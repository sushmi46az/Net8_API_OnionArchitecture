using ExtModule.API.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtModule.API.Application.Interfaces
{
    public interface ICrystalrepository
    {
          Task<byte[]> PrintCrystal(ClsPrintCrystal obj);
    }
}
