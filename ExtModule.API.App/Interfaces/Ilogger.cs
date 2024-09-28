using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtModule.API.Application.Interfaces
{
    public interface Ilogger
    {
         void LogInfo(string filename,string message) ;
        void LogError(string filename,string message, Exception ex);
    }
}
