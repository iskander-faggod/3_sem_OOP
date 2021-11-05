using System;
using System.Collections.Generic;
using Banks.Commands;
using Banks.Entities;
using Banks.Entities.AccountsModel;
using Banks.Entities.AccountsModel.Creator;
using Spectre.Console;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var unlockDate = new DateTime(2022, 10, 20);

            /*tinkoff.AddClient(iskander);
            tinkoff.CreateDepositAccount(iskander);
            tinkoff.CreateCreditAccount(iskander);*/

            /*foreach (KeyValuePair<Guid, IAccount> account in tinkoff.GetAccounts())
            {
                account.Value.CashWithdrawalFromAccount(200);
                account.Value.CashReplenishmentToAccount(100);
            }

            foreach (KeyValuePair<Client, List<Guid>> pair in tinkoff.GetClientAccountsById())
            {
                Console.WriteLine(pair.Key);
                Console.WriteLine(pair.Value);
            }

            AnsiConsole.Markup("[underline red]Hello[/] World!");*/

            var rule = new Rule("[red]Всем привет! Давайте начнем использовать и создавать банки[/]");
            AnsiConsole.Write(rule);
            while (true)
            {
                var iskander = new Client("Iskander", "Kudashev");
                string name = AnsiConsole.Ask<string>("Enter a [green]bank name[/] - ");
                decimal yearPercent = AnsiConsole.Ask<decimal>("Enter a [green]bank yearPercent[/] - ");
                decimal belowFiftyThousandPercent =
                    AnsiConsole.Ask<decimal>("Enter a [green]bank belowFiftyThousandPercent[/] - ");
                decimal betweenFiftyAndHundredThousandPercent =
                    AnsiConsole.Ask<decimal>("Enter a [green]bank betweenFiftyAndHundredThousandPercent[/] - ");
                decimal aboveHundredThousandPercent =
                    AnsiConsole.Ask<decimal>("Enter a [green]bank aboveHundredThousandPercent[/] - ");
                TimeSpan depositTimeDuration = AnsiConsole.Ask<TimeSpan>("Enter a [green]depositTimeDuration[/] - ");
                DateTime depositUnlockDate = AnsiConsole.Ask<DateTime>("Enter a [green]depositUnlockDate[/] - ");
                decimal transferLimit = AnsiConsole.Ask<decimal>("Enter a [green]transferLimit[/] - ");
                decimal commission = AnsiConsole.Ask<decimal>("Enter a [green]commission[/] - ");
                var settings = new BankSettings(name, yearPercent, belowFiftyThousandPercent, betweenFiftyAndHundredThousandPercent, aboveHundredThousandPercent, depositTimeDuration, depositUnlockDate, transferLimit, commission);
                var bank = new Bank(settings);

                AnsiConsole.Write(new Markup($"[green]Банк {bank.GetBankName()} создан[/]"));

                break;
            }
        }
    }
}