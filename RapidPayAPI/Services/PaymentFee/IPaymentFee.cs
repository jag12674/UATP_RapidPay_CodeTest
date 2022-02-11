using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RapidPayAPI.Services.PaymentFee
{
    public interface IPaymentFee
    {
        double GetFee();

        DateTime GetLastUpdateDatetime();
    }
}