using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using LSPOSWA.Models;

namespace LSPOSWA.Controllers
{
    [Authorize]
    public class MenuController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        RootModel[] menu = new RootModel[]
        {
            new RootModel { 
                id = "0", 
                text = "<i class=\"fa fa-shield\"></i>" + " Security", 
                iconCls = "", 
                parent_id = null,  
                className = null, 
                leaf = false, 
                items = new ItemsModel[] { 
                    new ItemsModel { id = "1", text = "<i class=\"fa fa-users\"></i> " + "Groups", iconCls = "", parent_id = "1",  className = "panel", leaf = true },
                    new ItemsModel { id = "2", text = "<i class=\"fa fa-user\"></i> " + "Users", iconCls = "", parent_id = "1",  className = "panel", leaf = true } 
                } 
            }
        };

        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        public IEnumerable<RootModel> Get()
        {
            string _user = Request.GetOwinContext().Authentication.User.Identity.Name;
            return menu;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
