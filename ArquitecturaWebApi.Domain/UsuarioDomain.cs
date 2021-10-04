using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Importar para acceder al proyecto Models y directorio Usuario, adempas importar el proyecto Data.Business
using ArquitecturaWebApi.Models.Usuario;
using ArquitecturaWebApi.Data.Business;

namespace ArquitecturaWebApi.Domain
{
    public class UsuarioDomain
    {
        public IEnumerable<UsuarioModel> ListUsers()
        {
            try
            {//Invocamos al método listar de la capa negocio "Data.Business"
                return Usuario.ListUsers();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertUsers(UsuarioModel usuarioModel)
        {
            try
            {
                return Usuario.InsertUsers(usuarioModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int UpdateUsers(int id, UsuarioModel usuarioModel)
        {
            try
            {
                return Usuario.UpdateUsers(id, usuarioModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int DeleteUsers(int id)
        {
            try
            {
                return Usuario.DeleteUsers(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}              