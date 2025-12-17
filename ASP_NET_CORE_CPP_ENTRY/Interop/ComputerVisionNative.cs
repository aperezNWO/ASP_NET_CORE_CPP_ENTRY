using System;
using System.Runtime.InteropServices;

namespace Pruebas.Cliente.Interop
{
    public static class ComputerVisionNative
    {
        //
        public const string dll_OpenCv           = "OpenCvDll.dll";
        public const string endPoint_OpenCv      = "OpenCvReadImage";
        public const string CPPSTDVersion_OpenCv = "OpenCv_GetCPPSTDVersion";

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
    }
}
