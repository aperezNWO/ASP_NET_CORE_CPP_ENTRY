using ASP_NET_CORE_CPP_ENTRY.Interface;
using System.Runtime.InteropServices;

namespace ASP_NET_CORE_CPP_ENTRY.Interop
{
    public class TensorFlowNative /*: INative*/
    {
        public const string DLL_NAME = @"TensorFlowAppCPP.dll";

        // Import TensorFlow-specific methods
        [DllImport(DLL_NAME, EntryPoint = @"GetTensorFlowAPIVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetAPIVersion();

        [DllImport(DLL_NAME, EntryPoint = @"GetTensorFlowAppVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetAPPVersion();

        [DllImport(DLL_NAME, EntryPoint = @"GetCPPSTDVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetCPPSTDVersion();
    }
}
