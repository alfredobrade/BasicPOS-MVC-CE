using BasicPOS.BLL.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net; //nuevo
using System.Net.Mail;  //nuevo
using BasicPOS.DAL.GenericRepository;
using BasicPOS.Models;

namespace BasicPOS.BLL.Services
{
    public class CorreoService : ICorreoService
    {
        private readonly IGenericRepository<Configuracion> _config;

        public CorreoService(IGenericRepository<Configuracion> config)
        {
             _config = config;
        }
        public async Task<bool> EnviarCorreo(string correoDestino, string asunto, string mensaje)
        {
            try
            {
                IQueryable<Configuracion> query = await _config.Consultar(c => c.Resource.Equals("Servicio_Correo"));

                Dictionary<string, string> config = query.ToDictionary(keySelector : c => c.Property, elementSelector : c => c.Value);//TODO: key y element Selector son nombres cualquiera?

                var credenciales = new NetworkCredential(config["correo"], config["clave"]);

                var correo = new MailMessage()
                {
                    From = new MailAddress(config["correo"], config["alias"]),
                    Subject = asunto,
                    Body = mensaje,
                    IsBodyHtml = true
                };

                correo.To.Add(new MailAddress(correoDestino));

                var clienteServidor = new SmtpClient()
                {
                    Host = config["host"],
                    Port = int.Parse(config["puerto"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network, 
                    UseDefaultCredentials = false,
                    EnableSsl = true
                };

                clienteServidor.Send(correo);

                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
