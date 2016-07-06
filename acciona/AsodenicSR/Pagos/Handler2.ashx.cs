using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Acciona
{
    /// <summary>
    /// Descripción breve de Handler1
    /// </summary>
    
    public class Handler2 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cache.SetNoStore();
            context.Response.ContentType = "application/x-javascript";
            context.Response.Write("//");

        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}