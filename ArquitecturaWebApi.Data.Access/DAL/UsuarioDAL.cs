using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Importar para acceder al poryecto Models y directio Usuario
using ArquitecturaWebApi.Models.Usuario;

using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ArquitecturaWebApi.Data.Access.DAL
{
    public class UsuarioDAL
    {
        public static IEnumerable<UsuarioModel> ListUsers()
        {//Acceder al archivo de configuración para ller la cadena de conexión
            string cs = ConfigurationManager.ConnectionStrings["CnBD"].ConnectionString;

            List<UsuarioModel> resultUsers = new List<UsuarioModel>();//Instanciamos a nuestro modelo
            using (SqlConnection con = new SqlConnection(cs))//Inicio la conexión a la base de datos
            {
                con.Open();//Abro la conexión a la base de datos
                SqlCommand com = new SqlCommand("SP_SELECCIONAR_USUARIO", con);//Defino el comando a ejeuctar
                com.CommandType = CommandType.StoredProcedure;//Comando de tipo procedimiento almacenado
                SqlDataReader rdr = com.ExecuteReader(); //Ejecuto y cargo lo valor en el data reader
                //Inicio a recorrer los valores del data reader y pasar lo valores al modelo UsuarioModel
                while (rdr.Read())
                {
                    var usuario = new UsuarioModel();
                    usuario.id = Convert.ToInt32(rdr["id"]);
                    usuario.usuario = rdr["usuario"].ToString();
                    usuario.contrasena = rdr["contrasena"].ToString();
                    usuario.intentos = Convert.ToInt32(rdr["intentos"]);
                    usuario.nivelSeg = decimal.Parse(rdr["nivelSeg"].ToString());
                    /*entPedido.FEC_PEDIDO = DateTime.Parse(rdr["FEC_PEDIDO"].ToString());*/
                    resultUsers.Add(usuario);
                }
                return resultUsers;//retorno todo los valores
            }
        }

        public static int InsertUsers(UsuarioModel usuarioModel)//Como parámetro enviaremos lo datos de nuestro modelo UsuarioModel
        {//Acceder al archivo de configuración para ller la cadena de conexión
            string cs = ConfigurationManager.ConnectionStrings["CnBD"].ConnectionString;

            int result;
            //int intIdUser = 0;

            using (SqlConnection con = new SqlConnection(cs))//Inicio la conexión a la base de datos
            {
                con.Open();//Abrimos la conexión a la base de datos
                SqlCommand com = new SqlCommand("SP_INSERTAR_USUARIO", con);//Definimos el comando a ejeuctar
                com.CommandType = CommandType.StoredProcedure;//Comando de tipo procedimiento almacenado

                //Agregamos los parametros y pasamos los datos a nuestro modelo
                com.Parameters.Add("@usuario", SqlDbType.VarChar, 50).Value = usuarioModel.usuario;
                com.Parameters.Add("@contrasena", SqlDbType.VarChar, 50).Value = usuarioModel.contrasena;
                com.Parameters.Add("@intentos", SqlDbType.Int, 5).Value = usuarioModel.intentos;
                com.Parameters.Add(new SqlParameter("@nivelSeg", SqlDbType.Decimal)
                {
                    Precision = 18,
                    Scale = 0
                }).Value = usuarioModel.nivelSeg;

                //Capturamos el dato de salida - ID de la tablas usuario
                com.Parameters.Add("@oid", SqlDbType.Int, 5).Direction = ParameterDirection.Output;

                com.ExecuteNonQuery();//Ejecuto y comando e insertamos los datos
                                      
                result = Convert.ToInt32(com.Parameters["@oid"].Value); //Paso el ID generado en la base de datos

                //result = Convert.ToInt32(com.ExecuteScalar());
            }
            return result;//Captura el ID generado
        }

        public static int UpdateUsers(int id, UsuarioModel usuarioModel)//Como parámetro de entrada enviamos el id y la lista de modelo UsuarioModel
        {//Acceder al archivo de configuración para ller la cadena de conexión
            string cs = ConfigurationManager.ConnectionStrings["CnBD"].ConnectionString;

            int result;
            using (SqlConnection con = new SqlConnection(cs))//Inicio la conexión a la base de datos
            {
                con.Open();//Abrimos la conexión a la base de datos
                SqlCommand com = new SqlCommand("SP_ACTUALIZAR_USUARIO", con);//Definimos el procedimiento a ejecutar
                com.CommandType = CommandType.StoredProcedure;//Comando de tipo procedimiento almacenado

                //Agregamos y hacemos match los parametros definidos
                com.Parameters.Add("@usuario", SqlDbType.VarChar, 50).Value = usuarioModel.usuario;
                com.Parameters.Add("@contrasena", SqlDbType.VarChar, 50).Value = usuarioModel.contrasena;
                com.Parameters.Add("@intentos", SqlDbType.Int, 5).Value = usuarioModel.intentos;
                com.Parameters.Add(new SqlParameter("@nivelSeg", SqlDbType.Decimal)
                {
                    Precision = 18,
                    Scale = 0
                }).Value = usuarioModel.nivelSeg;
                com.Parameters.Add("@id", SqlDbType.Int, 5).Value = id;

                result = com.ExecuteNonQuery();//Ejecuto y comando y actulizo los datos
            }
            return result;// Captura la lista de la identidad
        }

            public static int DeleteUsers(int id)//Como parámetro de entrada pasamos el id
            {//Acceder al archivo de configuración para leer la cadena de conexión
                string cs = ConfigurationManager.ConnectionStrings["CnBD"].ConnectionString;

                int result;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();//Abrimos la conexión a la base de datos
                    SqlCommand com = new SqlCommand("SP_ELIMINAR_USUARIO", con);//Definimos el procedimiento a ejecutar
                    com.CommandType = CommandType.StoredProcedure;//Comando de tipo procedimiento almacenado

                    com.Parameters.Add("@id", SqlDbType.Int, 5).Value = id;//Agregamos y hacemos match el id en referencia al parametro de entrada

                    result = com.ExecuteNonQuery();//Ejecutar el comando y eliminar el registro
                }
                return result;
            }
    }
}