using BasicPOS.BLL.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography; //nuevo


namespace BasicPOS.BLL.Services
{
    public class UtilidadesService : IUtilidadesService
    {
        public string GenerarClave()
        {
            
            return Guid.NewGuid().ToString("N").Substring(0, 6);
        }
        
        
        public string ConvertirSha256(string texto)
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create()) 
            {
                Encoding encoding = Encoding.UTF8;

                byte[] result = hash.ComputeHash(encoding.GetBytes(texto));

                foreach (byte item in result)
                {
                    stringBuilder.Append(item.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }

        
    }
}
