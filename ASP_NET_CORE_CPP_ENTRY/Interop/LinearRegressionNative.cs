using ASP_NET_CORE_CPP_ENTRY.Interop;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Runtime.InteropServices;

namespace Pruebas.Cliente.Interop
{
    public class LinearRegressionNative : TensorFlowNative
    {

        [DllImport(DLL_NAME, EntryPoint = "Predict", CallingConvention = CallingConvention.Cdecl)]
        private static extern double Predict( double missionNumberToPredict);

        public static double TryPredict(double missionNumberToPredic)
        {
            //
            try
            {
                return Predict( missionNumberToPredic );
            }
            catch (DllNotFoundException)
            {
                Console.WriteLine($"❌ DLL '{DLL_NAME}' not found.");

                return 0; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");

                return 0;
            }
        }
    }
}
