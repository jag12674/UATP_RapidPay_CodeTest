using RapidPay.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPayAPI.Services.CardManagement
{
    public interface ICardManagementService
    {
        Task<string> CreateCard(string CardNumber, decimal CreditLimit);

        Task<string> Pay(string CardNumber, decimal Amount, double Fee);

        Task<RPCard> GetBalance(string CardNumber);
        
    }
}