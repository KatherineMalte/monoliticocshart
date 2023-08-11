using AppCrud.Models;
using AppCrud.Repositories.Contract;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;

namespace AppCrud.Repositories.Implementation
{
    public class EmpleadoRepository : IGenericRepository<Empleado>
    {
        private readonly string _cadenaSQL = "";
        public EmpleadoRepository(IConfiguration configuracion)
        {
            _cadenaSQL = configuracion.GetConnectionString("cadenaSQL");
        }


        public async Task<List<Empleado>> List ()
        {
            List<Empleado> _lista = new List<Empleado>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("empleado_listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Empleado
                        {
                            id = Convert.ToInt32(dr["id"]),
                            Nombre = dr["nombre"].ToString(),
                            Apellido = dr["apellido"].ToString(),
                            Edad = Convert.ToInt32(dr["edad"]),
                            Cargo = dr["cargo"].ToString(),
                            Sueldo = Convert.ToInt32(dr["sueldo"]),
                        });
                    }

                }

            }
            return _lista;                                                                                                                                
        }

        /*
        public class EmpleadoRepository : IGenericRepository<Empleado>
    {
        private readonly string _cadenaSQL = "";
        public EmpleadoRepository(IConfiguration configuracion)
        {
            _cadenaSQL = configuracion.GetConnectionString("cadenaSQL");
        }

        public async Task<List<Empleado>> List()
        {
            List<Empleado> _lista = new List<Empleado>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("empleado_listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())

                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Empleado
                        {
                            id = Convert.ToInt32(dr["id"]),
                            Nombre = dr["nombre"].ToString(),
                            Apellido = dr["apellido"].ToString(),
                            Edad = Convert.ToInt32(dr["edad"]),
                            Cargo = dr["cargo"].ToString(),
                            Sueldo = Convert.ToInt32(dr["sueldo"]),
                           /* refDepartamento = new Departamento() {
                                idDepartamento = Convert.ToInt32(dr[idDepartamento]),
                                nombre = dr["nombre"].ToString()
                            },
                             
                        });
                    }
                }
            }
           
            return _lista;
        }
*/
        public async Task<bool> save(Empleado modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_GuardarEmpleado", conexion);
                cmd.Parameters.AddWithValue("Nombre", modelo.Nombre);
                cmd.Parameters.AddWithValue("Apellido", modelo.Apellido);
                cmd.Parameters.AddWithValue("Edad", modelo.Edad);
                cmd.Parameters.AddWithValue("Cargo", modelo.Cargo);
                cmd.Parameters.AddWithValue("Sueldo", modelo.Sueldo);
                cmd.CommandType = CommandType.StoredProcedure;
                int filas_afectadas = await cmd.ExecuteNonQueryAsync();
                if(filas_afectadas > 0)
                
                    return true;
                    else
                        return false;
                
            }
        }  
        public async Task<bool> Edit(Empleado modelo)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_editarEmpleado", conexion);
                cmd.Parameters.AddWithValue("Nombre", modelo.Nombre);
                cmd.Parameters.AddWithValue("Apellido", modelo.Apellido);
                cmd.Parameters.AddWithValue("Edad", modelo.Edad);
                cmd.Parameters.AddWithValue("Cargo", modelo.Cargo);
                cmd.Parameters.AddWithValue("Sueldo", modelo.Sueldo);
                cmd.CommandType = CommandType.StoredProcedure;
                int filas_afectadas = await cmd.ExecuteNonQueryAsync();
                if (filas_afectadas > 0)

                    return true;
                else
                    return false;

            }
        }
        public async Task<bool> Delete(int id)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_EliminarEmpleado", conexion);
                cmd.Parameters.AddWithValue("idEmpleado", id);
                cmd.CommandType = CommandType.StoredProcedure;
                int filas_afectadas = await cmd.ExecuteNonQueryAsync();
                if (filas_afectadas > 0)

                    return true;
                else
                    return false;

            }
        }
    }
}
