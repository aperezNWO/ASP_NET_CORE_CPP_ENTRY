using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Controllers;
using System.Runtime.InteropServices;
using ASP_NET_CORE_CPP_ENTRY.Interop;

namespace ASP_NET_CORE_CPP_ENTRY.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AlgorithmController : ControllerBase
    {
        #region "FIELDS"
        private bool _dllLoaded = false;
        #endregion

        #region "CONSTRUCTOR"
        private readonly ILogger<AlgorithmController> _logger;

        public AlgorithmController(ILogger<AlgorithmController> logger)
        {

            try
            {

                this._logger = logger;
                this._dllLoaded = true;
                Console.WriteLine("✅ DLL loaded successfully");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"❌ DLL load failed: {ex.Message}");

            }
        }
        #endregion

        #region "METHODS"

        // GET DLL VERSION
        [Microsoft.AspNetCore.Mvc.HttpGet("GetAppVersion")]
        public string GetDLLVersion()
        {
            //
            string return_value_str = string.Empty;
            //
            try
            {
                IntPtr intptr = AlgorithmNative._GetDLLVersion();
                string unicodeString = Marshal.PtrToStringUTF8(intptr);
                return_value_str = unicodeString;
            }
            catch (Exception ex)
            {
                string msg = ex.Message + " " + ex.StackTrace;

                // LogModel.Log(msg);
            }
            return return_value_str;
        }

        // GET C++ STD VERSION
        [Microsoft.AspNetCore.Mvc.HttpGet("GetCPPSTDVersion")]
        public string GetCPPSTDVersion()
        {
            //
            string return_value_str = string.Empty;
            //
            try
            {
                IntPtr intptr = AlgorithmNative._GetCPPSTDVersion();
                string unicodeString = Marshal.PtrToStringUTF8(intptr);
                return_value_str = unicodeString;
            }
            catch (Exception ex)
            {
                string msg = ex.Message + " " + ex.StackTrace;

                // LogModel.Log(msg);
            }
            return return_value_str;
        }


        ////////////////////////////////////////////////////////////
        // DIAGNOSTICS
        ////////////////////////////////////////////////////////////

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok(new { dllLoaded = _dllLoaded });
        }

        // DIJKSTRA
        [Microsoft.AspNetCore.Mvc.HttpGet("GenerateRandomVertex_CPP")]
        public string GenerateRandomVertex_CPP(ushort p_vertexSize, ushort p_sourcePoint)
        {
            string return_value_str = string.Empty;

            try
            {
                IntPtr intptr         = AlgorithmNative._Dijkstra_GenerateRandomVertex_CPP(p_vertexSize, p_sourcePoint);
                string unicodeString  = Marshal.PtrToStringUTF8(intptr);
                unicodeString         =  unicodeString.Replace("~", "■");
                unicodeString         = unicodeString.Replace("=", "≡");

                return_value_str = unicodeString;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return return_value_str;
        }

        // SORTBENCHMARK - HTML
        [Microsoft.AspNetCore.Mvc.HttpGet("_GetSort_CPP")]
        public string _GetSort_CPP(ushort p_sortAlgoritm, string p_unsortedList = "")
        {
            //
            if (string.IsNullOrWhiteSpace(p_unsortedList))
            {
                _logger.LogWarning("p_unsortedList is null or empty.");
                return "ERROR: Invalid input list.";
            }

            //
            string status = "OK";
            //
            try
            {
                //
                IntPtr intptr           = AlgorithmNative.SortBenchMark_GetSort_CPP(p_sortAlgoritm, p_unsortedList);
                //string unicodeString  = Marshal.PtrToStringUTF8(intptr);
                string unicodeString    = Marshal.PtrToStringAnsi(intptr); // For ANSI encoding

                //
                unicodeString = unicodeString.Replace("~", "■");
                status = unicodeString;

                //Marshal.FreeHGlobal(intptr);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in _GetSort_CPP.");
                return "Internal Server Error";
            }
            //
            return status;
        }

        // SORTBENCHMARK - JSON
        [Microsoft.AspNetCore.Mvc.HttpGet("_GetSort_CPP_JSON")]
        public IActionResult _GetSort_CPP_JSON(ushort p_sortAlgoritm, string p_unsortedList = "")
        {
            if (string.IsNullOrWhiteSpace(p_unsortedList))
            {
                _logger.LogWarning("p_unsortedList is null or empty.");
                return Content("ERROR: Invalid input list.", "application/json");
            }

            try
            {
                // Call the C++ function
                IntPtr intptr     = AlgorithmNative.SortBenchMark_GetSort_CPP_JSON(p_sortAlgoritm, p_unsortedList);
                string jsonString = Marshal.PtrToStringAnsi(intptr); // Get the JSON string

                // Return the raw JSON string with the correct content type
                return Content(jsonString, "application/json");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in _GetSort_CPP_JSON.");
                return Content("Internal Server Error", "application/json");
            }
        }

        // REGEX
        [Microsoft.AspNetCore.Mvc.HttpGet("_RegExEval_CPP")]
        public string _RegExEval_CPP(string p_tagSearch, string p_textSearch)
        {
            //--------------------------------------------------
            // DECLARACION DE VARIABLES 
            //--------------------------------------------------
            string status = "OK";
            //
            try
            {
                //
                IntPtr intptr        = AlgorithmNative.RegExManager_RegExEval(p_tagSearch, p_textSearch);
                string unicodeString = Marshal.PtrToStringUTF8(intptr);
                //
                status = unicodeString;
            }
            catch (Exception ex)
            {
                /*
                LogModel.Log(string.Format("SORT_BENCHMARK_ERROR_CPP. ='{0}'-'{1}'"
                                                             , ex.Message
                                                             , ex.StackTrace)
                            , string.Empty
                            , LogModel.LogType.Error);*/
            }
            //
            return status;
        }

        // SUDOKU
        [Microsoft.AspNetCore.Mvc.HttpGet("Sudoku_Generate_CPP")]
        public string Sudoku_Generate_CPP()
        {
            //
            string return_value_str = string.Empty;
            //
            try
            {
                IntPtr intptr        = AlgorithmNative._Sudoku_Generate_CPP();
                string unicodeString = Marshal.PtrToStringUTF8(intptr);
                return_value_str     = unicodeString;
            }
            catch (Exception ex)
            {
                string msg = ex.Message + " " + ex.StackTrace;

                //LogModel.Log(msg);
            }
            return return_value_str;
        }

        // SUDOKU
        [Microsoft.AspNetCore.Mvc.HttpGet("Sudoku_Solve_CPP")]
        public string Sudoku_Solve_CPP(string p_matrix)
        {
            //
            string return_value_str = string.Empty;
            //
            try
            {
                IntPtr intptr        = AlgorithmNative._Sudoku_Solve_CPP(p_matrix);
                string unicodeString = Marshal.PtrToStringUTF8(intptr);
                return_value_str     = unicodeString;
            }
            catch (Exception ex)
            {
                string msg = ex.Message + " " + ex.StackTrace;

                // LogModel.Log(msg);
            }
            return return_value_str;
        }


        #endregion

    }
}
