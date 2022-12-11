using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePatternTask.PaymentSystem
{
    public class PaymentSystem : IPaymentSystem
    {
        public bool MakePayment(Payment payment)
        {
            Console.WriteLine("Make payment");
            return true;
        }
    }
}
