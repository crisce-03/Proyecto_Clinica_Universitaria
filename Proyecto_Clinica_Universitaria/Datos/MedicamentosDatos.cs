using System.Data;
using System.Data.SqlClient;
using Proyecto_Clinica_Universitaria.Models;

namespace Proyecto_Clinica_Universitaria.Datos
{
    public class MedicamentosDatos
    {
        public List<MedicamentoModel> Listar()
        {
            var lista = new List<MedicamentoModel>();
            var cn = new Conexion();

            using var conexion = new SqlConnection(cn.getCadenaSQL());
            conexion.Open();
            using var cmd = new SqlCommand("sp_ListarMedicamentos", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new MedicamentoModel
                {
                    Codigo = Convert.ToInt32(dr["Codigo"]),
                    Medicamento = dr["Medicamento"]?.ToString() ?? string.Empty,
                    Descripcion = dr["Descripcion"] as string,
                    Cantidad = Convert.ToInt32(dr["Cantidad"]),
                    Vencimiento = dr["Vencimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["Vencimiento"]),
                    Imagen = dr["Imagen"] as string
                });
            }
            return lista;
        }

        public MedicamentoModel? Obtener(int codigo)
        {
            MedicamentoModel? obj = null;
            var cn = new Conexion();

            using var conexion = new SqlConnection(cn.getCadenaSQL());
            conexion.Open();
            using var cmd = new SqlCommand("sp_ObtenerMedicamento", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Codigo", codigo);

            using var dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                obj = new MedicamentoModel
                {
                    Codigo = Convert.ToInt32(dr["Codigo"]),
                    Medicamento = dr["Medicamento"]?.ToString() ?? string.Empty,
                    Descripcion = dr["Descripcion"] as string,
                    Cantidad = Convert.ToInt32(dr["Cantidad"]),
                    Vencimiento = dr["Vencimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["Vencimiento"]),
                    Imagen = dr["Imagen"] as string
                };
            }
            return obj;
        }

        public bool Guardar(MedicamentoModel obj)
        {
            var cn = new Conexion();
            try
            {
                using var conexion = new SqlConnection(cn.getCadenaSQL());
                conexion.Open();
                using var cmd = new SqlCommand("sp_GuardarMedicamentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Medicamento", obj.Medicamento);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)obj.Descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cantidad", obj.Cantidad);
                cmd.Parameters.AddWithValue("@Vencimiento", (object?)obj.Vencimiento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Imagen", (object?)obj.Imagen ?? DBNull.Value);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new Exception("Dato duplicado en Medicamentos.", ex);
            }
        }

        public bool Editar(MedicamentoModel obj)
        {
            var cn = new Conexion();
            try
            {
                using var conexion = new SqlConnection(cn.getCadenaSQL());
                conexion.Open();
                using var cmd = new SqlCommand("sp_EditarMedicamentos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Codigo", obj.Codigo);
                cmd.Parameters.AddWithValue("@Medicamento", obj.Medicamento);
                cmd.Parameters.AddWithValue("@Descripcion", (object?)obj.Descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cantidad", obj.Cantidad);
                cmd.Parameters.AddWithValue("@Vencimiento", (object?)obj.Vencimiento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Imagen", (object?)obj.Imagen ?? DBNull.Value);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                throw new Exception("Dato duplicado en Medicamentos.", ex);
            }
        }

        public bool Eliminar(int codigo)
        {
            var cn = new Conexion();
            using var conexion = new SqlConnection(cn.getCadenaSQL());
            conexion.Open();
            using var cmd = new SqlCommand("sp_EliminarMedicamento", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Codigo", codigo);
            cmd.ExecuteNonQuery();
            return true;
        }
    }
}
