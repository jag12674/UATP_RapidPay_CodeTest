using RapidPay.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RapidPayAPI.Services.CardManagement
{
    public class CardManagementService : ICardManagementService
    {        
        public async Task<string> CreateCard(string CardNumber, decimal CreditLimit)
        {
            try
            {
                using (RapidPayEntities db = new RapidPayEntities())
                {
                    if (CardNumber.Length != 15)
                        return "Card format must be 15 digits.";

                    if (CreditLimit <= 0)
                        return "Credit limit must be greater than zero.";

                    if (db.RPCards.Any(x => x.CardNumber == CardNumber))
                        return "Card number already exists.";

                    db.RPCards.Add(new RPCard() { 
                        CardNumber = CardNumber,
                        CreditLimit = CreditLimit,
                        Balance = CreditLimit
                    });

                    await db.SaveChangesAsync();
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred adding a new card: {ex.Message}", ex);
            }
        }

        public async Task<string> Pay(string CardNumber, decimal Amount, double Fee)
        {
            try
            {
                using (RapidPayEntities db = new RapidPayEntities())
                {
                    if (!db.RPCards.Any(x => x.CardNumber == CardNumber))
                        return "Card number not exists.";

                    if (Amount <= 0)
                        return "Amount must be greater than zero.";

                    var card = await db.RPCards.Where(x => x.CardNumber == CardNumber).SingleOrDefaultAsync();

                    double creditLimit = (double)card.CreditLimit;

                    double balance = (double)card.Balance;                    

                    if (balance - (Fee + (double)Amount) < 0)
                        return "Insufficient funds/over credit limit.";                    

                    db.RPPayments.Add(new RPPayment()
                    {
                        Card = CardNumber,
                        Amount = Amount,
                        Fee = (decimal)Fee,                        
                        TotalAmount = Amount + (decimal)Fee,
                        CreatedDateTime = DateTime.Now
                    });

                    //update balance
                    card.Balance = card.Balance - (Amount + (decimal)Fee);

                    await db.SaveChangesAsync();
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred making a payment: {ex.Message}", ex);
            }
        }

        public async Task<RPCard> GetBalance(string CardNumber)
        {
            try
            {
                using (RapidPayEntities db = new RapidPayEntities())
                {
                    db.Configuration.ProxyCreationEnabled = false;

                    var card = await db.RPCards.Where(x => x.CardNumber == CardNumber).SingleOrDefaultAsync();

                    if (card == null)
                        return null;

                    return card;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred getting the balance for card number {CardNumber}: {ex.Message}", ex);
            }
        }
    }
}