using System.Runtime.InteropServices;

namespace ASP_NET_CORE_CPP_ENTRY.Interop
{
    public class TensorFlowNative
    {
        #region "FIELDS"
        public const string tensorFlowDllName = @"TensorFlowAppCPP.dll";
        #endregion

        #region "TENSORFLOW"

        // API VERSION
        [DllImport(tensorFlowDllName, EntryPoint = @"GetTensorFlowAPIVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr _GetTensorflowAPIVersion();

        // APP VERSION
        [DllImport(tensorFlowDllName, EntryPoint = @"GetTensorFlowAppVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr _GetTensorFlowAppVersion();

        // C++ STD VERSION
        [DllImport(tensorFlowDllName, EntryPoint = @"GetCPPSTDVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr _TensorFlow_GetCPPSTDVersion();

        #endregion
    }
}
