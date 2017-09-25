using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.Routing;

namespace url.historiskatlas.dk
{
    public class RouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext context)
        {
            return new MapsHandler(context.RouteData);
        }
    }

    public class MapsHandler : IHttpHandler
    {
        RouteData routeData;

        public MapsHandler(RouteData routeData)
        {
            this.routeData = routeData;
        }

        public void ProcessRequest(HttpContext context)
        {
            UInt32 i = BitConverter.ToUInt32(Convert.FromBase64String(routeData.Values["id"].ToString() + "=="), 0);

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["hadb"].ConnectionString))
            {
                conn.Open();
                string url = (string)new SqlCommand("SELECT url FROM ShortURL WHERE id = " + i, conn).ExecuteScalar();
                context.Response.Redirect(url, true);
            }
        }

        public bool IsReusable { get { return false; } }
    }

}