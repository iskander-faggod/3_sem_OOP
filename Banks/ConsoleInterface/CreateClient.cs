using System;
using System.ComponentModel;
using Banks.Entities;
using Banks.Entities.ClientModel;
using Banks.Tools;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.ConsoleInterface
{
    public class CreateClient : Command<CreateClient.Settings>
    {
        public override int Execute(CommandContext context, Settings settings)
        {
            try
            {
                string bankName = AnsiConsole.Ask<string>("Enter a [green]bank name[/] - ");
                settings.Name = AnsiConsole.Ask<string>("Enter a [green]client name[/] - ");
                settings.Surname = AnsiConsole.Ask<string>("Enter a [green]client surname[/] - ");
                string address = AnsiConsole.Ask<string>("Enter a [green]client address[/] - ");
                int passportId = AnsiConsole.Ask<int>("Enter a [green]client passportId[/] - ");
                if (string.IsNullOrEmpty(bankName) || string.IsNullOrEmpty(address) || passportId == default)
                    throw new BanksException("Invalid input data");
                var client = new Client(settings.Name, settings.Surname, address, passportId);
                settings.MainBank.GetBankByName(bankName).AddClient(client);
                settings.MainBank.AddNewClient(client);
                AnsiConsole.Write($"Клиент с именем {settings.Name} {settings.Surname} создан");
            }
            catch (Exception e)
            {
                AnsiConsole.WriteException(e);
            }

            return 0;
        }

        public class Settings : CommandSettings
        {
            [CommandOption("-c|--client")]
            [CommandArgument(0, "[NAME]")]
            public string Name { get; set; }

            [CommandArgument(1, "[SURNAME]")]
            public string Surname { get; set; }
            public MainBank MainBank { get; } = CreateMainBank.MainBank;
        }
    }
}