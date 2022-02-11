using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using RapidPayAPI.Services.PaymentFee;

[assembly: OwinStartup(typeof(RapidPayAPI.Startup))]

namespace RapidPayAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }      
    }
}
