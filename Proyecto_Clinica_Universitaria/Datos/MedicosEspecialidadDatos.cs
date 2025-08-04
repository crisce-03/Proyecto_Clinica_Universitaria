using Proyecto_Clinica_Universitaria.Models;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_Clinica_Universitaria.Datos
{
    public class MedicosEspecialidadDatos
    {

        public bool Guardar(MedicosEspecialidadModel especialidadmedico)
        {
            bool result;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();

                    SqlCommand cmd = new SqlCommand("sp_GuardarMedicosEspecialidad", conexion);
                    cmd.Parameters.AddWithValue("@Especialidad", especialidadmedico.Especialidad ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Descripcion", especialidadmedico.Descripcion ?? (object)DBNull.Value);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();
                }

                result = true;
            }
            catch (Exception ex)
            {

                result = false;
            }

            return result;
        }

        

    }
}
