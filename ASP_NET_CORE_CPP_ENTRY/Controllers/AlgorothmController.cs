using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Controllers;
using System.Runtime.InteropServices;

namespace ASP_NET_CORE_CPP_ENTRY.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AlgorithmController : ControllerBase
    {
        #region "CONSTRUCTOR"
        private readonly ILogger<AlgorithmController> _logger;

        public AlgorithmController(ILogger<AlgorithmController> logger)
        {
            _logger = logger;
        }
        #endregion 

        #region "ALGORITHM"


        // DIJKSTRA
        [DllImport(@"Algorithm.dll", EntryPoint = @"Dijkstra_GenerateRandomVertex_CPP", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _Dijkstra_GenerateRandomVertex_CPP(int p_vertexSize, int sourcePoint);

        [Microsoft.AspNetCore.Mvc.HttpGet("GenerateRandomVertex_CPP")]
        public string GenerateRandomVertex_CPP(ushort p_vertexSize, ushort p_sourcePoint)
        {
            string return_value_str = string.Empty;

            try
            {
                IntPtr intptr = _Dijkstra_GenerateRandomVertex_CPP(p_vertexSize, p_sourcePoint);
                string unicodeString = Marshal.PtrToStringUTF8(intptr);
                unicodeString = unicodeString.Replace("~", "■");
                unicodeString = unicodeString.Replace("=", "≡");

                return_value_str = unicodeString;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return return_value_str;
        }

        // SORTBENCHMARK - HTML
        [DllImport(@"Algorithm.dll", EntryPoint = @"SortBenchMark_GetSort_CPP", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SortBenchMark_GetSort_CPP(ushort p_sortAlgoritm, string p_unsortedList);
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
                IntPtr intptr = SortBenchMark_GetSort_CPP(p_sortAlgoritm, p_unsortedList);
                //string unicodeString  = Marshal.PtrToStringUTF8(intptr);
                string unicodeString = Marshal.PtrToStringAnsi(intptr); // For ANSI encoding

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

        [DllImport(@"Algorithm.dll", EntryPoint = @"SortBenchMark_GetSort_CPP_JSON", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SortBenchMark_GetSort_CPP_JSON(ushort p_sortAlgoritm, string p_unsortedList);

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
                IntPtr intptr = SortBenchMark_GetSort_CPP_JSON(p_sortAlgoritm, p_unsortedList);
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
        [DllImport(@"Algorithm.dll", EntryPoint = @"RegExManager_RegExEval", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr RegExManager_RegExEval(string p_tagSearch, string p_textSearch);
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
                IntPtr intptr = RegExManager_RegExEval(p_tagSearch, p_textSearch);
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
        [DllImport(@"Algorithm.dll", EntryPoint = @"Sudoku_Generate_CPP", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _Sudoku_Generate_CPP();
        [Microsoft.AspNetCore.Mvc.HttpGet("Sudoku_Generate_CPP")]
        public string Sudoku_Generate_CPP()
        {
            //
            string return_value_str = string.Empty;
            //
            try
            {
                IntPtr intptr = _Sudoku_Generate_CPP();
                string unicodeString = Marshal.PtrToStringUTF8(intptr);
                return_value_str = unicodeString;
            }
            catch (Exception ex)
            {
                string msg = ex.Message + " " + ex.StackTrace;

                //LogModel.Log(msg);
            }
            return return_value_str;
        }

        // SUDOKU
        [DllImport(@"Algorithm.dll", EntryPoint = @"Sudoku_Solve_CPP", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _Sudoku_Solve_CPP(string p_matrix);
        [Microsoft.AspNetCore.Mvc.HttpGet("Sudoku_Solve_CPP")]
        public string Sudoku_Solve_CPP(string p_matrix)
        {
            //
            string return_value_str = string.Empty;
            //
            try
            {
                IntPtr intptr = _Sudoku_Solve_CPP(p_matrix);
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

        // GET DLL VERSION
        [DllImport(@"Algorithm.dll", EntryPoint = @"GetDLLVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _GetDLLVersion();
        [Microsoft.AspNetCore.Mvc.HttpGet("GetDLLVersion")]
        public string GetDLLVersion()
        {
            //
            string return_value_str = string.Empty;
            //
            try
            {
                IntPtr intptr = _GetDLLVersion();
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
        [DllImport(@"Algorithm.dll", EntryPoint = @"GetCPPSTDVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _GetCPPSTDVersion();
        [Microsoft.AspNetCore.Mvc.HttpGet("Algorithm_GetCPPSTDVersion")]
        public string GetCPPSTDVersion()
        {
            //
            string return_value_str = string.Empty;
            //
            try
            {
                IntPtr intptr = _GetCPPSTDVersion();
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
        #endregion

        #region "TENSORFLOW"

        private const string tensorFlowDllName = @"TensorFlowAppCPP.dll";

        // API VERSION
        [DllImport(tensorFlowDllName, EntryPoint = @"GetTensorFlowAPIVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr _GetTensorflowAPIVersion();
        [Microsoft.AspNetCore.Mvc.HttpGet("GetTensorFlowAPIVersion")]
        public string GetTensorflowAPIVersion()
        {
            //
            string return_value_str = string.Empty;
            //
            try
            {

                IntPtr intptr = _GetTensorflowAPIVersion();
                string unicodeString = Marshal.PtrToStringUTF8(intptr);

                return_value_str = unicodeString;
            }
            catch (Exception ex)
            {
                string msg = ex.Message + " " + ex.StackTrace;

                //LogModel.Log(msg);
            }
            return return_value_str;
        }

        // APP VERSION
        [DllImport(tensorFlowDllName, EntryPoint = @"GetTensorFlowAppVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr _GetTensorFlowAppVersion();
        [Microsoft.AspNetCore.Mvc.HttpGet("GetTensorFlowAPPVersion")]
        public string GetTensorflowAPPVersion()
        {
            IntPtr ptr = _GetTensorFlowAppVersion();
            if (ptr == IntPtr.Zero)
                return null;

            return Marshal.PtrToStringAnsi(ptr);
        }

        // C++ STD VERSION
        [DllImport(tensorFlowDllName, EntryPoint = @"GetCPPSTDVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr _TensorFlow_GetCPPSTDVersion();
        [Microsoft.AspNetCore.Mvc.HttpGet("TensorFlow_GetCPPSTDVersion")]
        public string TensorFlow_GetCPPSTDVersion()
        {
            IntPtr ptr = _TensorFlow_GetCPPSTDVersion();
            if (ptr == IntPtr.Zero)
                return null;

            return Marshal.PtrToStringAnsi(ptr);
        }

        #endregion
    }
}
