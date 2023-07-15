using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BasicPOS.DAL.Context;
using Microsoft.EntityFrameworkCore;
using BasicPOS.DAL.GenericRepository;
using BasicPOS.DAL.IRepository;
using BasicPOS.DAL.Repository;
using BasicPOS.BLL.ServiceInterfaces;
using BasicPOS.BLL.Services;


namespace BasicPOS.IOC
{
    public static class Dependencies
    {
        public static void InyectarDependencia(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BasicPosPruebaContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
           
            services.AddScoped<IVentaRepository, VentaRepository>();
            services.AddScoped<ICorreoService, CorreoService>();
            services.AddScoped<IFirebaseService, FirebaseService>();
            services.AddScoped<IUtilidadesService, UtilidadesService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUsuarioService, UsuarioService>();


        }
    }
}
