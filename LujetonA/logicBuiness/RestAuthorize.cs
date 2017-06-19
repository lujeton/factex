using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace LujetonA.logicBuiness
{
    public class RestAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
    {
        var data = actionContext.Request.Headers.Authorization;

        var res = tokenProcessor.ValidateToken(data.Parameter,60 * 60);
        if (res != null)
        {
            return;
        }

        HandleUnauthorizedRequest(actionContext);
    }

    protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
    {
        actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.OK, "esto es un error");
        base.HandleUnauthorizedRequest(actionContext);
    }
}
}