using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LEISIO.Controllers.API
{
    public class LoginController : ApiController
    {
        public HttpResponseMessage Post(Lib_Primavera.Model.Login login)
        {

            try
            {
                string pass = (string)Properties.Settings.Default[login.Nome];

                if (pass.Equals(login.Pass))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "login concluido");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "login falhou");
                }

            }
            catch (System.Configuration.SettingsPropertyNotFoundException)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "login falhou");
            }



        }
    }
    }
