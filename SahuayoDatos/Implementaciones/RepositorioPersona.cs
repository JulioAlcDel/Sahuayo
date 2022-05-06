using SahuayoDatos.Entidades;
using SahuayoDatos.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SahuayoDatos.Implementaciones
{
    public class RepositorioPersona : IPersonaRepositorio
    {
        private readonly string _cadenaConexion;
        public RepositorioPersona(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }
        public async Task<bool> Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarPersona",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdPersona", id);

                try
                {
                    await con.OpenAsync();
                    cmd.ExecuteNonQuery();
                    con.Close();
                   
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public async Task<List<Persona>> GetListaPersona()
        {

            List<Persona> listPersona = new List<Persona>();
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("sp_ConsultarPersona",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    SqlDataReader sql = await cmd.ExecuteReaderAsync();
                    while (sql.Read())
                    {
                        listPersona.Add(new Persona()
                        {
                            IdPersona = Convert.ToInt32(sql["IdPersona"]),
                            Nombre = sql["Nombre"].ToString(),
                            ApellidoMaterno = sql["ApellidoMaterno"].ToString(),
                            ApellidoPaterno = sql["ApellidoPaterno"].ToString(),
                            Descripcion = sql["Descripcion"].ToString(),
                            Sueldo = decimal.Parse(sql["Sueldo"].ToString()),
                            FechaRegistro = DateTime.Parse(sql["FechaRegistro"].ToString()),
                            TieneEnfermedad = bool.Parse(sql["TieneEnfermedad"].ToString())

                        });

                    }
                    con.Close();

                }
                catch (Exception e)
                {
                    con.Close();
                    return listPersona;
                }
                return listPersona;
            }
        }
        public async Task<Persona> GetPersonaId(int id)
        {
            Persona persona = new Persona();
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("sp_ConsultarPersona",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdPersona", id);

                try
                {
                    await con.OpenAsync();
                    SqlDataReader sql = await cmd.ExecuteReaderAsync();
                    while (sql.Read())
                    {
                        persona.IdPersona = Convert.ToInt32(sql["IdPersona"]);
                        persona.Nombre = sql["Nombre"].ToString() ;
                        persona.ApellidoMaterno = sql["ApellidoMaterno"].ToString();
                        persona.ApellidoPaterno = sql["ApellidoPaterno"].ToString();
                        persona.Descripcion = sql["Descripcion"].ToString();
                        persona.Sueldo = decimal.Parse(sql["Sueldo"].ToString());
                        persona.FechaRegistro = DateTime.Parse(sql["FechaRegistro"].ToString());
                        persona.TieneEnfermedad = bool.Parse(sql["TieneEnfermedad"].ToString());
                    }
                    con.Close();

                }
                catch (Exception)
                {
                    con.Close();
                    return persona;
                }
                return persona;
            }
        }

        public async Task<bool> Grabar(Persona entity)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion) ) {
                SqlCommand cmd = new SqlCommand("sp_AgregarPersona", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@IdPersona", entity.IdPersona);
                cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
                cmd.Parameters.AddWithValue("@ApellidoMaterno", entity.ApellidoMaterno);
                cmd.Parameters.AddWithValue("@ApellidoPaterno", entity.ApellidoMaterno);
                cmd.Parameters.AddWithValue("@TieneEnfermedad", entity.TieneEnfermedad);
                cmd.Parameters.AddWithValue("@Descripcion", entity.Descripcion);
                cmd.Parameters.AddWithValue("@Sueldo", entity.Sueldo.ToString());

                try
                {
                    await con.OpenAsync();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}
