using System;
using System.Collections.Generic;

namespace Entities
{
    public interface ICustomerBL
    {

        void Register(Customer customer);
        List<Customer> GetAllCustomer();
        int GenerateCustomerId();

       


    }
}
