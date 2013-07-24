using System.Collections.Generic;
using IssueTracker.Data.Users;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Customers
{
    [Route("/customers/{id}/users", "GET")]
    public class GetCustomerUsers : IReturn<GetCustomerUsersResponse>
    {
        [ApiMember]
        public long Id { get; set; }
    }

    public class GetCustomerUsersResponse
    {
        public Customer Customer { get; set; }
        public List<User> Users { get; set; }
    }
}