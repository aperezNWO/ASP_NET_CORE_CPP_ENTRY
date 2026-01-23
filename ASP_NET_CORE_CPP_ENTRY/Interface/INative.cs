using System.Runtime.InteropServices;
namespace ASP_NET_CORE_CPP_ENTRY.Interface
{
    public interface INative
    {
        // Abstract property for DLL_NAME
        // Static abstract property for DLL_NAME
        public extern string DLL_NAME { get; }

        // Abstract methods to enforce implementation
        public extern IntPtr GetAPIVersion();
        public extern IntPtr GetAPPVersion();
        public extern IntPtr GetCPPSTDVersion();
    }
}
