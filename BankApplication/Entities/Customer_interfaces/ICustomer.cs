using System.Collections.Generic;
namespace Entities
{
    public interface ICustomer
    {
        //properties
        string FirstName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string Address { get; set; }
        int CustomerId { get; set; }
     


    }
    
}
