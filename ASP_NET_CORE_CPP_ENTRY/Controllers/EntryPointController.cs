using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Controllers;
using System.Runtime.InteropServices;

namespace ASP_NET_CORE_CPP_ENTRY.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntryPointController : ControllerBase
    {
        private readonly ILogger<EntryPointController> _logger;

        public EntryPointController(ILogger<EntryPointController> logger)
        {
            _logger = logger;
        }

        [NonAction]
        public string ApplicationVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("_GetAppVersion")]
        public string _GetAppVersion()
        {
            //--------------------------------------------------
            // DECLARACION DE VARIABLES
            //--------------------------------------------------
            string appVersion = "";

            try
            {
                appVersion = ApplicationVersion();
                //--------------------------------------------------
                // LOG
                //--------------------------------------------------
                //LogModel.Log(string.Format("app_version : {0}", appVersion));
            }
            catch (Exception ex)
            {
                /*
                LogModel.Log(string.Format("app_version. ERROR ='{0}'-'{1}'"
                                                      , ex.Message
                                                      , ex.StackTrace)
                             , string.Empty
                             , LogModel.LogType.Error
                             );*/
            }
            //
            return appVersion;
        }
    }
}
