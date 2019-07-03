using System.Collections.Generic;



namespace Entities
{
    public interface IAccountDAL
    {
        List<IAccount> GetAllAccount();
        List<IAccount> GetAllAccount(int id);

        int GenerateAccountno();
        string GetAcountType(IAccount account);
        double GetBalance();
        bool AcccountIsFound(int accoutno);

        void OpenAccount(IAccount account);
        void CloseAccount(int id, int accountno);
        void Deposit(IAccount account, double amount);
        void Withdraw(IAccount account, double amount);
        void Transfer(int fromAccountno, int toAccountno, double amount);

        IAccount GetAccount(int accountno);
        List<Transaction> GetTransaction(int accountno);




    }
}
