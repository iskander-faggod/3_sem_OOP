using System;
using Banks.Entities;
using Banks.Entities.AccountsModel.Creator;
using Banks.Entities.ClientModel;
using Banks.Tools;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.ConsoleInterface
{
    public class CreateAccount : Command<CreateAccount.Settings>
    {
        public override int Execute(CommandContext context, Settings settings)
        {
            try
            {
                int userId = AnsiConsole.Ask<int>("Enter a user id");
                string bankName = AnsiConsole.Ask<string>("Enter a bank name");
                string account = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What type of [green]account[/] you want to create?")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "Debit", "Credit", "Deposit",
                        }));

                if (userId == default || string.IsNullOrEmpty(bankName) || string.IsNullOrEmpty(account))
                    throw new BanksException("Invalid account data");
                Bank bank = settings.MainBank.GetBankByName(bankName);
                Client client = bank.GetClientById(userId);

                switch (account)
                {
                    case "Debit":
                        IAccount newAccount1 = bank.CreateDebitAccount(client);
                        bank.AddAccountToClient(client, newAccount1);
                        AnsiConsole.WriteLine("Ваш номер аккаунта " + $"{newAccount1.GetAccountId()}");
                        break;
                    case "Credit":
                        IAccount newAccount2 = bank.CreateCreditAccount(client);
                        bank.AddAccountToClient(client, newAccount2);
                        AnsiConsole.WriteLine("Ваш номер аккаунта " + $"{newAccount2.GetAccountId()}");
                        break;
                    case "Deposit":
                        IAccount newAccount3 = bank.CreateDepositAccount(client);
                        bank.AddAccountToClient(client, newAccount3);
                        AnsiConsole.WriteLine("Ваш номер аккаунта " + $"{newAccount3.GetAccountId()}");

                        break;
                    default:
                        AnsiConsole.WriteLine("Account can't be created");
                        break;
                }
            }
            catch (Exception e)
            {
                AnsiConsole.WriteException(e);
            }

            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandOption("-a|--account")]
            public MainBank MainBank { get; } = CreateMainBank.MainBank;
        }
    }
}