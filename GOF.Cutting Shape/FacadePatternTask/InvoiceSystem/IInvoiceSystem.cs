using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePatternTask.InvoiceSystem
{
    public interface IInvoiceSystem
    {
        void SendInvoice(Invoice invoice);
    }
}
