using System;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                int userId = AnsiConsole.Ask<int>("Enter a user id");
                Guid accountId = AnsiConsole.Ask<Guid>("Enter a account id");
                string bankName = AnsiConsole.Ask<string>("Enter a bank name");
                int cash = AnsiConsole.Ask<int>("Enter transfer value");
                string command = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What type of [green]account[/] you want to create?")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "Repleshment", "Transfer", "Withdrawal",
                        }));
                Bank bank = settings.MainBank.GetBankByName(bankName);
                Client client = bank.GetClientById(userId);
                List<IAccount> accounts = bank.GetAllAccounts();
                IAccount account = accounts.FirstOrDefault(account => account.GetAccountId() == accountId);

                switch (command)
                {
                    case "Repleshment":
                        bank.HandleCommand(
                            new RepleshmentBankCommand(account.GetAccountId(), cash, account), client);
                        AnsiConsole.WriteLine($"Текущий счет {account.GetDeposit()}");
                        break;
                    case "Withdrawal":
                        bank.HandleCommand(
                            new WithdrawalBankCommand(account.GetAccountId(), cash, account), client);
                        AnsiConsole.WriteLine($"Текущий счет {account.GetDeposit()}");

                        break;
                    case "Transfer":
                        Guid id = AnsiConsole.Ask<Guid>("Enter account id");
                        IAccount newAccount = bank.GetAccountById(id);
                        bank.HandleCommand(
                            new TransferToAnotherAccountCommand(account.GetAccountId(), cash, account, newAccount),
                            client);
                        AnsiConsole.WriteLine($"Текущий счет {account.GetDeposit()}");
                        break;
                    default:
                        AnsiConsole.WriteLine("Invalid operations");
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