using System;
namespace Entities
{
    public class Loan : Account, ILoan
    {



        public Loan(decimal amount)
        {
            Balance = amount;
        }

        public Loan()
        {

        }

        public override double GetIntrestrate()
        {
            return Interestrate = 0.08;
        }

        public override string GetAccountType(IAccount account)
        {
            return AccountType = "Loan";
        }
        public override void Withdraw(IAccount account, decimal amount)
        {
            Console.WriteLine("Can not withdraw from Loan Account");
        }

 

    }
}
