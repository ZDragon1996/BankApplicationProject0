using System.Collections.Generic;

namespace Entities
{
    public interface ICustomerDAL
    {

        void Register(Customer customer);
        List<Customer> GetAllCustomer();
        int GenerateCustomerId();




    }
}


