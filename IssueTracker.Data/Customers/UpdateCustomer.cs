using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Customers
{
    [Route("/customers", "POST")]
    [Route("/customers/{id}", "PUT")]
    public class UpdateCustomer : IReturn<UpdateCustomerResponse>
    {
        [ApiMember]
        public long Id { get; set; }

        public string Name { get; set; }
    }

    public class UpdateCustomerResponse
    {
        public Customer Customer { get; set; }
    }
}