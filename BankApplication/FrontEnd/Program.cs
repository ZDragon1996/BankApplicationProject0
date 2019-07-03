using BusinessLayer;
using System;
using Entities;
using System.Linq;
using System.Globalization;

namespace FrontEnd
{
    class Program
    {


        static Customer customer;
        static IAccount checkingAccountInfo;
        static IAccount businessAccountInfo;
        static IAccount loanAccountInfo;
        static IAccount termDepositAccountInfo;
        static AccountBL accountbl = new AccountBL();

        static void Main(string[] args)
        {


            RunApplication();


        }//end main;


        //=============Helper methods=================//

        #region RunApplication()
        public static void RunApplication()
        {
            string ans = "";

            while (ans != "2")
            {

                Console.WriteLine("**************************************************************");
                Console.WriteLine("*                     1. Register a customer                 *");
                Console.WriteLine("*                     2. Quit                                *");
                Console.WriteLine("**************************************************************");
                ans = Console.ReadLine();


                if (ans == "1")
                {
                    HandleRegister();
                    PerformOperations();
                }
                else if (ans == "2")
                {
                    Console.WriteLine("Exit");
                }

                else
                {
                    Console.WriteLine("Invalide Option");
                }

            }
        }
        #endregion

        



        #region AccountsExisted()
        public static bool AccountsExisted()
        {
            var accounts = accountbl.GetAllAccount(customer.CustomerId);
            bool result = true;

            if (accounts.Count == 0)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region DisplayAcocuntWithoutLoan()
        public static void DisplayAcocuntWithoutLoan()
        {
            var accounts = accountbl.GetAllAccountsWithoutLoan(customer.CustomerId);
            foreach (var account in accounts)
            {
                var formatIntrestRate = $"{account.Interestrate * 100} %";
                var formatmoney = account.Balance.ToString("C", new CultureInfo("en-US"));

                Console.WriteLine($"Account no: {account.Accountno} | Customer Id: {account.CustomerId} | Account Type: {account.GetAccountType(checkingAccountInfo)} | Balance: {formatmoney} | rate: {formatIntrestRate}| Activated: {account.AccountStatus}");
            }
        }
        #endregion

        #region CheckingAndBusinessAccountsExisted()
        public static bool CheckingAndBusinessAccountsExisted()
        {
            var accounts = accountbl.GetAllAccount(customer.CustomerId);
            bool result = false;

            if (accounts.Count == 0)
            {
                result = false;
            }

            foreach(var acc in accounts)
            {
                if(acc.AccountType == "Checking" || acc.AccountType == "Business" )
                {
                    result = true;
                }
            }


            return result;
        }
        #endregion

        #region DisplayAccounts(int customerId)
        public static void DisplayAccounts(int customerId)
        {
            if (AccountsExisted())
            {
                var accounts = accountbl.GetAllAccount(customerId);
                foreach (var account in accounts)
                {
                    Console.WriteLine($"Account no: {account.Accountno} | Customer Id: {account.CustomerId} | Account Type: {account.GetAccountType(checkingAccountInfo)} | Balance: {account.Balance} | Activated: {account.AccountStatus}");
                }
            }
            else
            {
                Console.WriteLine("No exisiting accounts");
            }

        }
        #endregion

        #region PerformOperations()
        public static void PerformOperations()
        {

            string stringResult;
            EOperations result;
            bool exit = false;

            do
            {


                Console.WriteLine("**************************************************************");
                Console.WriteLine("*            1. Open a new account                           *");
                Console.WriteLine("*            2. Close an account                             *");
                Console.WriteLine("*            3. Deposit                                      *");
                Console.WriteLine("*            4. Withdraw                                     *");
                Console.WriteLine("*            5. Transfer(between own accounts)               *");
                Console.WriteLine("*            6. Pay Loan installment                         *");
                Console.WriteLine("*            7. Display all customer accounts                *");
                Console.WriteLine("*            8. (admin)Display all accounts                  *");
                Console.WriteLine("*            9. Display transcations for an account          *");
                Console.WriteLine("*            10. Back                                        *");
                Console.WriteLine("**************************************************************");


                stringResult = Console.ReadLine();
                Console.WriteLine("Enter an operation between(1-9):");
                try
                {


                    result = (EOperations)Enum.Parse(typeof(EOperations), stringResult); // Convert string to Enum


                    switch (result)
                    {
                        case EOperations.OpenAccount:
                            Console.WriteLine("--->>>Open a new account<<<---");
                            HandleOpenAccount();
                            break;
                        case EOperations.CloseAccount:
                            Console.WriteLine("--->>>Close an Account<<<---");
                            HandleCloseAccount();
                            break;

                        case EOperations.Deposit:
                            Console.WriteLine("--->>>Deposit<<<---");
                            HandleDeposit();
                            break;

                        case EOperations.Withdraw:
                            Console.WriteLine("--->>>Withdraw<<<---");
                            HandleWithdraw();
                            break;

                        case EOperations.Transfer:
                            Console.WriteLine("--->>>Transfer<<<---");
                            HandleTransfer();
                            break;

                        case EOperations.PayLoan:
                            Console.WriteLine("--->>>Pay Loan installment<<<---");
                            HandlePayLoan();

                            break;
                        case EOperations.DisplayCustomerAccounts:
                            Console.WriteLine("--->>>Display all customer accounts<<<---");
                            DisplayUserAccounts();
                            break;
                        case EOperations.DisplayAllAccounts:
                            Console.WriteLine("--->>>Display all accounts<<<---");
                            DisplayAccounts();
                            break;

                        case EOperations.DisplayTransaction:
                            DisplayTransaction();
                            break;
                        case EOperations.Quit:
                            Console.WriteLine("--->>>Back<<<--");
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("--->>>Invalid Option<<<--");
                            break;

                    }



                }//end try
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }//end catch


            } while (!exit);
        }
        #endregion

        #region AskForAmount(IAccount acc)
        public static void AskForAmount(IAccount acc)
        {
            Console.WriteLine("Enter Amount you want to deposit");
            decimal amount = Convert.ToDecimal(Console.ReadLine());

            if (amount > 0)
            {
                accountbl.Deposit(acc, amount);
            }
            else if (amount.Equals(0))
            {
                Console.WriteLine("Input 0 amount is not valid");
            }
            else
            {
                Console.WriteLine("Failed!! Negative amount");
            }
        }
        #endregion


        //=============All Operations=================//

        #region HandleRegister()
        public static void HandleRegister()
        {
            Console.WriteLine($"--->>>Add Customer info to database<<<---");

            Console.WriteLine("Enter First Name");
            string firstname = Console.ReadLine();

            Console.WriteLine("Enter Last Name");
            string lastname = Console.ReadLine();

            Console.WriteLine("Enter Phone Number:");
            string phone = Console.ReadLine();

            Console.WriteLine("Enter Address:");
            string address = Console.ReadLine();

            CustomerBL customerBL = new CustomerBL();


            //create customer
            customer = new Customer()
            {
                FirstName = firstname,
                LastName = lastname,
                PhoneNumber = phone,
                Address = address,
                CustomerId = customerBL.GenerateCustomerId()
            };

            Console.WriteLine($"--->>>Assigned CustomerId: {customer.CustomerId}<<<---");

            customerBL.Register(customer);


            //output test for cusotmers list in CustomerBl, connect to CustomerDAL
            var customers = customerBL.GetAllCustomer();
            foreach (var cus in customers)
            {
                Console.WriteLine($"CustomerId: {cus.CustomerId}, First Name: {cus.FirstName}, Last Name: {cus.LastName}, Phone: {cus.PhoneNumber}, Address: {cus.Address}.");
            }


        }
        #endregion

        #region 1.HandleOpenAccount()
        public static void HandleOpenAccount()
        {

            Console.WriteLine("**************************************************************");
            Console.WriteLine("*                     1. Open Checking Account               *");
            Console.WriteLine("*                     2. Open Business Account               *");
            Console.WriteLine("*                     3. Open Loan Account                   *");
            Console.WriteLine("*                     4. Open Term Deposit                   *");
            Console.WriteLine("**************************************************************");


            string accountType = Console.ReadLine();

            try
            {
                EAccountType result = (EAccountType)Enum.Parse(typeof(EAccountType), accountType); // Convert string to Enum

                switch (result)
                {

                    case EAccountType.CheckingAccount:
                        checkingAccountInfo = new CheckingAccount()
                        {
                            Accountno = accountbl.GenerateAccountno(),
                            Balance = accountbl.GetBalance(),
                            CustomerId = customer.CustomerId,
                            AccountStatus = accountbl.GetAccountStatus()
    
                        };

                        

                        checkingAccountInfo.AccountType = accountbl.GetAcountType(checkingAccountInfo);
                        checkingAccountInfo.Interestrate = accountbl.GetIntrestrate(checkingAccountInfo);

                        accountbl.OpenAccount(checkingAccountInfo);

                        Console.WriteLine($"--->>>Generate random Account no: {checkingAccountInfo.Accountno}<<<---");

                        break;

                    case EAccountType.BusinessAccount:
                        businessAccountInfo = new BusinessAccount()
                        {
                            Accountno = accountbl.GenerateAccountno(),
                            Balance = accountbl.GetBalance(),
                            CustomerId = customer.CustomerId,
                            AccountStatus = accountbl.GetAccountStatus()


                        };

                        

                        businessAccountInfo.AccountType = accountbl.GetAcountType(businessAccountInfo);
                        businessAccountInfo.Interestrate = accountbl.GetIntrestrate(businessAccountInfo);
                        accountbl.OpenAccount(businessAccountInfo);

                        Console.WriteLine($"--->>>Generate random Account no: {businessAccountInfo.Accountno}<<<---");
                        break;

                    case EAccountType.Loan:
                      


                        Console.WriteLine("Enter Amount");
                        decimal amount = Convert.ToDecimal(Console.ReadLine());

                        if (amount >= 0)
                        {
                            loanAccountInfo = new Loan(amount)
                            {
                                Accountno = accountbl.GenerateAccountno(),
                                Balance = amount,
                                CustomerId = customer.CustomerId,
                                AccountStatus = accountbl.GetAccountStatus()
                            };



                            loanAccountInfo.AccountType = accountbl.GetAcountType(loanAccountInfo);
                            loanAccountInfo.Interestrate = accountbl.GetIntrestrate(loanAccountInfo);
                            accountbl.OpenAccount(loanAccountInfo);

                            Console.WriteLine($"--->>>Generate random Account no: {loanAccountInfo.Accountno}<<<---");
                        }
                        else
                        {
                            Console.WriteLine("Failed!! Negative amount");
                        }
                       

                        break;


                    case EAccountType.TermDeposit:

                        Console.WriteLine("Enter Amount");
                        decimal termdepositamount = Convert.ToDecimal(Console.ReadLine());

                        if (termdepositamount >= 0)
                        {
                            termDepositAccountInfo = new TermDeposit(termdepositamount)
                            {
                                Accountno = accountbl.GenerateAccountno(),
                                Balance = termdepositamount,
                                CustomerId = customer.CustomerId,
                                AccountStatus = accountbl.GetAccountStatus()
                            };



                            termDepositAccountInfo.AccountType = accountbl.GetAcountType(termDepositAccountInfo);
                            termDepositAccountInfo.Interestrate = accountbl.GetIntrestrate(termDepositAccountInfo);
                            accountbl.OpenAccount(termDepositAccountInfo);
                            Console.WriteLine($"--->>>Generate random Account no: {termDepositAccountInfo.Accountno}<<<---");
                        }
                        else
                        {
                            Console.WriteLine("Failed!! Negative amount");
                        }

                        break;
                    default:
                        break;

                }//end switch

         
            }//end try
            catch
            {
                Console.WriteLine("Invalid Option");
            }


        }//end handleOpenAccount()
        #endregion

        #region 2.HandleCloseAccount()
        public static void HandleCloseAccount()
        {


            if (AccountsExisted())
            {
                DisplayAccounts(customer.CustomerId);
                Console.WriteLine("Enter Account number you want to close");
                string accountNo = Console.ReadLine();//error not deal
                accountbl.CloseAccount(Convert.ToInt32(accountNo)); // Conver dealing with null, and out put 0. Prase does not deal with null value.
            }

            else
            {
                Console.WriteLine("No exisiting accounts");

            }
        }
        #endregion

        #region 3.HandleDeposit()
        public static void HandleDeposit()
        {
            if (accountbl.GetAllAccountsWithoutLoan(customer.CustomerId).Count != 0)
            {


                DisplayAcocuntWithoutLoan();
                Console.WriteLine("Enter Account number for deposit");
                int accountno = Convert.ToInt32(Console.ReadLine());
                IAccount acc = accountbl.GetAccount(accountno);

                if (accountbl.AcccountIsFound(accountno))
                {
                    Console.WriteLine("Enter Amount you want to deposit");
                    decimal amount = Convert.ToDecimal(Console.ReadLine());

                    if (amount > 0)
                    {
                        accountbl.Deposit(acc, amount);
                    }
                    else if (amount.Equals(0)){
                        Console.WriteLine("Input 0 amount is not valid");
                    }
                    else
                    {
                    Console.WriteLine("Failed!! Negative amount");
                    }

                }//end if (accountbl.AcccountIsFound(accountno))


                else
                {
                    Console.WriteLine("Account is not found in the database");

                }
            }//end if (CheckingAndBusinessAccountsExisted())

            else
            {
                Console.WriteLine("No exisiting checking or business accounts");
            }


        }
        #endregion

        #region 4.HandleWithdraw()
        public static void HandleWithdraw()
        {

            if (accountbl.GetAllAccountsWithoutLoan(customer.CustomerId).Count != 0)
            {
                DisplayAcocuntWithoutLoan();
                Console.WriteLine("Enter Account no for deposit");
                int accountno = Convert.ToInt32(Console.ReadLine());

                IAccount account = accountbl.GetAccount(accountno);



                if (accountbl.AcccountIsFound(accountno))
                {
                    Console.WriteLine("Enter Amount you want to withdraw");
                    decimal amount = Convert.ToDecimal(Console.ReadLine());
                    
                    if(amount > 0)
                    {
                        accountbl.Withdraw(account, amount);
                    }
                    else if (amount.Equals(0)) {
                        Console.WriteLine("Input 0 amount is not valid");
                    }
                    else
                    {
                        Console.WriteLine("Failed!! Negative amount");
                    }
                  

                  
                }
                else
                {
                    Console.WriteLine("Account is not found in the database");

                }
            }//end if (AccountsExisted())
            else
            {
                Console.WriteLine("No exisiting checking or business accounts");
            }

        }
        #endregion

        #region 5.HandleTransfer()
        public static void HandleTransfer()
        {
            if (accountbl.GetAllAccountsWithoutLoan(customer.CustomerId).Count != 0)
            {
                DisplayAcocuntWithoutLoan();


                Console.WriteLine("Transfer from account number:");
                int fromAccountno = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("to account number:");
                int toAccountno = Convert.ToInt32(Console.ReadLine());

                if (accountbl.AcccountIsFound(fromAccountno) && accountbl.AcccountIsFound(toAccountno))
                {
                    Console.WriteLine("Amount want to transfer");
                    decimal amount = Convert.ToDecimal(Console.ReadLine());
                    if(amount > 0)
                    {
                        accountbl.Transfer(fromAccountno, toAccountno, amount);
                    }
                    else if (amount.Equals(0))
                    {
                        Console.WriteLine("Input 0 amount is not valid");
                    }
                    else
                    {
                        Console.WriteLine("Failed!! Negative amount");
                    }
            
                }
                else
                {
                    Console.WriteLine("One of the accounts number is invalid");
                }

            }// end if (AccountsExisted())
            else
            {
                Console.WriteLine("No exisiting accounts");
            }
        }//End HandleWithdraw()
        #endregion

        #region 6.HandlePayLoan()
        public static void HandlePayLoan()
        {

            if (AccountsExisted())
            {

                var accounts = accountbl.GetAllLoanAccounts(customer.CustomerId);
                foreach (var account in accounts)
                {
                    Console.WriteLine($"Account no: {account.Accountno} | Customer Id: {account.CustomerId} | Account Type: {account.GetAccountType(checkingAccountInfo)} | Balance: {account.Balance} | Activated: {account.AccountStatus}");
                }


                Console.WriteLine("Enter account no");
                int accountno = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter amount you want to pay");
                int amount = Convert.ToInt32(Console.ReadLine());

                accountbl.PayLoan(accountno, amount);



            }
            else
            {
                Console.WriteLine("No exisiting accounts");
            }


        }
        #endregion

        #region 7.DisplayUserAccounts()
        public static void DisplayUserAccounts()
        {
            if (AccountsExisted())
            {
                var accounts = accountbl.GetAllAccount(customer.CustomerId);
                foreach (var account in accounts)
                {
                    var formatIntrestRate = $"{account.Interestrate * 100} %";
                    var formatmoney = account.Balance.ToString("C", new CultureInfo("en-US"));

                    Console.WriteLine($"Account no: {account.Accountno} | Customer Id: {account.CustomerId} | Account Type: {account.GetAccountType(checkingAccountInfo)} | Balance: {formatmoney} | rate: {formatIntrestRate}| Activated: {account.AccountStatus}");
                }
            }
            else
            {
                Console.WriteLine("No exisiting accounts");
            }


        }
        #endregion

        #region 8.DisplayAccounts()
        public static void DisplayAccounts()
        {
          
                var accounts = accountbl.GetAllAccount();
                foreach (var account in accounts)
                {
                var formatIntrestRate = $"{account.Interestrate * 100} %";
                var formatmoney = account.Balance.ToString("C", new CultureInfo("en-US"));

                Console.WriteLine($"Account no: {account.Accountno} | Customer Id: {account.CustomerId} | Account Type: {account.GetAccountType(checkingAccountInfo)} | Balance: {formatmoney} | rate: {formatIntrestRate} | Activated: {account.AccountStatus}");
                }
         
        }//end DisplayAccounts()
        #endregion

        #region 9.DisplayTransaction()
        public static void DisplayTransaction()
        {
            if (AccountsExisted())
            {

                Console.WriteLine("Enter Account no for deposit");
                int accountno = Convert.ToInt32(Console.ReadLine());

                var trans = accountbl.GetTransaction(accountno);

                foreach (var tran in trans)
                {
                    Console.WriteLine($"Transaction no: {tran.TransactionNo} | Time: {tran.Time} | Info: {tran.TransactionInfo}");

                }
            }
            else
            {
                Console.WriteLine("No exisiting accounts");
            }

        }//End DisplayTransaction()
        #endregion


    }//end program



}
