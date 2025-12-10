using ASP_NET_CORE_CPP_ENTRY.Models;
using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Interop;
using System.Runtime.InteropServices;

namespace ASP_NET_CORE_CPP_ENTRY.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LinearRegressionController : ControllerBase
    {

        private static bool _dllLoaded = false;


        static LinearRegressionController()
        {
            try
            {

                _dllLoaded = true;
                Console.WriteLine("✅ DLL loaded successfully");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"❌ DLL load failed: {ex.Message}");

            }
        }

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok(new { dllLoaded = _dllLoaded });
        }

        [HttpGet("predict")]
        public IActionResult Predict(
            [FromQuery] double missionNumberToPredict)
        {

            double predictedTotalTime = LinearRegressionNative.TryPredict(missionNumberToPredict);

            if (predictedTotalTime == 0)
            {
                return StatusCode(500, new { error = "Failed to load dll." });
            }


            return Ok(new
            {
                predictedTotalTime
            });
        }
    }
}

