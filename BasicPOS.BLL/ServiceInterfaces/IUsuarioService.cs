using BasicPOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicPOS.BLL.ServiceInterfaces
{
    public interface IUsuarioService
    {
        Task<List<User>> Lista();
        Task<User> Crear(User user, Stream foto = null, string nombreFoto = "", string urlPlantillaCorreo = "");
        Task<User> Editar(User user, Stream foto = null, string nombreFoto = "");
        Task<bool> Eliminar(int id);
        Task<User> ObtenerPorCredenciales(string correo, string clave);
        Task<User> ObtenerPorId(int id);
        Task<bool> GuardarPerfil(User user);
        Task<bool> CambiarClave(int id, string claveActual, string nuevaClave);
        Task<bool> RestablecerClave(string correo, string urlPlantillaCorreo);

    }
}
