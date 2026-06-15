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

            // Prepare the response data
            /*
            response_data = {
                'input_mission_number'       : mission_number,
                'predicted_total_time_hours' : predicted_time,
                'predicted_duration_days'    : predicted_time / 24.0
            }*/

            return Ok(new
            {
                input_mission_number        = missionNumberToPredict,
                predicted_total_time_hours  = predictedTotalTime,
                predicted_duration_days     = (predictedTotalTime / 24.0)
            });
        }
    }
}

