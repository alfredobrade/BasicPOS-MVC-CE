using BasicPOS.BLL.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Firebase.Auth; //nuevo - descargado
using Firebase.Storage; //nuevo - descargado
using BasicPOS.DAL.GenericRepository;
using BasicPOS.Models;

namespace BasicPOS.BLL.Services
{
    public class FirebaseService : IFirebaseService
    {
        private readonly IGenericRepository<Configuracion> _config;

        public FirebaseService(IGenericRepository<Configuracion> config)
        {
            _config = config;
        }

        public async Task<string> SubirStorage(Stream streamArchivo, string carpetaDestino, string nombreArchivo)
        {
            string urlImg = "";

            try
            {
                IQueryable<Configuracion> query = await _config.Consultar(c => c.Resource.Equals("FireBase_Storage"));

                Dictionary<string, string> config = query.ToDictionary(keySelector: c => c.Property, elementSelector: c => c.Value);//TODO: key y element Selector son nombres cualquiera?

                var auth = new FirebaseAuthProvider(new FirebaseConfig(config["api_key"]));
                var a = await auth.SignInWithEmailAndPasswordAsync(config["correo"], config["clave"]);
                var cancellation = new CancellationTokenSource();
                var task = new FirebaseStorage(
                    config["ruta"],
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(config[carpetaDestino])
                    .Child(config[nombreArchivo])
                    .PutAsync(streamArchivo, cancellation.Token);

                urlImg = await task;

            }
            catch (Exception)
            {
                urlImg = "";
                throw;
            }

            return urlImg;

        }
        public async Task<bool> EliminarStorage(string carpetaDestino, string nombreArchivo)
        {
            try
            {
                IQueryable<Configuracion> query = await _config.Consultar(c => c.Resource.Equals("FireBase_Storage"));

                Dictionary<string, string> config = query.ToDictionary(keySelector: c => c.Property, elementSelector: c => c.Value);//TODO: key y element Selector son nombres cualquiera?

                var auth = new FirebaseAuthProvider(new FirebaseConfig(config["api_key"]));
                var a = await auth.SignInWithEmailAndPasswordAsync(config["correo"], config["clave"]);
                var cancellation = new CancellationTokenSource();
                var task = new FirebaseStorage(
                    config["ruta"],
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true
                    })
                    .Child(config[carpetaDestino])
                    .Child(config[nombreArchivo]) //TODO: aca estoy eliminando por nombre pero yo no puse nombre foto
                    .DeleteAsync();

                await task;

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

       
    }
}
