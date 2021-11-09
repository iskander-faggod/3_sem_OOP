using System;
using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.ConsoleInterface
{
    public class CreateBank : Command<CreateBank.Settings>
    {
        public override int Execute(CommandContext context, Settings settings)
        {
            settings.BankName = AnsiConsole.Ask<string>("Enter a [green]bank name[/] - ");
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
                settings.BankName,
                bankYearPercent,
                bankBelowFiftyThousandPercent,
                bankBetweenFiftyAndHundredThousandPercent,
                bankAboveHundredThousandPercent,
                bankDepositUnlockDate,
                bankTransferLimit,
                bankCommission);

            var newBank = new Bank(bankSettings);
            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandOption("-b|--bank")]
            public string BankName { get; set; }
        }
    }
}