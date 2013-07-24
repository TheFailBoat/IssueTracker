using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Customers
{
    [Route("/customers", "GET")]
    public class ListCustomers : IReturn<ListCustomerResponse>
    {
    }

    public class ListCustomerResponse
    {
        public List<Customer> Customers { get; set; }
    }
}