using System;
using System.Collections.Generic;

namespace Entities
{
    public class Customer: ICustomer
    {
        public Customer()
        {
        }

        //properties
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int CustomerId { get; set; }
 
        //methods




    }
}
