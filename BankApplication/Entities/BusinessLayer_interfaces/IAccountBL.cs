using System.Collections.Generic;

namespace Entities
{
    public interface IAccountBL
    {
        List<IAccount> GetAllAccount();
        List<IAccount> GetAllAccount(int id);

        int GenerateAccountno();
        string GetAcountType(IAccount account);
        decimal GetBalance();
        bool AcccountIsFound(int accoutno);

        void OpenAccount(IAccount account);
        void CloseAccount(int accountno);
        void Deposit(IAccount account, decimal amount);
        void Withdraw(IAccount account, decimal amount);
        void Transfer(int fromAccountno, int toAccountno, decimal amount);

        IAccount GetAccount(int accountno);
        List<Transaction> GetTransaction(int accountno);




    }

}
