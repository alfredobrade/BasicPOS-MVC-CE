using BasicPOS.DAL.GenericRepository;
using BasicPOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicPOS.DAL.IRepository
{
    public interface IVentaRepository : IGenericRepository<Sale>
    {
        Task<Sale> Registrar(Sale sale);
        Task<List<SaleProduct>> Reporte(DateTime inicio, DateTime fin);

    }
}
