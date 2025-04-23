using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace mcsd.Web.Controllers
{

    [ApiController]
    [Route("/[controller]/")]

    public class BaseApiController : Controller
    {
        #region "Campos"
        //
        protected  IConfiguration        _configuration;
        protected  IWebHostEnvironment   _env;
        protected  IHttpContextAccessor  _httpContextAccessor;
        protected  readonly IMemoryCache _memoryCache;
        protected  HttpContext           _httpContext;
        protected  Object                _logModel;
        protected  string                _connString;
        #endregion

        #region "Metodos"
        [NonAction]
        public string GetIpValue()
        {
            var remoteIpAddress = HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;
            //
            return remoteIpAddress.ToString();
        }
        [NonAction]
        public string ApplicationVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        #endregion

        #region "Constructor"
        public BaseApiController( IConfiguration configuration
                                , IWebHostEnvironment env
                                , Microsoft.AspNetCore.Http.IHttpContextAccessor p_httpContextAccessor
                                , IMemoryCache memoryCache)
        {
            this._configuration       = configuration;
            this._connString          = _configuration.GetConnectionString("defaultConnection");
            this._logModel            = new object();
            this._env                 = env;
            this._httpContextAccessor = p_httpContextAccessor;
            this._httpContext         = this._httpContextAccessor.HttpContext;
            this._memoryCache         = memoryCache;
        }
        #endregion
     }
}