using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePatternTask.InvoiceSystem
{
    public class InvoiceSystem : IInvoiceSystem
    {
        public void SendInvoice(Invoice invoice)
        {
            Console.WriteLine("Send Invoice");
        }
    }
}
