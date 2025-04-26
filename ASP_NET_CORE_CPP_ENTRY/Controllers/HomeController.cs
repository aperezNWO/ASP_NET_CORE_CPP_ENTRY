using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Models;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;

namespace Pruebas.Cliente.Controllers
{
    /// <summary>
    /// Represents the structure of the incoming JSON payload.
    /// </summary>
    public class UploadRequest
    {
        public string Base64Image { get; set; }
    }
    public class HomeController : Controller
    {
        #region "DLL WRAPPER FUNCTIONS "

        #region "TESSERACT"
        const string dll_Tesseract                = "tesseract.dll";
        const string fn_GetTesseractOcrOutput     = "GetTesseractOcrOutput";
        const string fn_GetTesseractOcrOutputPath = "GetTesseractOcrOutputPath";
        const string fn_GetTesseractVersion       = "GetTesseractVersion";
        const string fn_GetTesseractAppVersion    = "GetTesseractAppVersion";

        //////////////////////////////////////////////////////////////
        /// COMMON FUNCTION
        //////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////
        /// GetTesseractVersion
        //////////////////////////////////////////////////////////////
        [DllImport(@"" + dll_Tesseract + "", EntryPoint = @"" + fn_GetTesseractVersion + "", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _GetTesseractVersion();

        [Microsoft.AspNetCore.Mvc.HttpGet(fn_GetTesseractVersion)]
        public string GetTesseractVersion()
        {
            string return_value_str = string.Empty;
            IntPtr intptr = IntPtr.Zero;

            try
            {
                // Call the external DLL function to get the result
                intptr = _GetTesseractVersion();

                // Convert the IntPtr to a string
                string unicodeString = Marshal.PtrToStringUTF8(intptr);

                // Assign the result to the return value
                return_value_str = unicodeString;


            }
            catch (Exception ex)
            {
                // Handle exceptions
                string msg = ex.Message + " " + ex.StackTrace;
                return_value_str = msg;
            }
            return return_value_str;
        }

        //////////////////////////////////////////////////////////////
        /// GetTesseractAppVersion
        //////////////////////////////////////////////////////////////
        [DllImport(@"" + dll_Tesseract + "", EntryPoint = @"" + fn_GetTesseractAppVersion + "", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _GetTesseractAppVersion();

        [Microsoft.AspNetCore.Mvc.HttpGet(fn_GetTesseractAppVersion)]
        public string GetTesseractAppVersion()
        {
            string return_value_str = string.Empty;
            IntPtr intptr           = IntPtr.Zero;

            try
            {
                // Call the external DLL function to get the result
                intptr = _GetTesseractAppVersion();

                // Convert the IntPtr to a string
                string unicodeString = Marshal.PtrToStringUTF8(intptr);

                // Assign the result to the return value
                return_value_str = unicodeString;


            }
            catch (Exception ex)
            {
                // Handle exceptions
                string msg = ex.Message + " " + ex.StackTrace;
                return_value_str = msg;
            }
            return return_value_str;
        }

  
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

        /// <summary>
        /// Handles the POST request for uploading base64-encoded images.
        /// </summary>
        /// <param name="request">The JSON payload containing the base64 image.</param>
        /// <returns>An HTTP response indicating success or failure.</returns>
        [DllImport(@"" + dll_Tesseract + "", EntryPoint = @"" + fn_GetTesseractOcrOutputPath + "", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _GetTesseractOcrOutputPath(string imagePath);

        [Microsoft.AspNetCore.Mvc.HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromBody] UploadRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Base64Image))
                {
                    return BadRequest("Invalid request: base64Image is required.");
                }

                // Extract file extension and base64 data using regex
                var regex = new Regex(@"^data:image\/([A-Za-z-+/]+);base64,(.+)$");
                var match = regex.Match(request.Base64Image);

                if (!match.Success)
                {
                    return BadRequest("Invalid base64 image format.");
                }

                var fileExtension = match.Groups[1].Value;
                var base64Data = match.Groups[2].Value;

                // Convert base64 string to byte array
                var imageBytes = Convert.FromBase64String(base64Data);

                // Generate a unique filename based on timestamp
                var fileName = $"image_{DateTime.Now:yyyyMMddHHmmss}.{fileExtension}";

                // Define the file path where the image will be saved
                var filePath = Path.Combine("img", "signatures", "dest", fileName);

                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // Write the byte array to a file
                await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                Console.WriteLine($"Image saved successfully: {filePath}");

                // Call the OCR function
                IntPtr intptr           = _GetTesseractOcrOutputPath(filePath);
                string unicodeString    = string.Format("Texto en imagen : {0}",Marshal.PtrToStringUTF8(intptr));

                return Ok(new { Message = unicodeString, FilePath = filePath });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing image: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        /// <summary>
        /// Simulates OCR processing for the uploaded image.
        /// </summary>
        /// <param name="imagePath">The path to the uploaded image.</param>
        private void DoOcr(string imagePath)
        {
            // Implement your OCR logic here
            Console.WriteLine($"Processing OCR for image: {imagePath}");
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
                IntPtr intptr        = _GetDLLVersion();
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

        #endregion

        #endregion


        #region "ROOT FUNCTIONS "

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        [NonAction]
        public string ApplicationVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("_GetAppVersion")]
        public string _GetAppVersion()
        {
            //--------------------------------------------------
            // DECLARACION DE VARIABLES
            //--------------------------------------------------
            string appVersion = "";

            try
            {
                appVersion = ApplicationVersion();
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