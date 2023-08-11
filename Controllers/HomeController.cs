using AppCrud.Models;
using AppCrud.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AppCrud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Empleado> _empleadoRepository;

        public HomeController(ILogger<HomeController> logger,
            IGenericRepository<Empleado> empleadoRepository)
        {
            
            _logger = logger;
            _empleadoRepository = empleadoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }


        /*  [HttpGet]
          public async Task<IActionResult> listaDepartamentos()
          {
              List<Departamento> _lista = await_departamentoRepository.Lista();
              return StatusCode(StatusCodes.Status200OK._lista);
          }
        */
        [HttpGet]
        public async Task<IActionResult> listaEmpleado()
        {
            List<Empleado> _lista = await _empleadoRepository.List();

            return StatusCode(StatusCodes.Status200OK, _lista);
        }

        [HttpPost]
        public async Task<IActionResult> guardarEmpleado([FromBody] Empleado modelo)
        {
            bool _resultado = await _empleadoRepository.save(modelo);
            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "error" });
        }

        [HttpPut]
        public async Task<IActionResult> editarEmpleado([FromBody] Empleado modelo)
        {
            bool _resultado = await _empleadoRepository.Edit(modelo);
            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "error" });
        }

        [HttpDelete]
        public async Task<IActionResult> eliminarEmpleado(int idEmpleado)
        {
            bool _resultado = await _empleadoRepository.Delete(idEmpleado);
            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "error" });
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}