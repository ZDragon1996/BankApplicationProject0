using System;
using Entities;
using DAL;
using System.Collections.Generic;


namespace BusinessLayer
{
    public class CustomerBL: ICustomerBL
    {
        public CustomerBL()
        {

        }

        CustomerDAL customerdal = new CustomerDAL();



        public void Register(Customer customer)
        {
            customerdal.Register(customer);
        }

        public List<Customer> GetAllCustomer()
        {
            return customerdal.GetAllCustomer();
        }

        public int GenerateCustomerId()
        {
            return customerdal.GenerateCustomerId();
        }

      

    }
}
