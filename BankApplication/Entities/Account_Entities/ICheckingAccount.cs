using System;
using System.Collections.Generic;

namespace Entities
{
    public interface ICheckingAccount: IAccount
    {



        List<IAccount> GetAllAccounts(uint id);
      

    }
}
