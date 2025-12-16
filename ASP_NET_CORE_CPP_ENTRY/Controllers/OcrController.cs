using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Controllers;
using Pruebas.Cliente.Interop;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ASP_NET_CORE_CPP_ENTRY.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OcrController : ControllerBase
    {

        private static bool _dllLoaded = false;

        static OcrController()
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

        [Microsoft.AspNetCore.Mvc.HttpGet(OcrNative.fn_GetTesseractVersion)]
        public string GetTesseractVersion()
        {
            string return_value_str = string.Empty;
            IntPtr intptr = IntPtr.Zero;

            try
            {
                // Call the external DLL function to get the result
                intptr               = OcrNative._GetTesseractVersion();

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

        [Microsoft.AspNetCore.Mvc.HttpGet(OcrNative.fn_GetTesseractAppVersion)]
        public string GetTesseractAppVersion()
        {
            string return_value_str = string.Empty;
            IntPtr intptr = IntPtr.Zero;

            try
            {
                // Call the external DLL function to get the result
                intptr = OcrNative._GetTesseractAppVersion();

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

        [Microsoft.AspNetCore.Mvc.HttpGet(OcrNative.fn_GetTesseractCPPSTDVersion)]
        public string GetTesseractCPPSTDVersion()
        {
            string return_value_str = string.Empty;
            IntPtr intptr = IntPtr.Zero;

            try
            {
                // Call the external DLL function to get the result
                intptr = OcrNative._GetTesseractCPPSTDVersion();

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

        [Microsoft.AspNetCore.Mvc.HttpGet(OcrNative.fn_GetTesseractOcrOutput)]
        public string GetTesseractOcrOutput()
        {
            //
            string return_value_str = string.Empty;
            //
            try
            {

                IntPtr intptr        = OcrNative._GetTesseractOcrOutput();
                string unicodeString = Marshal.PtrToStringUTF8(intptr);

                return_value_str = unicodeString;
            }
            catch (Exception ex)
            {
                string msg = ex.Message + " " + ex.StackTrace;

                return_value_str = msg;
            }
            return return_value_str;
        }

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
                IntPtr intptr        = OcrNative._GetTesseractOcrOutputPath(filePath);
                string unicodeString = string.Format("text from image : {0}", Marshal.PtrToStringUTF8(intptr));

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

    }
}
