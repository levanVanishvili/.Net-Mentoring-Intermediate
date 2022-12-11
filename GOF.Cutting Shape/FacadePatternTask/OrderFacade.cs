using FacadePatternTask.InvoiceSystem;
using FacadePatternTask.PaymentSystem;
using FacadePatternTask.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePatternTask
{
    public class OrderFacade
    {
        private readonly IProductCatalog _productCatalog;
        private readonly IInvoiceSystem _invoiceSystem;
        private readonly IPaymentSystem _paymentSystem;

        public OrderFacade(IProductCatalog productCatalog, IInvoiceSystem invoiceSystem, IPaymentSystem paymentSystem)
        {
            _productCatalog = productCatalog;
            _invoiceSystem = invoiceSystem;
            _paymentSystem = paymentSystem;
        }

        public void PlaceOrder(string productId, int quantity, string email)
        {
            _productCatalog.GetProductDetails(productId);
            _paymentSystem.MakePayment(new Payment());
            _invoiceSystem.SendInvoice(new Invoice());
        }
    }
}
