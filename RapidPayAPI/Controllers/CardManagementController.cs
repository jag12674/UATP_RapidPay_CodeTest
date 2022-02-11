using RapidPay.Data;
using RapidPayAPI.Services.CardManagement;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using RapidPayAPI.Services.PaymentFee;
using RapidPayAPI.Models;

namespace RapidPayAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/CardManagement")]
    public class CardManagementController : ApiController
    {
        readonly IPaymentFee _paymentFee = PaymentFee.GetInstance();

        readonly ICardManagementService _service = new CardManagementService();

        [HttpPost]
        [Route("CreateCard")]
        public async Task<HttpResponseMessage> CreateCard(string CardNumber, decimal CreditLimit)
        {
            try
            {
                var response = await _service.CreateCard(CardNumber, CreditLimit);
                if (response != "OK")
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new ResultModel() { Result = $"Create card method failed. {response}" });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ResultModel() { Result = "Card added sucessfully." });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ResultModel() { Result = $"There was a problem with the request. {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("GetFee")]
        public async Task<HttpResponseMessage> GetFee()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ResultModel() { Result = $"payment fee {_paymentFee.GetFee()} at {DateTime.Now}. Last update {_paymentFee.GetLastUpdateDatetime()}." });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ResultModel() { Result = $"There was a problem with the request. {ex.Message}" });
            }
        }

        [HttpPost]
        [Route("Pay")]
        public async Task<HttpResponseMessage> Pay(string CardNumber, decimal Amount)
        {
            try
            {
                var response = await _service.Pay(CardNumber, Amount, Math.Round(_paymentFee.GetFee(), 2));
                if (response != "OK")
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new ResultModel() { Result = $"Pay method failed. {response}" });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ResultModel() { Result = "Payment created sucessfully." });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ResultModel() { Result = $"There was a problem with the request. {ex.Message}" });
            }
        }

        [HttpGet]
        [Route("GetBalance")]
        public async Task<HttpResponseMessage> GetBalance(string CardNumber)
        {
            try
            {
                var response = await _service.GetBalance(CardNumber);

                if (response == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new ResultModel() { Result = $"The balance for the card {CardNumber} is not found." });

                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ResultModel() { Result = ex.Message });
            }
        }
    }
}
