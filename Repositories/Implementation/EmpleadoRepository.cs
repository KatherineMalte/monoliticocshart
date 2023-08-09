using AppCrud.Models;
using AppCrud.Repositories.Contract;
using System.Data.SqlClient;
namespace AppCrud.Repositories.Implementation
{
    public class EmpleadoRepository : IGenericRepository<Empleado>
    {
        private readonly string _cadenaSQL = "";
        
        public EmpleadoRepository(IConfiguration configuracion)
        {
            _cadenaSQL= configuracion.GetConnectionString("CadenaSQL");
        }
        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Edit(Empleado modelo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Empleado>> List()
        {
            List<Empleado> _lista = new List<Empleado>();
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("dbo.empleado_listar", conexion);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                using (var dr = await cmd.ExecuteReaderAsync()) {
                    while (await dr.ReadAsync()) {
                        _lista.Add(new Empleado
                        {
                            id = Convert.ToInt32(dr["id"]),
                            Nombre = dr["nombre"].ToString(),
                            Apellido = dr["apellido"].ToString(),
                            Cargo = dr["cargo"].ToString(),
                            Edad = dr["edad"].ToString(),
                            Sueldo = dr["sueldo"].ToString()
                           /* refDepartamento = new Departamento() {
                                idDepartamento = Convert.ToInt32(dr[idDepartamento]),
                                nombre = dr["nombre"].ToString()
                            },*/
                            
                        });  
                    }
                }
            }
            return _lista;
        }

        public Task<bool> save(Empleado modelo)
        {
            throw new NotImplementedException();
        }
    }
}
