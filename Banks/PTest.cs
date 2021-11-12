using System;
using Banks.Commands;
using Banks.Entities;
using Banks.Entities.AccountsModel.Creator;
using Banks.Entities.ClientModel;
using Spectre.Console;

namespace Banks
{
    public class PTest
    {
        private static void TMain()
        {
            var iskander = new Client("Iskander", "Kudashev", "Svoboda 10", "6717637675");
            var misha = new Client("Misha", "Lipa", "Gde-to v Kupchino", "13371448");
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
            IAccount iskanderCredit = bank.CreateCreditAccount(iskander);
            IAccount iskanderDebit = bank.CreateDebitAccount(iskander);
            IAccount iskanderDeposit = bank.CreateDepositAccount(iskander);

            IAccount mishaCreditAccount = bank.CreateCreditAccount(misha);
            IAccount mishaDebitAccount = bank.CreateDebitAccount(misha);
            IAccount mishaDepositAccount = bank.CreateDepositAccount(misha);

            bank.HandleCommand(
                new RepleshmentBankCommand(
                    mishaCreditAccount.GetAccountId(),
                    200,
                    mishaCreditAccount),
                misha);
            bank.HandleCommand(
                new WithdrawalBankCommand(
                    mishaCreditAccount.GetAccountId(),
                    200,
                    mishaCreditAccount),
                misha);
            bank.HandleCommand(
                new TransferToAnotherAccountCommand(
                    mishaCreditAccount.GetAccountId(),
                    200,
                    mishaCreditAccount,
                    iskanderCredit), misha);

            mainBank.TimeRewind(3);
            iskanderCredit.GetDeposit();
            mishaCreditAccount.GetDeposit();

            var rule = new Rule("[red]Всем привет! Давайте начнем использовать и создавать банки[/]");
            AnsiConsole.Write(rule);

            var bank2 = bank.GetAccountById(iskanderCredit.GetAccountId());
            var client2 = bank.GetClientById("13371448");
        }
    }
}