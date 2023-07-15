using BasicPOS.DAL.Context;
using BasicPOS.DAL.GenericRepository;
using BasicPOS.DAL.IRepository;
using BasicPOS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasicPOS.DAL.Repository
{
    public class VentaRepository : GenericRepository<Sale>, IVentaRepository
    {
        private readonly BasicPosPruebaContext _context;

        public VentaRepository(BasicPosPruebaContext context) : base(context) //aca le mandamos el contexto a GenericRepository 
        {
            _context = context;
        }
        public async Task<Sale> Registrar(Sale sale)
        {
            var ventaGenerada = new Sale();

            //TODO: preguntar sergio por lo de la transaction esta
            using (var transaction = _context.Database.BeginTransaction()) 
            {
                try
                {
                    //aca parece que solo actualiza stock
                    foreach (var item in sale.SaleProducts)
                    {
                        Product producto_encontrado = _context.Products.Where(p => p.IdProduct == item.IdProduct).First();

                        producto_encontrado.Stock = producto_encontrado.Stock - item.Quantity;
                        _context.Products.Update(producto_encontrado);

                    }
                    await _context.SaveChangesAsync();


                    //aca boludea con su "numero correlativo"
                    SaleNumber correlativo = _context.SaleNumbers.Where(n => n.Management == "venta").First();
                    correlativo.LastNumber = correlativo.LastNumber +1;
                    correlativo.UpdateDate = DateTime.Now;
                    _context.SaleNumbers.Update(correlativo);
                    await _context.SaveChangesAsync();

                    //aca boludea pasandole a sting el numero de venta loco
                    string ceros = string.Concat(Enumerable.Repeat("0", correlativo.DigitQty.Value));
                    string numeroVenta = ceros + correlativo.LastNumber.ToString();
                    numeroVenta = numeroVenta.Substring(numeroVenta.Length - correlativo.DigitQty.Value, correlativo.DigitQty.Value); //creo que aca le recorta la puntita
                    sale.SaleNumber = numeroVenta;
                    await _context.Sales.AddAsync(sale);
                    await _context.SaveChangesAsync();
                    
                    //sale.SaleNumber = correlativo.LastNumber.ToString().PadLeft(6,'0');

                    ventaGenerada = sale;


                    await transaction.CommitAsync(); //TODO: Sergio

                    

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

                return ventaGenerada;
            }
        }

        public async Task<List<SaleProduct>> Reporte(DateTime inicio, DateTime fin)
        {
            //TODO: que puta son todos los include esos? aprender SQL y Relaciones!!!!
            List<SaleProduct> listaResumen = await _context.SaleProducts
                .Include(v => v.IdSaleNavigation) //este include funciona dentro de SaleProduct
                .ThenInclude(u => u.IdUserNavigation) //dice que ThenInclude funciona para la venta dentro del Include
                .Include(v => v.IdSaleNavigation)  //ese de nuevo es contra SaleProduct
                .ThenInclude(tdv => tdv.IdSaleDocumentTypeNavigation)
                .Where(dv => dv.IdSaleNavigation.RegisterDate.Value.Date >= inicio.Date && 
                        dv.IdSaleNavigation.RegisterDate.Value.Date <= fin.Date)
                .ToListAsync();

            return listaResumen;
        }
    }
}
