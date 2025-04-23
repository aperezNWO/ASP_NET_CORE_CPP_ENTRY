using mcsd.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Pruebas.Cliente.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Web;

namespace Pruebas.Cliente.Controllers
{
    public class HomeController : BaseApiController
    {
        #region "DLL WRAPPER FUNCTIONS "

        #region "TESSERACT"
        const string dll_Tesseract                    = "tesseract.dll";
        const string fn_GetTesseractOcrOutput         = "GetTesseractOcrOutput";
        const string fn_GetTesseractVersion           = "GetTesseractVersion";
     
        //////////////////////////////////////////////////////////////
        /// COMMON FUNCTION
        //////////////////////////////////////////////////////////////
   
        //////////////////////////////////////////////////////////////
        /// _GetTesseractOcrOutput
        //////////////////////////////////////////////////////////////
        [DllImport(@"" + dll_Tesseract + "", EntryPoint = @"" + fn_GetTesseractOcrOutput + "", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _GetTesseractOcrOutput();

        [Microsoft.AspNetCore.Mvc.HttpGet(fn_GetTesseractOcrOutput)]
        public string GetTesseractOcrOutput()
        {
            //
            string return_value_str = string.Empty;
            //
            try
            {

                IntPtr intptr        = _GetTesseractOcrOutput();
                string unicodeString = Marshal.PtrToStringUTF8(intptr);

                return_value_str     = unicodeString;
            }
            catch (Exception ex)
            {
                string msg = ex.Message + " " + ex.StackTrace;

                return_value_str = msg;
            }
            return return_value_str;
        }

        
        //////////////////////////////////////////////////////////////
        /// GetTesseractVersion
        //////////////////////////////////////////////////////////////
        [DllImport(@"" + dll_Tesseract + "", EntryPoint = @"" + fn_GetTesseractVersion + "", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _GetTesseractVersion();

        [Microsoft.AspNetCore.Mvc.HttpGet(fn_GetTesseractVersion)]
        public string GetTesseractVersion()
        {
            string return_value_str = string.Empty;
            IntPtr intptr           = IntPtr.Zero;

            try
            {
                // Call the external DLL function to get the result
                intptr               = _GetTesseractVersion();

                // Convert the IntPtr to a string
                string unicodeString = Marshal.PtrToStringUTF8(intptr);

                // Assign the result to the return value
                return_value_str     = unicodeString;


            }
            catch (Exception ex)
            {
                // Handle exceptions
                string msg = ex.Message + " " + ex.StackTrace;
                return_value_str = msg;
            }
            return return_value_str;
        }

        #endregion

        #region "TENSORFLOW"

        #endregion

        #region "ALGORITHM"
        // CallingConvention.Cdecl = x86 DLL
        #region "DLL Import"

        // DIJKSTRA
        [DllImport(@"Algorithm.dll", EntryPoint = @"Dijkstra_GenerateRandomVertex_CPP", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _Dijkstra_GenerateRandomVertex_CPP(int p_vertexSize, int sourcePoint);
        [Microsoft.AspNetCore.Mvc.HttpGet("GenerateRandomVertex_CPP")]
        public string GenerateRandomVertex_CPP(ushort p_vertexSize, ushort p_sourcePoint)
        {
            //
            string return_value_str = string.Empty;
            //
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
                string msg = ex.Message + " " + ex.StackTrace;

                //LogModel.Log(msg);
            }
            return return_value_str;
        }

        // SORTBENCHMARK
        [DllImport(@"Algorithm.dll", EntryPoint = @"SortBenchMark_GetSort_CPP", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SortBenchMark_GetSort_CPP(ushort p_sortAlgoritm, string p_unsortedList);
        [Microsoft.AspNetCore.Mvc.HttpGet("_GetSort_CPP")]
        public string _GetSort_CPP(ushort p_sortAlgoritm, string p_unsortedList = "")
        {
            //--------------------------------------------------
            // DECLARACION DE VARIABLES 
            //--------------------------------------------------
            string status = "OK";
            //
            try
            {
                //
                IntPtr intptr = SortBenchMark_GetSort_CPP(p_sortAlgoritm, p_unsortedList);
                string unicodeString = Marshal.PtrToStringUTF8(intptr);
                //
                unicodeString = unicodeString.Replace("~", "■");
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
        #endregion

        #endregion

        #endregion

        #region "ROOT FUNCTIONS "

        public HomeController(IConfiguration configuration
                                       , IWebHostEnvironment env
                                       , IHttpContextAccessor p_httpContextAccessor
                                       , IMemoryCache memoryCache)
                   : base(configuration
                           , env
                           , p_httpContextAccessor
                           , memoryCache
                   )
        {
            //
        }

        public IActionResult Index()
        {
            return View();
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

        //
        [Microsoft.AspNetCore.Mvc.HttpGet("_GetAppVersion")]
        public string _GetAppVersion()
        {
            //--------------------------------------------------
            // DECLARACION DE VARIABLES
            //--------------------------------------------------
            string appVersion = "";

            try
            {
                appVersion = base.ApplicationVersion();
                //--------------------------------------------------
                // LOG
                //--------------------------------------------------
                //LogModel.Log(string.Format("app_version : {0}", appVersion));
            }
            catch (Exception ex)
            {
                /*
                LogModel.Log(string.Format("app_version. ERROR ='{0}'-'{1}'"
                                                      , ex.Message
                                                      , ex.StackTrace)
                             , string.Empty
                             , LogModel.LogType.Error
                             );*/
            }
            //
            return appVersion;
        }
        #endregion 
    }
}