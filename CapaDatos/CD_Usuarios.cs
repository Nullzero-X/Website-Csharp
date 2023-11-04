using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;

namespace CapaDatos
{
	public class CD_Usuarios
	{
		public List<Usuario> Listar()
		{
			List<Usuario> lista = new List<Usuario>(); // Creamos una lista donde guardaremos usuarios.

			try
			{
				using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
				{
					string query = "select IdUsuario, Nombres, Apellidos, Correo, Clave, Reestablecer, Activo from USUARIO"; // Consulta SQL que obtendrá los datos de usuarios.

					SqlCommand cmd = new SqlCommand(query, oconexion); // Preparamos una consulta SQL utilizando la conexión.
					cmd.CommandType = CommandType.Text; // Le decimos que esta consulta es de tipo texto (SQL).

					oconexion.Open(); // Abrimos la conexión a la base de datos.

					using (SqlDataReader dr = cmd.ExecuteReader()) // Ejecutamos la consulta y obtenemos un lector de datos.
					{
						while (dr.Read()) // Mientras haya datos para leer en el lector de datos...
						{
							// Creamos un nuevo objeto de tipo Usuario y llenamos sus propiedades con los datos de la consulta.
							lista.Add(
								new Usuario()
								{
									IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
									Nombres = dr["Nombres"].ToString(),
									Apellidos = dr["Apellidos"].ToString(),
									Correo = dr["Correo"].ToString(),
									Clave = dr["Clave"].ToString(),
									Reestablecer = Convert.ToBoolean(dr["Reestablecer"]),
									Activo = Convert.ToBoolean(dr["Activo"])
								}
							);
						}
					}
				}
			}
			catch
			{
				lista = new List<Usuario>(); // En caso de algún error, creamos una lista vacía de usuarios.
			}

			return lista; // Devolvemos la lista de usuarios (puede estar vacía si hubo un error).
		}
	}
}
