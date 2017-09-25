using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace url.historiskatlas.dk
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public string GetContent()
        {
            UInt32 i = UInt32.Parse(Request.QueryString["id"]);
            string b = Convert.ToBase64String(BitConverter.GetBytes(i)).Remove(6);
            return b + " - " + BitConverter.ToUInt32(Convert.FromBase64String(b + "=="), 0);
        }
    }
}