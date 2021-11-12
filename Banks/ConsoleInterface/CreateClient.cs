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
            var bankName = AnsiConsole.Ask<string>("Enter a [green]bank name[/] - ");
            settings.Name = AnsiConsole.Ask<string>("Enter a [green]client name[/] - ");
            settings.Surname = AnsiConsole.Ask<string>("Enter a [green]client surname[/] - ");
            string address = AnsiConsole.Ask<string>("Enter a [green]client address[/] - ");
            string passportId = AnsiConsole.Ask<string>("Enter a [green]client passportId[/] - ");
            if (string.IsNullOrEmpty(bankName) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(passportId)) throw new BanksException("Invalid input data");
            var client = new Client(settings.Name, settings.Surname, address, passportId);
            settings.MainBank.GetBankByName(bankName).AddClient(client);
            settings.MainBank.AddNewClient(client);
            AnsiConsole.Write($"Клиент с именем {settings.Name} {settings.Surname} создан");
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