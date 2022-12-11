using FacadePatternTask;
using FacadePatternTask.InvoiceSystem;
using FacadePatternTask.PaymentSystem;
using FacadePatternTask.Product;

var productCatalog = new ProductCatalog();
var paymentSystem = new PaymentSystem();
var invoiceSystem = new InvoiceSystem();

var orderFacade = new OrderFacade(productCatalog, invoiceSystem, paymentSystem);
orderFacade.PlaceOrder("P-12", 20, "test@gmail.com");

Console.WriteLine("Order placed");