using System;
using System.Runtime.InteropServices;

namespace Pruebas.Cliente.Interop
{
    public static class ComputerVisionNative /* : INative */
    {
        #region "FIELDS"
        //
        public const string dll_OpenCv           = "OpenCvDll.dll";
        public const string endPoint_OpenCv      = "OpenCvReadImage";
        #endregion

        #region "METHODS"
        // READ IMAGE
        [DllImport(dll_OpenCv, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr OpenCvReadImage();

        // C++ STD VERSION
        [DllImport(dll_OpenCv, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr OpenCv_GetCPPSTDVersion();

        // READ IMAGE
        [DllImport(dll_OpenCv, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr OpenCvReadImagePath(string path);

        // APP VERSION
        [DllImport(dll_OpenCv, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetOpenCvAppVersion();
            
        // API VERSION
        [DllImport(dll_OpenCv, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetOpenCvAPIVersion();
        #endregion
    }
}
