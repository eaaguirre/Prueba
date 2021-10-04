using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Importar los poryectos Models y Data.Access, para acceder al directorio Usuario y DAL
using ArquitecturaWebApi.Models.Usuario;
using ArquitecturaWebApi.Data.Access.DAL;

namespace ArquitecturaWebApi.Data.Business
{
    public class Usuario
    {        
        public static IEnumerable<UsuarioModel> ListUsers()
        {
            try
            {
                //Retornamos la lista de valores del método ListUsers() que invocamos
                return UsuarioDAL.ListUsers();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static int InsertUsers(UsuarioModel usuarioModel)
        {
            try
            {
                return UsuarioDAL.InsertUsers(usuarioModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int UpdateUsers(int id, UsuarioModel usuarioModel)
        {
            try
            {
                return UsuarioDAL.UpdateUsers(id, usuarioModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int DeleteUsers(int id)
        {
            try
            {
                return UsuarioDAL.DeleteUsers(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}