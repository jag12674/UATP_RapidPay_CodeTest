using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RapidPayAPI.Services.PaymentFee
{
    public class PaymentFee : IPaymentFee
    {
        private const int interval = 60; //seconds

        private double fee;

        private double lastFeeAmount = 1; //seed fee

        private DateTime lastUpdateDatetime = DateTime.Now; //last update fee

        private static readonly PaymentFee _myPaymentFee = new PaymentFee();

        public static PaymentFee GetInstance() => _myPaymentFee;

        public PaymentFee()
        {
            CalculateFee();
        }
        public double GetFee()
        {
            DateTime now = DateTime.Now;

            if ((now - lastUpdateDatetime).TotalSeconds >= interval)
            {
                CalculateFee();

                lastUpdateDatetime = lastUpdateDatetime.AddSeconds(Math.Floor(((now - lastUpdateDatetime).TotalSeconds / interval)) * interval);
            }

            return fee;
        }
        private void CalculateFee()
        {
            Random random = new Random();
            var n = random.NextDouble() * 2;

            fee = n * lastFeeAmount;

            lastFeeAmount = fee;
        }

        public DateTime GetLastUpdateDatetime()
        {
            return lastUpdateDatetime;
        }
    }
}