using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Controllers;
using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Pruebas.Cliente.Interop
{
    public static class OcrNative
    {

        #region "TESSERACT"
        public const string dll_Tesseract                = "tesseract.dll";
        public const string fn_GetTesseractOcrOutput     = "GetTesseractOcrOutput";
        public const string fn_GetTesseractOcrOutputPath = "GetTesseractOcrOutputPath";
        public const string fn_GetTesseractVersion       = "GetTesseractVersion";
        public const string fn_GetTesseractAppVersion    = "GetTesseractAppVersion";
        public const string fn_GetTesseractCPPSTDVersion = "GetTesseract_CPPSTDVersion";

        //////////////////////////////////////////////////////////////
        /// COMMON FUNCTION
        //////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////
        /// GetTesseractVersion
        //////////////////////////////////////////////////////////////
        [DllImport(@"" + dll_Tesseract + "", EntryPoint = @"" + fn_GetTesseractVersion + "", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _GetTesseractVersion();

        //////////////////////////////////////////////////////////////
        /// GetTesseractAppVersion
        //////////////////////////////////////////////////////////////
        [DllImport(@"" + dll_Tesseract + "", EntryPoint = @"" + fn_GetTesseractAppVersion + "", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _GetTesseractAppVersion();


        //////////////////////////////////////////////////////////////
        /// GetTesseract_CPPSTDVersion
        //////////////////////////////////////////////////////////////
        [DllImport(@"" + dll_Tesseract + "", EntryPoint = @"" + fn_GetTesseractCPPSTDVersion + "", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _GetTesseractCPPSTDVersion();

   

        //////////////////////////////////////////////////////////////
        /// _GetTesseractOcrOutput
        //////////////////////////////////////////////////////////////
        [DllImport(@"" + dll_Tesseract + "", EntryPoint = @"" + fn_GetTesseractOcrOutput + "", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _GetTesseractOcrOutput();

   
        [DllImport(@"" + dll_Tesseract + "", EntryPoint = @"" + fn_GetTesseractOcrOutputPath + "", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _GetTesseractOcrOutputPath(string imagePath);

        #endregion

    }
}
