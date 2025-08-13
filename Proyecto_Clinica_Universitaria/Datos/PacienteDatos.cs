using Proyecto_Clinica_Universitaria.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Proyecto_Clinica_Universitaria.Datos
{
    public class PacienteDatos
    {
        public List<PacienteModel> ListarNombres()
        {
            var lista = new List<PacienteModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("SELECT Codigo, Nombre FROM Paciente", conexion);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new PacienteModel
                        {
                            Codigo = dr.GetInt32(0),
                            Nombre = dr.GetString(1)
                        });
                    }
                }
            }

            return lista;
        }
    }
}

