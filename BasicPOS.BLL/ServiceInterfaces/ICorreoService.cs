using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicPOS.BLL.ServiceInterfaces
{
    public interface ICorreoService
    {
        Task<bool> EnviarCorreo(string correoDestino, string asunto, string mensaje);

    }
}
