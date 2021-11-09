using System.ComponentModel;
using Banks.Entities.ClientModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.ConsoleInterface
{
    public class CreateClient : Command<CreateClient.Settings>
    {
        public override int Execute(CommandContext context, Settings settings)
        {
            settings.Name = AnsiConsole.Ask<string>("Enter a [green]client name[/] - ");
            settings.Surname = AnsiConsole.Ask<string>("Enter a [green]client surname[/] - ");
            string address = AnsiConsole.Ask<string>("Enter a [green]client address[/] - ");
            string passportId = AnsiConsole.Ask<string>("Enter a [green]client passportId[/] - ");
            var client = new Client(settings.Name, settings.Surname, address, passportId);
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
        }
    }
}