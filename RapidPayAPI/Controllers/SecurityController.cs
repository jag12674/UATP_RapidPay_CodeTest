using RapidPayAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RapidPayAPI.Controllers
{
    [RoutePrefix("api/Security")]
    public class SecurityController : ApiController
    {
        [HttpGet]
        [Route("Encrypt")]
        public string Encrypt(string value)
        {
            return UtilityModel.CreateSHA512(UtilityModel.CreateMD5(value));
        }
    }
}