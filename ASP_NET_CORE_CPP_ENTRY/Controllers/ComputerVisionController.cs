using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Controllers;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Pruebas.Cliente.Interop;

namespace ASP_NET_CORE_CPP_ENTRY.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComputerVisionController : ControllerBase
    {
        //
        private static bool _dllLoaded = false;

        //
        static ComputerVisionController()
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


        [Microsoft.AspNetCore.Mvc.HttpGet(ComputerVisionNative.endPoint_OpenCv)]
        public string _OpenCvReadImage()
        {
            string return_value_str = string.Empty;
            IntPtr intptr = IntPtr.Zero;

            try
            {
                // Call the external DLL function to get the result
                intptr = ComputerVisionNative.OpenCvReadImage();

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


        [Microsoft.AspNetCore.Mvc.HttpGet(ComputerVisionNative.CPPSTDVersion_OpenCv)]
        public string _OpenCv_GetCPPSTDVersion()
        {
            string return_value_str = string.Empty;
            IntPtr intptr = IntPtr.Zero;

            try
            {
                // Call the external DLL function to get the result
                intptr = ComputerVisionNative.OpenCv_GetCPPSTDVersion();

                // Convert the IntPtr to a string
                string unicodeString = Marshal.PtrToStringUTF8(intptr);

                // Assign the result to the return value
                return_value_str     = unicodeString;


            }
            catch (Exception ex)
            {
                // Handle exceptions
                string msg       = ex.Message + " " + ex.StackTrace;
                return_value_str = msg;
            }
            return return_value_str;
        }


        ////////////////////////////////////////////////////////////
        // OPENCV READ IMAGE (PATH)
        ////////////////////////////////////////////////////////////

        public string _OpenCvReadImagePath()
        {
            string return_value_str = string.Empty;
            IntPtr intptr = IntPtr.Zero;

            try
            {
                //
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image.png");
                // Call the external DLL function to get the result
                intptr = ComputerVisionNative.OpenCvReadImagePath(imagePath);

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

        ////////////////////////////////////////////////////////////
        // OPENCV UPLOAD FILE
        ////////////////////////////////////////////////////////////
        [Microsoft.AspNetCore.Mvc.HttpPost("UploadOpenCv")]
        public async Task<IActionResult> UploadOpenCv([FromBody] UploadRequest request)
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

                // Generate  a unique filename based on timestamp
                var fileName = $"image_{DateTime.Now:yyyyMMddHHmmss}.{fileExtension}";

                // Define the file path where the image will be saved
                var filePath = Path.Combine("img", "signatures", "dest", fileName);

                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                // Write the byte array to a file
                await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                Console.WriteLine($"Image saved successfully: {filePath}");

                // Call the OCR function
                IntPtr intptr        = ComputerVisionNative.OpenCvReadImagePath(filePath);
                string unicodeString = string.Format("Figura Detectada : {0}", Marshal.PtrToStringUTF8(intptr));

                return Ok(new { Message = unicodeString, FilePath = filePath });
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing image: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        ////////////////////////////////////////////////////////////
        // OPENCV GET APP VERSION
        ////////////////////////////////////////////////////////////


        [Microsoft.AspNetCore.Mvc.HttpGet("GetOpenCvAppVersion")]
        public string _GetOpenCvAppVersion()
        {
            string return_value_str = string.Empty;
            IntPtr intptr = IntPtr.Zero;

            try
            {
                // Call the external DLL function to get the result
                intptr = ComputerVisionNative.GetOpenCvAppVersion();

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

        ////////////////////////////////////////////////////////////
        // OPENCV GET API VERSION
        ////////////////////////////////////////////////////////////


        [Microsoft.AspNetCore.Mvc.HttpGet("GetOpenCvAPIVersion")]
        public string _GetOpenCvAPIVersion()
        {
            string return_value_str = string.Empty;
            IntPtr intptr = IntPtr.Zero;

            try
            {
                // Call the external DLL function to get the result
                intptr = ComputerVisionNative.GetOpenCvAPIVersion();

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

        ////////////////////////////////////////////////////////////
        // OPENCV GENERATE FRACTAL JULIA
        ////////////////////////////////////////////////////////////
        [DllImport(ComputerVisionNative.dll_OpenCv, CallingConvention = CallingConvention.StdCall)]
        private static extern int generateJulia();

        [HttpGet("generateJulia")]
        public IActionResult GenerateJuliaRandom()
        {
            // Call the C++ DLL function
            int result = generateJulia();
            if (result != 0)
            {
                return StatusCode(500, "Failed to generate Julia fractal.");
            }

            // Path to the generated image
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "julia.png");

            // Check if the image file exists
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound("Generated image not found.");
            }

            // Read the image file as a byte array
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

            // Return the image as a response with the appropriate content type
            return File(imageBytes, "image/jpeg");
        }

        ////////////////////////////////////////////////////////////
        // OPENCV GENERATE FRACTAL JULIA
        // http://localhost:83/generateJuliaParams?maxIterations=500&realPart=0.355&imagPart=0.355
        ////////////////////////////////////////////////////////////
        [DllImport(ComputerVisionNative.dll_OpenCv, CallingConvention = CallingConvention.StdCall)]
        private static extern int generateJuliaParams(int maxIterations, double realPart, double imagPart);

        [HttpGet("generateJuliaParams")]
        public IActionResult GenerateJuliaParams(int maxIterations, double realPart, double imagPart)
        {
            // Call the C++ DLL function
            int result = generateJuliaParams(maxIterations, realPart, imagPart);
            if (result != 0)
            {
                return StatusCode(500, "Failed to generate Julia fractal.");
            }

            // Path to the generated image
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "julia.png");

            // Check if the image file exists
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound("Generated image not found.");
            }

            // Read the image file as a byte array
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

            // Return the image as a response with the appropriate content type
            return File(imageBytes, "image/jpeg");
        }

    }
}
