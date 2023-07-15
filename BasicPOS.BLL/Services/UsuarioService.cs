using BasicPOS.BLL.ServiceInterfaces;
using BasicPOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using BasicPOS.DAL.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace BasicPOS.BLL.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IFirebaseService _firebaseService;
        private readonly IUtilidadesService _utilidadesService;
        private readonly ICorreoService _correoService;

        public UsuarioService(IGenericRepository<User> repository, IFirebaseService firebaseService, IUtilidadesService utilidadesService, ICorreoService correoService)
        {
            _repository = repository;
            _firebaseService = firebaseService;
            _utilidadesService = utilidadesService;
            _correoService = correoService;
        }

        public async Task<bool> CambiarClave(int id, string claveActual, string nuevaClave)
        {
            try
            {
                User usuario_encontrado = await _repository.Obtener(u => u.IdUser == id);
                if (usuario_encontrado == null) throw new TaskCanceledException("el usuario no existe");

                if(usuario_encontrado.Password != _utilidadesService.ConvertirSha256(claveActual)) throw new TaskCanceledException("la clave es incorrecta");

                usuario_encontrado.Password = _utilidadesService.ConvertirSha256(nuevaClave);

                bool respuesta = await _repository.Editar(usuario_encontrado);

                return respuesta;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        public async Task<User> Crear(User user, Stream foto = null, string nombreFoto = "", string urlPlantillaCorreo = "")
        {
            //validad que el usuario existe
            User usuario_existe = await _repository.Obtener(u => u.UserEmail == user.UserEmail);
            if (usuario_existe != null) throw new TaskCanceledException("El correo ya existe");

            try
            {
                string clave_generada = _utilidadesService.GenerarClave();
                //a esta clave la guardamos encriptada
                user.Password = _utilidadesService.ConvertirSha256(clave_generada);
                //--poner el nombre de la foto pero yo no puse esta propiedad
                if (foto != null)
                {
                    string urlFoto = await _firebaseService.SubirStorage(foto, "carpeta_usuario", nombreFoto);
                    user.PhotoUrl = urlFoto;

                }

                User usuario_creado = await _repository.Crear(user);

                if (usuario_creado.IdUser == 0) throw new TaskCanceledException("no se pudo crear");

                if (urlPlantillaCorreo != "")
                {
                    urlPlantillaCorreo = urlPlantillaCorreo.Replace("[correo]", usuario_creado.UserEmail).Replace("[clave]", clave_generada);
                    string htmlCorreo = "";
                    //esto es para hacer la lectura de EnviarClave.cshtml
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlPlantillaCorreo);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream dataStream = response.GetResponseStream())
                        {
                            StreamReader readerStream = null;

                            if (response.CharacterSet == null) readerStream = new StreamReader(dataStream);
                            else readerStream = new StreamReader(dataStream, Encoding.GetEncoding(response.CharacterSet));

                            htmlCorreo = readerStream.ReadToEnd();
                            response.Close();
                            readerStream.Close();


                        }
                    }

                    if (htmlCorreo != "") await _correoService.EnviarCorreo(usuario_creado.UserEmail, "Cuenta Creada", htmlCorreo);
                }

                IQueryable<User> query = await _repository.Consultar(u => u.IdUser == usuario_creado.IdUser);
                usuario_creado = query.Include(r => r.IdRoleNavigation).First();

                return usuario_creado;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<User> Editar(User user, Stream foto = null, string nombreFoto = "")
        {
            //validad que el usuario existe
            //no entendi esta validacion.. la idea es que no hayan 2 usuarios con el mismo correo
            User usuario_existe = await _repository.Obtener(u => u.UserEmail == user.UserEmail && u.IdUser != user.IdUser);
            if (usuario_existe != null) throw new TaskCanceledException("El correo ya existe");

            try
            {
                IQueryable<User> queryUsuario = await _repository.Consultar(u => u.IdUser == user.IdUser);
                User usuario_editar = queryUsuario.First();
                usuario_editar.UserName = user.UserName;
                usuario_editar.UserEmail = user.UserEmail;
                usuario_editar.PhoneNumber = user.PhoneNumber;
                usuario_editar.IdRole = user.IdRole;

                if (foto != null)
                {
                    string urlFoto = await _firebaseService.SubirStorage(foto, "carpeta_usuario", nombreFoto);
                    usuario_editar.PhotoUrl = urlFoto;

                }
                bool respuesta = await _repository.Editar(usuario_editar);
                if (!respuesta) throw new TaskCanceledException("no se pudo editar");

                User usuario_editado = queryUsuario.Include(r => r.IdRoleNavigation).First();

                return usuario_editado;

            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                User usuario_encontrado = await _repository.Obtener(u => u.IdUser == id);

                if (usuario_encontrado == null) throw new TaskCanceledException("no se encontro");

                //string nombreFoto = usuario_encontrado. //no puse nombre foto
                bool respuesta = await _repository.Eliminar(usuario_encontrado);

                //TODO:
                //if (!respuesta) await _firebaseService.EliminarStorage("carpeta_usuario", nombreFoto);
                
                return respuesta;
            }
            catch (Exception)
            {
                return false;

                throw;
            }


        }
        public async Task<bool> GuardarPerfil(User user)
        {
            try
            {
                User usuario_encontrado = await _repository.Obtener(u => u.IdUser == user.IdUser);
                if (usuario_encontrado == null) throw new TaskCanceledException("el usuario no existe");

                usuario_encontrado.UserEmail = user.UserEmail;
                usuario_encontrado.PhoneNumber = user.PhoneNumber;

                bool respuesta = await _repository.Editar(usuario_encontrado);

                return respuesta;

            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        public async Task<List<User>> Lista()
        {
            IQueryable<User> query = await _repository.Consultar();
            return query.Include(r => r.IdRoleNavigation).ToList();
        }
        public async Task<User> ObtenerPorCredenciales(string correo, string clave)
        {
            string clave_encriptada = _utilidadesService.ConvertirSha256(clave);
            User usuario_encontrado = await _repository.Obtener(u => u.UserEmail.Equals(correo) && u.Password.Equals(clave_encriptada));

            return usuario_encontrado;
        }
        public async Task<User> ObtenerPorId(int id)
        {
            IQueryable<User> query = await _repository.Consultar(u => u.IdUser == id);
            User resultado = query.Include(r => r.IdRoleNavigation).FirstOrDefault();
            return resultado;
        }
        public async Task<bool> RestablecerClave(string correo, string urlPlantillaCorreo)
        {
            try
            {
                User usuario_encontrado = await _repository.Obtener(u => u.UserEmail == correo);
                if (usuario_encontrado == null) throw new TaskCanceledException("el usuario no existe");

                string clave_generada = _utilidadesService.GenerarClave();
                usuario_encontrado.Password = _utilidadesService.ConvertirSha256(clave_generada);

                urlPlantillaCorreo = urlPlantillaCorreo.Replace("[clave]", clave_generada);
                string htmlCorreo = "";
                //esto es para hacer la lectura de EnviarClave.cshtml
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlPlantillaCorreo);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        StreamReader readerStream = null;

                        if (response.CharacterSet == null) readerStream = new StreamReader(dataStream);
                        else readerStream = new StreamReader(dataStream, Encoding.GetEncoding(response.CharacterSet));

                        htmlCorreo = readerStream.ReadToEnd();
                        response.Close();
                        readerStream.Close();


                    }
                }
                bool correo_enviado = false;
                if (htmlCorreo != "") correo_enviado = await _correoService.EnviarCorreo(correo , "Contraseña Reestablecida", htmlCorreo);
                if (!correo_enviado) throw new TaskCanceledException("Contraseña no reestablecida");

                bool respuesta = await _repository.Editar(usuario_encontrado);
                return respuesta;
            }
            catch (Exception)
            {
                return false;

                throw;
            }

        }
    }
}
