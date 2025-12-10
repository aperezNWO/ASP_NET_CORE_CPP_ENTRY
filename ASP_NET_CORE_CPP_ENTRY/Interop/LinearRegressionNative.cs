using System;
using System.Runtime.InteropServices;

namespace Pruebas.Cliente.Interop
{
    public static class LinearRegressionNative
    {
        private const string DllName = @"TensorFlowAppCPP.dll";

        [DllImport(DllName, EntryPoint = "Predict", CallingConvention = CallingConvention.Cdecl)]
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
                Console.WriteLine($"❌ DLL '{DllName}' not found.");

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
