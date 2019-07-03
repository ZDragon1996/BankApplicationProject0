using System;
using System.Collections.Generic;

namespace Entities
{
    public class Transaction
    {
        public Transaction()
        {

        }

        public int TransactionNo { get; set; }
        public string TransactionInfo { get; set; }
        public int AccountNo { get; set; }
        public DateTime Time { get; set; }
        public int CustomerId { get; set; }
       
        public static List<Transaction> transactions = new List<Transaction>();


        static public int GenerateTransactionNo()
        {
            //uint range: 4.294967295 × 10^9
            //int range: -2.147483648 x 10^9 to 2.147483647 x 10^9
            //uint uintMax = uint.MaxValue;

            int intMax = int.MaxValue;

            Random random = new Random();
            int randomint = random.Next(1, intMax);


            Console.WriteLine($"--->>>Generate random Transaction No: {randomint}<<<---");
            return randomint;
        }

        public static void CreateTransaction(IAccount account, IAccount account2, decimal amount, string type)
        {
            string transactionInfo;
            int accountno = account.Accountno;
            int customerId = account.Accountno;

            switch (type)
            {
                case "Deposit":
                    transactionInfo = $"Deposit {amount} to accountno: {account.Accountno}";
                    break;
                case "Withdraw":
                    transactionInfo = $"Withdraw {amount} from accountno: {account.Accountno}";               
                    break;
                case "Transfer":              
                    transactionInfo = $"Transfer {amount} from: {account.Accountno} to: {account2.Accountno}";
                    break;
                case "Received":
                    transactionInfo = $"Received {amount} from: {account.Accountno}";
                    accountno = account2.Accountno;
                    customerId = account2.Accountno;
                    break;
                default:
                    transactionInfo = $"Transaction info is invalid";
                    break;

            }

            Transaction transaction = new Transaction()
            {
                TransactionNo = Transaction.GenerateTransactionNo(),
                TransactionInfo = transactionInfo,
                AccountNo = accountno,
                CustomerId = customerId,
                Time = DateTime.Now
            };

            transactions.Add(transaction); // add to transactions
            // add to transaction inside an account

        }

    }
}
