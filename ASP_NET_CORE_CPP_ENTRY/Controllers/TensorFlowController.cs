using ASP_NET_CORE_CPP_ENTRY.Interop;
using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Interop;
using System.Runtime.InteropServices;


namespace ASP_NET_CORE_CPP_ENTRY.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TensorFlowController : ControllerBase
    {
        #region "TENSORFLOW"

        // API VERSION
        [Microsoft.AspNetCore.Mvc.HttpGet("GetAPIVersion")]
        public string GetTensorflowAPIVersion()
        {
            //
            string return_value_str = string.Empty;
            //
            try
            {

                IntPtr intptr        = TensorFlowNative.GetAPIVersion();
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

        // APP VERSION
        [Microsoft.AspNetCore.Mvc.HttpGet("GetAppVersion")]
        public string GetTensorflowAPPVersion()
        {
            IntPtr ptr = TensorFlowNative.GetAPPVersion();
            if (ptr == IntPtr.Zero)
                return null;

            return Marshal.PtrToStringAnsi(ptr);
        }

        // C++ STD VERSION
        [Microsoft.AspNetCore.Mvc.HttpGet("GetCPPSTDVersion")]
        public string TensorFlow_GetCPPSTDVersion()
        {
            IntPtr ptr = TensorFlowNative.GetCPPSTDVersion();
            if (ptr == IntPtr.Zero)
                return null;

            return Marshal.PtrToStringAnsi(ptr);
        }

        #endregion
    }
}
