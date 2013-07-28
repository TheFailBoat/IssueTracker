using IssueTracker.API.Repositories;
using IssueTracker.API.Security;
using IssueTracker.API.Utilities;
using IssueTracker.Data.Customers;
using ServiceStack.Common.Web;
using ServiceStack.Text;

namespace IssueTracker.API.Services
{
    internal class CustomersService : BaseService
    {
        public CustomersService(ISecurityService securityService)
            : base(securityService)
        {
        }

        public ICustomerRepository CustomerRepository { get; set; }

        public ListCustomerResponse Get(ListCustomers request)
        {
            return new ListCustomerResponse
            {
                Customers = CustomerRepository.GetAll().ToDto()
            };
        }
        public GetCustomerResponse Get(GetCustomer request)
        {
            var customer = CustomerRepository.GetById(request.Id);
            if (customer == null) throw HttpError.NotFound("customer {0} not found".Fmt(request.Id));

            return new GetCustomerResponse
            {
                Customer = customer.ToDto()
            };
        }
    }
}