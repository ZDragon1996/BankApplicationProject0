using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using BusinessLayer;

namespace UnitTest1
{
    [TestClass]
    public class UnitTest1
    {
        #region DepositAndWithdrawTest()
        [TestMethod]
        public void DepositAndWithdrawTest()
        {

            AccountBL accountbl = new AccountBL();

          
            decimal expectedCheckingBalance = 2000m;
            decimal expectedBusinessBalance = 4001m;
            decimal expectedLoanBalance = 0;
            decimal expectedTermdepositBalance = 0;

            //Checking Deposit Test
            //===========================================================================================
            IAccount checkingAccountInfo = new CheckingAccount()
            {
                Accountno = accountbl.GenerateAccountno(),
                Balance = accountbl.GetBalance(),
                CustomerId = 1,
                AccountStatus = true

            };

            checkingAccountInfo.AccountType = accountbl.GetAcountType(checkingAccountInfo);
            checkingAccountInfo.Interestrate = accountbl.GetIntrestrate(checkingAccountInfo);

            accountbl.OpenAccount(checkingAccountInfo);

            accountbl.Deposit(checkingAccountInfo, 1000m); //good test
            accountbl.Deposit(checkingAccountInfo, 2000m); //good test
            accountbl.Deposit(checkingAccountInfo, 1.0m); //good test

            accountbl.Deposit(checkingAccountInfo, -1000m); //bad test
            accountbl.Deposit(checkingAccountInfo, -1m); //bad test
            accountbl.Deposit(checkingAccountInfo, -1.0m); //bad test
            accountbl.Deposit(checkingAccountInfo, 0m); //bad test

            //after deposit: 3001
            ////================================withdraw==========================================


            accountbl.Withdraw(checkingAccountInfo, 900m); //good test
            accountbl.Withdraw(checkingAccountInfo, 100m); //good test
            accountbl.Withdraw(checkingAccountInfo, 1.0m); //good test

            accountbl.Withdraw(checkingAccountInfo, -1000m); //bad test
            accountbl.Withdraw(checkingAccountInfo, -1m); //bad test
            accountbl.Withdraw(checkingAccountInfo, -1.0m); //bad test
            accountbl.Withdraw(checkingAccountInfo, 0m); //bad test



            Assert.AreEqual<decimal>(expectedCheckingBalance, checkingAccountInfo.Balance);//output: 2000

            //Business Deposit Test
            //===========================================================================================
            IAccount businessAccountInfo = new BusinessAccount()
            {
                Accountno = accountbl.GenerateAccountno(),
                Balance = accountbl.GetBalance(),
                CustomerId = 1,
                AccountStatus = true,
          

            };

            businessAccountInfo.AccountType = accountbl.GetAcountType(businessAccountInfo);
            businessAccountInfo.Interestrate = accountbl.GetIntrestrate(businessAccountInfo);

            accountbl.OpenAccount(businessAccountInfo);

            accountbl.Deposit(businessAccountInfo, 2000m); //good test
            accountbl.Deposit(businessAccountInfo, 2000m); //good test
            accountbl.Deposit(businessAccountInfo, 1.0m); //good test

            accountbl.Deposit(businessAccountInfo, -1000m); //bad test
            accountbl.Deposit(businessAccountInfo, -1m); //bad test
            accountbl.Deposit(businessAccountInfo, -1.0m); //bad test
            accountbl.Deposit(businessAccountInfo, 0m); //bad test

           

            Assert.AreEqual<decimal>(expectedBusinessBalance, businessAccountInfo.Balance);//output 3000
            //Loan Deposit Test
            //========================================================================================
            IAccount loanAccountInfo = new Loan(0)
            {
                Accountno = accountbl.GenerateAccountno(),
                Balance = accountbl.GetBalance(),
                CustomerId = 1,
                AccountStatus = true
        

            };


            loanAccountInfo.Interestrate = accountbl.GetIntrestrate(loanAccountInfo);

            accountbl.OpenAccount(loanAccountInfo);

            accountbl.Deposit(loanAccountInfo, 2000m); //good test
            accountbl.Deposit(loanAccountInfo, 2000m); //good test
            accountbl.Deposit(loanAccountInfo, 1.0m); //good test

            accountbl.Deposit(loanAccountInfo, -1000m); //bad test
            accountbl.Deposit(loanAccountInfo, -1m); //bad test
            accountbl.Deposit(loanAccountInfo, -1.0m); //bad test
            accountbl.Deposit(loanAccountInfo, 0m); //bad test


            Assert.AreEqual<decimal>(expectedLoanBalance,loanAccountInfo.Balance);
            //Term Deposit Test
            //=======================================================================================


            IAccount termdepositAccountInfo = new TermDeposit(0)
            {
                Accountno = accountbl.GenerateAccountno(),
                Balance = accountbl.GetBalance(),
                CustomerId = 1,
                AccountStatus = true


            };


            accountbl.OpenAccount(termdepositAccountInfo);

            accountbl.Deposit(termdepositAccountInfo, 2000m); //good test
            accountbl.Deposit(termdepositAccountInfo, 2000m); //good test
            accountbl.Deposit(termdepositAccountInfo, 1.0m); //good test

            accountbl.Deposit(termdepositAccountInfo, -1000m); //bad test
            accountbl.Deposit(termdepositAccountInfo, -1m); //bad test
            accountbl.Deposit(termdepositAccountInfo, -1.0m); //bad test
            accountbl.Deposit(termdepositAccountInfo, 0m); //bad test


            Assert.AreEqual<decimal>(expectedTermdepositBalance, termdepositAccountInfo.Balance);
        }
        #endregion


        [TestMethod]
        public void TransferTest()
        {
            AccountBL accountbl = new AccountBL();


            decimal expectedCheckingBalance = 1900;
            decimal expectedBusinessBalance = -900;
            decimal expectedLoanBalance = 0;
            decimal expectedTermdepositBalance = 0;

            //Checking Deposit Test
            //===========================================================================================
            IAccount checkingAccountInfo = new CheckingAccount()
            {
                Accountno = accountbl.GenerateAccountno(),
                Balance = accountbl.GetBalance(),
                CustomerId = 1,
                AccountStatus = true

            };

            checkingAccountInfo.AccountType = accountbl.GetAcountType(checkingAccountInfo);
            checkingAccountInfo.Interestrate = accountbl.GetIntrestrate(checkingAccountInfo);

            accountbl.OpenAccount(checkingAccountInfo);


           
            IAccount businessAccountInfo = new BusinessAccount()
            {
                Accountno = accountbl.GenerateAccountno(),
                Balance = accountbl.GetBalance(),
                CustomerId = 1,
                AccountStatus = true,


            };

            businessAccountInfo.AccountType = accountbl.GetAcountType(businessAccountInfo);
            businessAccountInfo.Interestrate = accountbl.GetIntrestrate(businessAccountInfo);

            accountbl.OpenAccount(businessAccountInfo);



         
            IAccount loanAccountInfo = new Loan(0)
            {
                Accountno = accountbl.GenerateAccountno(),
                Balance = accountbl.GetBalance(),
                CustomerId = 1,
                AccountStatus = true


            };


            loanAccountInfo.Interestrate = accountbl.GetIntrestrate(loanAccountInfo);

            accountbl.OpenAccount(loanAccountInfo);

            

            IAccount termdepositAccountInfo = new TermDeposit(0)
            {
                Accountno = accountbl.GenerateAccountno(),
                Balance = accountbl.GetBalance(),
                CustomerId = 1,
                AccountStatus = true


            };


            accountbl.OpenAccount(termdepositAccountInfo);




            accountbl.Deposit(checkingAccountInfo, 1000);

        
           //1.from checking to business
            accountbl.Transfer(checkingAccountInfo.Accountno, businessAccountInfo.Accountno, 100);
            accountbl.Transfer(checkingAccountInfo.Accountno, businessAccountInfo.Accountno, -200);
            accountbl.Transfer(checkingAccountInfo.Accountno, businessAccountInfo.Accountno, 0);
            accountbl.Transfer(checkingAccountInfo.Accountno, businessAccountInfo.Accountno, -99999);
            // checking:900,business: 100

            //2.from checking to loan
            accountbl.Transfer(checkingAccountInfo.Accountno, loanAccountInfo.Accountno, 100);
            accountbl.Transfer(checkingAccountInfo.Accountno, loanAccountInfo.Accountno, -200);
            accountbl.Transfer(checkingAccountInfo.Accountno, loanAccountInfo.Accountno, 0);
            accountbl.Transfer(checkingAccountInfo.Accountno, loanAccountInfo.Accountno, -99999);
            // checking:900,business: 0

            //3.from checking to term deposit
            accountbl.Transfer(checkingAccountInfo.Accountno, termdepositAccountInfo.Accountno, 100);
            accountbl.Transfer(checkingAccountInfo.Accountno, termdepositAccountInfo.Accountno, -200);
            accountbl.Transfer(checkingAccountInfo.Accountno, termdepositAccountInfo.Accountno, 0);
            accountbl.Transfer(checkingAccountInfo.Accountno, termdepositAccountInfo.Accountno, -99999);
            // checking:900,term deposit: 0



            //4.from business to checking
            accountbl.Transfer(businessAccountInfo.Accountno, checkingAccountInfo.Accountno, 1000);
            accountbl.Transfer(businessAccountInfo.Accountno, checkingAccountInfo.Accountno, -200);
            accountbl.Transfer(businessAccountInfo.Accountno, checkingAccountInfo.Accountno, 0);
            //business: -900 , checking:1900

            //5.from business to loan
            accountbl.Transfer(businessAccountInfo.Accountno, loanAccountInfo.Accountno, 1000);
            accountbl.Transfer(businessAccountInfo.Accountno, loanAccountInfo.Accountno, -200);
            accountbl.Transfer(businessAccountInfo.Accountno, loanAccountInfo.Accountno, 0);
            accountbl.Transfer(businessAccountInfo.Accountno, loanAccountInfo.Accountno, -99999);
            // business:-900,loan: 0

            //6.from business to termdeposit
            accountbl.Transfer(businessAccountInfo.Accountno, termdepositAccountInfo.Accountno, 1000);
            accountbl.Transfer(businessAccountInfo.Accountno, termdepositAccountInfo.Accountno, -200);
            accountbl.Transfer(businessAccountInfo.Accountno, termdepositAccountInfo.Accountno, 0);
            accountbl.Transfer(businessAccountInfo.Accountno, termdepositAccountInfo.Accountno, -99999);
            // business:-900,term deposit 0

            //7.from loan to termdeposit
            accountbl.Transfer(loanAccountInfo.Accountno, termdepositAccountInfo.Accountno, 1000);
            accountbl.Transfer(loanAccountInfo.Accountno, termdepositAccountInfo.Accountno, -200);
            accountbl.Transfer(loanAccountInfo.Accountno, termdepositAccountInfo.Accountno, 0);
            accountbl.Transfer(loanAccountInfo.Accountno, termdepositAccountInfo.Accountno, -99999);
            // loan: 0, termdeposit: 0

            //8.from termdeposit to loan
            accountbl.Transfer(termdepositAccountInfo.Accountno, loanAccountInfo.Accountno, 1000);
            accountbl.Transfer(termdepositAccountInfo.Accountno, loanAccountInfo.Accountno, -200);
            accountbl.Transfer(termdepositAccountInfo.Accountno, loanAccountInfo.Accountno, 0);
            accountbl.Transfer(termdepositAccountInfo.Accountno, loanAccountInfo.Accountno, -99999);
            // term deposit: 0 loan: 0

            //9.from termdeposit to business
            accountbl.Transfer(termdepositAccountInfo.Accountno, businessAccountInfo.Accountno, 1000);
            accountbl.Transfer(termdepositAccountInfo.Accountno, businessAccountInfo.Accountno, -200);
            accountbl.Transfer(termdepositAccountInfo.Accountno, businessAccountInfo.Accountno, 0);
            accountbl.Transfer(termdepositAccountInfo.Accountno, businessAccountInfo.Accountno, -99999);
            // term deposit: 0 business: -900

            //10.from termdeposit to checking
            accountbl.Transfer(termdepositAccountInfo.Accountno, checkingAccountInfo.Accountno, 1000);
            accountbl.Transfer(termdepositAccountInfo.Accountno, checkingAccountInfo.Accountno, -200);
            accountbl.Transfer(termdepositAccountInfo.Accountno, checkingAccountInfo.Accountno, 0);
            accountbl.Transfer(termdepositAccountInfo.Accountno, checkingAccountInfo.Accountno, -99999);
            // term deposit: 0 checking: 1900

            //11.from loan to business
            accountbl.Transfer(loanAccountInfo.Accountno, businessAccountInfo.Accountno, 1000);
            accountbl.Transfer(loanAccountInfo.Accountno, businessAccountInfo.Accountno, -200);
            accountbl.Transfer(loanAccountInfo.Accountno, businessAccountInfo.Accountno, 0);
            accountbl.Transfer(loanAccountInfo.Accountno, businessAccountInfo.Accountno, -99999);
            // loan: 0, business: -900

            //12.from loan to checking
            accountbl.Transfer(loanAccountInfo.Accountno, checkingAccountInfo.Accountno, 1000);
            accountbl.Transfer(loanAccountInfo.Accountno, checkingAccountInfo.Accountno, -200);
            accountbl.Transfer(loanAccountInfo.Accountno, checkingAccountInfo.Accountno, 0);
            accountbl.Transfer(loanAccountInfo.Accountno, checkingAccountInfo.Accountno, -99999);
            // loan: 0, checking: 1900

            





            Assert.AreEqual(expectedCheckingBalance, checkingAccountInfo.Balance);
            Assert.AreEqual(expectedBusinessBalance, businessAccountInfo.Balance);
            Assert.AreEqual(expectedLoanBalance, loanAccountInfo.Balance);
            Assert.AreEqual(expectedTermdepositBalance, termdepositAccountInfo.Balance);




        
    }
    }
}


