using System;
using Banks.Commands;
using Banks.Entities;
using Banks.Entities.AccountsModel.Creator;
using Banks.Entities.ClientModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.ConsoleInterface
{
    public class CreateOperations : Command<CreateOperations.Settings>
    {
        public override int Execute(CommandContext context, Settings settings)
        {
            string userId = AnsiConsole.Ask<string>("Enter a user id");
            string bankName = AnsiConsole.Ask<string>("Enter a bank name");
            int cash = AnsiConsole.Ask<int>("Enter transfer value");
            Guid id = AnsiConsole.Ask<Guid>("Enter account id");
            string command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What type of [green]account[/] you want to create?")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "Repleshment", "Transfer", "Withdrawal",
                    }));

            Client client = settings.MainBank.GetClientById(userId);
            Bank bank = settings.MainBank.GetBankByName(bankName);
            IAccount newAccount = bank.GetAccountById(id);

            switch (command)
            {
                case "Repleshment":
                    bank.HandleCommand(
                        new RepleshmentBankCommand(settings.Account.GetAccountId(), cash, settings.Account), client);
                    break;
                case "Withdrawal":
                    bank.HandleCommand(
                        new WithdrawalBankCommand(settings.Account.GetAccountId(), cash, settings.Account), client);
                    break;
                case "Transfer":
                    bank.HandleCommand(
                        new TransferToAnotherAccountCommand(settings.Account.GetAccountId(), cash, settings.Account, newAccount), client);
                    break;
                default:
                    AnsiConsole.WriteLine("Invalid operations");
                    break;
            }

            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandOption("-a|--account")]
            [CommandArgument(0, "[MAIBANK]")]
            public MainBank MainBank { get; set; }

            [CommandArgument(0, "[ACCOUNT]")]
            public IAccount Account { get; set; }
        }
    }
}