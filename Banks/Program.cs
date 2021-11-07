using System;
using System.Collections.Generic;
using Banks.Commands;
using Banks.Entities;
using Banks.Entities.AccountsModel;
using Banks.Entities.AccountsModel.Creator;
using Banks.Entities.ClientModel;
using Spectre.Console;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
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

            bank.HandleCommand(new RepleshmentBankCommand(mishaCreditAccount.GetAccountId(), 200, mishaCreditAccount), misha);
            bank.HandleCommand(new WithdrawalBankCommand(mishaCreditAccount.GetAccountId(), 200, mishaCreditAccount), misha);
            bank.HandleCommand(
                new TransferToAnotherAccountCommand(mishaCreditAccount.GetAccountId(), 200, mishaCreditAccount, iskanderCredit), misha);

            mainBank.TimeRewind(3);
            iskanderCredit.GetDeposit();
            mishaCreditAccount.GetDeposit();

            var rule = new Rule("[red]Всем привет! Давайте начнем использовать и создавать банки[/]");
            AnsiConsole.Write(rule);
            while (true)
            {
                // TODO : Создание клиента
                string clientName = AnsiConsole.Ask<string>("Enter a [green]client name[/] - ");
                string clientSurname = AnsiConsole.Ask<string>("Enter a [green]client surname[/] - ");
                string address = AnsiConsole.Ask<string>("Enter a [green]client address[/] - ");
                string passportId = AnsiConsole.Ask<string>("Enter a [green]client passportId[/] - ");
                var client = new Client(clientName, clientSurname, address, passportId);
                AnsiConsole.Write($"Клиент с именем {clientName} создан");
                AnsiConsole.WriteLine();

                // TODO : Создание банка
                string bankName = AnsiConsole.Ask<string>("Enter a [green]bank name[/] - ");
                decimal bankYearPercent = AnsiConsole.Ask<decimal>("Enter a [green]bank yearPercent[/] - ");
                decimal bankBelowFiftyThousandPercent =
                    AnsiConsole.Ask<decimal>("Enter a [green]bank belowFiftyThousandPercent[/] - ");
                decimal bankBetweenFiftyAndHundredThousandPercent =
                    AnsiConsole.Ask<decimal>("Enter a [green]bank betweenFiftyAndHundredThousandPercent[/] - ");
                decimal bankAboveHundredThousandPercent =
                    AnsiConsole.Ask<decimal>("Enter a [green]bank aboveHundredThousandPercent[/] - ");
                DateTime bankDepositUnlockDate = AnsiConsole.Ask<DateTime>("Enter a [green]depositUnlockDate[/] - ");
                decimal bankTransferLimit = AnsiConsole.Ask<decimal>("Enter a [green]transferLimit[/] - ");
                decimal bankCommission = AnsiConsole.Ask<decimal>("Enter a [green]commission[/] - ");
                var bankSettings = new BankSettings(
                    bankName,
                    bankYearPercent,
                    bankBelowFiftyThousandPercent,
                    bankBetweenFiftyAndHundredThousandPercent,
                    bankAboveHundredThousandPercent,
                    bankDepositUnlockDate,
                    bankTransferLimit,
                    bankCommission);

                var newBank = new Bank(bankSettings);
                AnsiConsole.Write(new Markup($"[green]Банк {newBank.GetBankName()} создан[/]"));
                AnsiConsole.WriteLine();
            }

            /*while (true)
            {
                var iskander = new Client("Iskander", "Kudashev", "Svoboda 10", "иди нахуй");
                string name = AnsiConsole.Ask<string>("Enter a [green]bank name[/] - ");
                decimal yearPercent = AnsiConsole.Ask<decimal>("Enter a [green]bank yearPercent[/] - ");
                decimal belowFiftyThousandPercent =
                    AnsiConsole.Ask<decimal>("Enter a [green]bank belowFiftyThousandPercent[/] - ");
                decimal betweenFiftyAndHundredThousandPercent =
                    AnsiConsole.Ask<decimal>("Enter a [green]bank betweenFiftyAndHundredThousandPercent[/] - ");
                decimal aboveHundredThousandPercent =
                    AnsiConsole.Ask<decimal>("Enter a [green]bank aboveHundredThousandPercent[/] - ");
                DateTime depositUnlockDate = AnsiConsole.Ask<DateTime>("Enter a [green]depositUnlockDate[/] - ");
                decimal transferLimit = AnsiConsole.Ask<decimal>("Enter a [green]transferLimit[/] - ");
                decimal commission = AnsiConsole.Ask<decimal>("Enter a [green]commission[/] - ");
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
                bank.AddClient(iskander);
                AnsiConsole.Write(new Markup($"[green]Банк {bank.GetBankName()} создан[/]"));

                string account = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What's type of[green] account[/] you want to create?")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more accounts)[/]")
                        .AddChoices(new[]
                        {
                            "Credit Account", "Debit Account", "Deposit Account",
                        }));
                switch (account)
                {
                    case "Credit Account":
                        bank.CreateCreditAccount(iskander);
                        break;
                    case "Debit Account":
                        bank.CreateDebitAccount(iskander);
                        break;
                    case "Deposit Account":
                        bank.CreateDepositAccount(iskander);
                        break;
                    default:
                        AnsiConsole.Write($"{account} was created by {iskander.Name}");
                        break;
                }
            }
        }*/
        }
    }
}

// TODO : Команды не должны быть связаны, каждая команда делает свое действие
// TODO : Команда создания, команда транзакции, команда таблички
// TODO : ПРОВЕРИТЬ СИГНАТУРЫ АККАУНТОВ
// TODO : UI для команд оберрунть