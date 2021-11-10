using Banks.Entities;
using Spectre.Console.Cli;

namespace Banks.ConsoleInterface
{
    public class CreateMainBank : Command<CommandSettings>
    {
        public static MainBank MainBank { get; set; }

        public override int Execute(CommandContext context, CommandSettings settings)
        {
            MainBank = new MainBank();
            return 0;
        }
    }
}