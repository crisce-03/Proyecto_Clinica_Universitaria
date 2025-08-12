using Proyecto_Clinica_Universitaria.Models;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto_Clinica_Universitaria.Datos
{
    public class MedicoDatos
    {
        public List<MedicoModel> Listar()
        {
            var lista = new List<MedicoModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ListarMedico", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new MedicoModel
                            {
                                Codigo = Convert.ToInt32(dr["Codigo"]),
                                Cedula = dr["Cedula"].ToString() ?? string.Empty,
                                Nombre = dr["Nombre"].ToString() ?? string.Empty,
                                Apellido = dr["Apellido"].ToString() ?? string.Empty,
                                EspecialidadCodigo = Convert.ToInt32(dr["EspecialidadCodigo"]),
                                Telefono = dr["Telefono"] == DBNull.Value ? null : dr["Telefono"]!.ToString(),
                                Correo = dr["Correo"] == DBNull.Value ? null : dr["Correo"]!.ToString(),
                                Usuario = dr["Usuario"] == DBNull.Value ? null : dr["Usuario"]!.ToString(),
                                Contrasena = dr["Contrasena"] == DBNull.Value ? null : dr["Contrasena"]!.ToString(),
                                Estado = dr["Estado"].ToString() ?? "Activo"
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public MedicoModel? Obtener(int codigo)
        {
            MedicoModel? obj = null;
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerMedico", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Codigo", codigo);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            obj = new MedicoModel
                            {
                                Codigo = Convert.ToInt32(dr["Codigo"]),
                                Cedula = dr["Cedula"].ToString() ?? string.Empty,
                                Nombre = dr["Nombre"].ToString() ?? string.Empty,
                                Apellido = dr["Apellido"].ToString() ?? string.Empty,
                                EspecialidadCodigo = Convert.ToInt32(dr["EspecialidadCodigo"]),
                                Telefono = dr["Telefono"] == DBNull.Value ? null : dr["Telefono"]!.ToString(),
                                Correo = dr["Correo"] == DBNull.Value ? null : dr["Correo"]!.ToString(),
                                Usuario = dr["Usuario"] == DBNull.Value ? null : dr["Usuario"]!.ToString(),
                                Contrasena = dr["Contrasena"] == DBNull.Value ? null : dr["Contrasena"]!.ToString(),
                                Estado = dr["Estado"].ToString() ?? "Activo"
                            };
                        }
                    }
                }
            }

            return obj;
        }

        public bool Guardar(MedicoModel obj)
        {
            bool result;
            var cn = new Conexion();

            try
            {
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GuardarMedico", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Cedula", obj.Cedula);
                        cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", obj.Apellido);
                        cmd.Parameters.AddWithValue("@EspecialidadCodigo", obj.EspecialidadCodigo);
                        cmd.Parameters.AddWithValue("@Telefono", (object?)obj.Telefono ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Correo", (object?)obj.Correo ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Usuario", (object?)obj.Usuario ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Contrasena", (object?)obj.Contrasena ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                        cmd.ExecuteNonQuery();
                    }
                }
                result = true;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                // Violación de UNIQUE / clave duplicada
                throw new Exception("La Cédula o el Usuario ya existen. Usa valores distintos.", ex);
            }
            catch (Exception ex)
            {
                throw; // deja que el controlador lo maneje
            }

            return result;
        }

        public bool Editar(MedicoModel obj)
        {
            bool result;
            var cn = new Conexion();

            try
            {
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_EditarMedico", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Codigo", obj.Codigo);
                        cmd.Parameters.AddWithValue("@Cedula", obj.Cedula);
                        cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", obj.Apellido);
                        cmd.Parameters.AddWithValue("@EspecialidadCodigo", obj.EspecialidadCodigo);
                        cmd.Parameters.AddWithValue("@Telefono", (object?)obj.Telefono ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Correo", (object?)obj.Correo ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Usuario", (object?)obj.Usuario ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Contrasena", (object?)obj.Contrasena ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Estado", obj.Estado);

                        cmd.ExecuteNonQuery();
                    }
                }
                result = true;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                // Violación de UNIQUE / clave duplicada
                throw new Exception("La Cédula o el Usuario ya existen. Usa valores distintos.", ex);
            }
            catch (Exception ex)
            {
                throw; // deja que el controlador lo maneje
            }

            return result;
        }

        public bool Eliminar(int codigo)
        {
            bool result;
            var cn = new Conexion();

            try
            {
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_EliminarMedico", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Codigo", codigo);
                        cmd.ExecuteNonQuery();
                    }
                }
                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
