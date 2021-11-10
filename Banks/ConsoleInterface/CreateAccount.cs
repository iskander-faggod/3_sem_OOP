using Banks.Entities;
using Banks.Entities.ClientModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.ConsoleInterface
{
    public class CreateAccount : Command<CreateAccount.Settings>
    {
        public override int Execute(CommandContext context, Settings settings)
        {
            string userId = AnsiConsole.Ask<string>("Enter a user id");
            string bankName = AnsiConsole.Ask<string>("Enter a bank name");
            string account = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What type of [green]account[/] you want to create?")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Debit", "Credit", "Deposit",
                    }));
            Client client = settings.MainBank.GetClientById(userId);
            Bank bank = settings.MainBank.GetBankByName(bankName);

            switch (account)
            {
                case "Debit":
                    bank.CreateDebitAccount(client);
                    break;
                case "Credit":
                    bank.CreateCreditAccount(client);
                    break;
                case "Deposit":
                    bank.CreateDepositAccount(client);
                    break;
                default:
                    AnsiConsole.WriteLine("Account can't be created");
                    break;
            }

            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandOption("-a|--account")]
            [CommandArgument(0, "[MAIBANK]")]
            public MainBank MainBank { get; } = CreateMainBank.MainBank;
        }
    }
}