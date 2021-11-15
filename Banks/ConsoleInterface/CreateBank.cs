using System;
using Banks.Entities;
using Banks.Tools;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.ConsoleInterface
{
    public class CreateBank : Command<CreateBank.Settings>
    {
        public override int Execute(CommandContext context, Settings settings)
        {
            try
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
                if (bankDepositUnlockDate < DateTime.Now)
                    throw new BanksException("Account unblocking date must be later than now");
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

                if (bankSettings is null) throw new BanksException("Invalid banks settings");
                var newBank = new Bank(bankSettings);
                settings.MainBank.AddNewBank(newBank);
            }
            catch (Exception e)
            {
                AnsiConsole.WriteException(e);
            }

            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandOption("-b|--bank")]
            public string BankName { get; set; }
            public MainBank MainBank { get; } = CreateMainBank.MainBank;
        }
    }
}