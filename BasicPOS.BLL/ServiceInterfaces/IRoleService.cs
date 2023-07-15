using BasicPOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicPOS.BLL.ServiceInterfaces
{
    public interface IRoleService
    {
        Task<List<Role>> Lista();

    }
}
