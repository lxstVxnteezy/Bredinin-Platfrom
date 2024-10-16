using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bredinin.TestProject.Service.Core
{
    internal class ServiceOwnErrorAttribute
    {
        public string Message { get; }

        public ServiceOwnErrorAttribute(string message)
        {
            Message = message;
        }
    }
}
