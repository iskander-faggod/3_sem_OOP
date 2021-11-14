using System;
using System.Linq;
using Banks.Commands;
using Banks.Entities;
using Banks.Entities.AccountsModel.Creator;
using Banks.Entities.ClientModel;
using NUnit.Framework;

namespace Banks.Tests
{
    public class Tests
    {

        [Test]
        public void AddBanksFromMainBank()
        {
            const string name = "Tinkoff";
            const decimal yearPercent = 0.02M;
            const decimal belowFiftyThousandPercent = 0.03M;
            const decimal betweenFiftyAndHundredThousandPercent = 0.04M;
            const decimal aboveHundredThousandPercent = 0.05M;
            var depositUnlockDate = new DateTime(2021, 11, 20);
            const decimal transferLimit = -10000;
            const decimal commission = 300;
            
            var settings = new BankSettings(
                name,
                yearPercent,
                belowFiftyThousandPercent,
                betweenFiftyAndHundredThousandPercent,
                aboveHundredThousandPercent,
                depositUnlockDate,
                transferLimit,
                commission);
            
            var bank = new Bank(settings);

            var mainBank = new MainBank();
            Bank addedBank = mainBank.AddNewBank(bank);
            Assert.AreEqual(bank, addedBank);
            Assert.True(mainBank.GetBanks.Contains(bank));
        }  
        
        [Test]
        public void AddAccountsToBank()
        {
            const string name = "Tinkoff";
            const decimal yearPercent = 0.02M;
            const decimal belowFiftyThousandPercent = 0.03M;
            const decimal betweenFiftyAndHundredThousandPercent = 0.04M;
            const decimal aboveHundredThousandPercent = 0.05M;
            var depositUnlockDate = new DateTime(2021, 11, 20);
            const decimal transferLimit = -10000;
            const decimal commission = 300;
            
            var settings = new BankSettings(
                name,
                yearPercent,
                belowFiftyThousandPercent,
                betweenFiftyAndHundredThousandPercent,
                aboveHundredThousandPercent,
                depositUnlockDate,
                transferLimit,
                commission);
            
            var bank = new Bank(settings);
            var mainBank = new MainBank();
            mainBank.AddNewBank(bank);
            var iskander = new Client("Iskander", "Kudashev", "Svoboda 10", 671763767);
            var misha = new Client("Misha", "Lipa", "Gde-to v Kupchino", 10012223);
            IAccount iskanderCredit = bank.CreateCreditAccount(iskander);
            IAccount iskanderDebit = bank.CreateDebitAccount(iskander);
            IAccount iskanderDeposit = bank.CreateDepositAccount(iskander);

            IAccount mishaCreditAccount = bank.CreateCreditAccount(misha);
            IAccount mishaDebitAccount = bank.CreateDebitAccount(misha);
            IAccount mishaDepositAccount = bank.CreateDepositAccount(misha);
            
            Assert.Contains(iskanderCredit, bank.GetAllAccounts());
            Assert.Contains(iskanderDebit, bank.GetAllAccounts());
            Assert.Contains(iskanderDebit, bank.GetAllAccounts());
            Assert.Contains(mishaCreditAccount, bank.GetAllAccounts());
            Assert.Contains(mishaDebitAccount, bank.GetAllAccounts());
            Assert.Contains(mishaDepositAccount, bank.GetAllAccounts());
        }

        [Test]
        public void CheckOperations()
        {
            const string name = "Tinkoff";
            const decimal yearPercent = 0.02M;
            const decimal belowFiftyThousandPercent = 0.03M;
            const decimal betweenFiftyAndHundredThousandPercent = 0.04M;
            const decimal aboveHundredThousandPercent = 0.05M;
            var depositUnlockDate = new DateTime(2021, 11, 20);
            const decimal transferLimit = -10000;
            const decimal commission = 300;

            var settings = new BankSettings(
                name,
                yearPercent,
                belowFiftyThousandPercent,
                betweenFiftyAndHundredThousandPercent,
                aboveHundredThousandPercent,
                depositUnlockDate,
                transferLimit,
                commission);

            var bank = new Bank(settings);
            var mainBank = new MainBank();
            mainBank.AddNewBank(bank);
            var iskander = new Client("Iskander", "Kudashev", "Svoboda 10", 671763767 );
            var misha = new Client("Misha", "Lipa", "Gde-to v Kupchino", 10012223);
            IAccount iskanderCredit = bank.CreateCreditAccount(iskander);
            IAccount iskanderDebit = bank.CreateDebitAccount(iskander);
            IAccount iskanderDeposit = bank.CreateDepositAccount(iskander);
            IAccount mishaDepositAccount = bank.CreateDepositAccount(misha);
            IAccount mishaDebitAccount = bank.CreateDebitAccount(misha);
            
            bank.HandleCommand(new RepleshmentBankCommand(iskanderCredit.GetAccountId(), 300, iskanderCredit), iskander);
            bank.HandleCommand(new RepleshmentBankCommand(iskanderCredit.GetAccountId(), 300, iskanderCredit), iskander);
            Assert.True(bank.GetAccountById(iskanderCredit.GetAccountId()).GetDeposit() == 600);
            bank.HandleCommand(new WithdrawalBankCommand(iskanderCredit.GetAccountId(), 300, iskanderCredit), iskander);
            Assert.True(bank.GetAccountById(iskanderCredit.GetAccountId()).GetDeposit() == 300);

            bank.HandleCommand(new TransferToAnotherAccountCommand(iskanderCredit.GetAccountId(), 300, iskanderCredit, mishaDebitAccount), iskander);
            Assert.True(bank.GetAccountById(iskanderCredit.GetAccountId()).GetDeposit() == 0);
            Assert.True(bank.GetAccountById(mishaDebitAccount.GetAccountId()).GetDeposit() == 300);
        }
    }
    
}