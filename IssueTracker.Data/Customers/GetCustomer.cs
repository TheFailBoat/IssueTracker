using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Customers
{
    [Route("/customers/{id}", "GET,OPTIONS")]
    public class GetCustomer : IReturn<GetCustomerResponse>
    {
        [ApiMember]
        public long Id { get; set; }
    }

    public class GetCustomerResponse
    {
        public Customer Customer { get; set; }
    }
}