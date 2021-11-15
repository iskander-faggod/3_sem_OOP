using System;
using Banks.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks.ConsoleInterface
{
    public class CreateMainBank : Command<CreateMainBank.Settings>
    {
        public static MainBank MainBank { get; set; } = new MainBank();
        public override int Execute(CommandContext context, Settings settings)
        {
            AnsiConsole.Write("MainBank успешно создан");
            return 0;
        }

        public class Settings : CommandSettings
        {
        }
    }
}