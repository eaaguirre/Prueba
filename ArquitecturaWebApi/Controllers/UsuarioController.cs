using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web;
using System.Web.UI;
//Importar los proyectos
using ArquitecturaWebApi.Models.Usuario;
using ArquitecturaWebApi.Domain;

namespace ArquitecturaWebApi.Controllers
{
    public class UsuarioController : ApiController
    {
        UsuarioDomain usuarioDomain = new UsuarioDomain();

        /// <summary>
        /// GET: api/Usuario (retorna toda la lista)
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable<UsuarioModel>))]
        public IEnumerable<UsuarioModel> Get()
        {
            return usuarioDomain.ListUsers().ToArray();
            //return users;
        }

        /// <summary>
        /// GET: api/Usuario/5 (retorna los valores de un sólo registro según el valor id)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public IHttpActionResult Get(int id)
        public HttpResponseMessage Get(int id)
        {
            HttpResponseMessage httpMsg = null;
            var usuario = usuarioDomain.ListUsers().ToArray().FirstOrDefault((p) => p.id == id);
            if (usuario == null)
            {
                //NotFound() - en caso que se encuentre el registro;
                httpMsg = Request.CreateErrorResponse(HttpStatusCode.NotFound, "El Id (" + id.ToString() + ") no se encuentra registrado");
            }
            else {
                httpMsg = Request.CreateResponse(HttpStatusCode.OK, usuario);
            }
            return httpMsg;
        }

        // POST: api/Usuario
        /// <summary>
        /// Método para insertar los datos desde la Web API
        /// </summary>
        /// <param name="usuarioModel"></param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody]UsuarioModel usuarioModel)
        {
            HttpResponseMessage httpMsg = null;
            try
            {
                UsuarioModel usuario = new UsuarioModel();
                usuario.usuario = usuarioModel.usuario;
                usuario.contrasena = usuarioModel.contrasena;
                usuario.intentos = usuarioModel.intentos;
                usuario.nivelSeg = usuarioModel.nivelSeg;
                //usuario.id = usuarioModel.id;

                int result = usuarioDomain.InsertUsers(usuario);//Invocamos el proceso insertar y capturamos el nuevo ID generado
                httpMsg = Request.CreateResponse(HttpStatusCode.OK, usuario);//Confirm Request
                //httpMsg.Headers.Location = new Uri(Request.RequestUri + usuario.usuario.ToString());

                if (httpMsg.IsSuccessStatusCode) {//Validamos si el registro fue satisfactorio
                    httpMsg = Get(result);//Listamos los datos del nuevo registro ingresado, como parámetro enviamos el ID nuevo generado
                }
                else
                {//Caso contrario mostramos el siguiente mensaje en pantalla
                    httpMsg = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Ocurrio problemas al ingresar el registro");
                }
                return httpMsg;
            }
            catch (Exception ex)
            {
                httpMsg = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return httpMsg;
        }

        //public HttpResponseMessage Post([FromBody]UsuarioModel usuarioModel)
        /// <summary>
        /// Método PUT con el fin de actualizar los datos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuarioModel"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, [FromBody]UsuarioModel usuarioModel)
        {
            HttpResponseMessage httpMsg = null;
            try
            {
                UsuarioModel usuario = new UsuarioModel();
                usuario.usuario = usuarioModel.usuario;
                usuario.contrasena = usuarioModel.contrasena;
                usuario.intentos = usuarioModel.intentos;
                usuario.nivelSeg = usuarioModel.nivelSeg;
                usuario.id = id;

                usuarioDomain.UpdateUsers(id, usuario);//Invocamos el proceso actualizar en base los parametros ID y el modelo usuarioModel

                httpMsg = Request.CreateResponse(HttpStatusCode.OK, usuario);//Capturamos la respuesta de la petición del proceso realizado
                //httpMsg.Headers.Location = new Uri(Request.RequestUri + usuario.usuario.ToString());
                return httpMsg;
            }
            catch (Exception ex)
            {
                httpMsg = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            return httpMsg;
        }

        // DELETE: api/Usuario/5
        public HttpResponseMessage Delete(int id)
        {
            HttpResponseMessage httpMsg = null;
            try
            {
                UsuarioModel usuario = new UsuarioModel();
                usuario.id = id;

                usuarioDomain.DeleteUsers(id);
                httpMsg = Request.CreateResponse(HttpStatusCode.OK, usuario);

                return httpMsg;
            }
            catch (Exception ex)
            {
                httpMsg = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return httpMsg;
        }       
    }
}