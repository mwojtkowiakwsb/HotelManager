

using HotelManager.Payment;

namespace HotelManager.Factories
{
    public class PaymentStrategyFactory
    {
        public PaymentStrategyFactory() 
        { 
        }

        public IPaymentStrategy GetPaymentStrategy(int strategyNumber)
        {
            return strategyNumber switch
            {
                1 => new BankTransferStrategy(),
                2 => new BlikPayStrategy(),
                _ => throw new ArgumentException($"Opcja o numerze {3} nie jest dostępna.")
            };
        }
    }
}
