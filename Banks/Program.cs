using System;
using System.Collections.Generic;
using Banks.Commands;
using Banks.ConsoleInterface;
using Banks.Entities;
using Banks.Entities.AccountsModel;
using Banks.Entities.AccountsModel.Creator;
using Banks.Entities.ClientModel;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Banks
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var rule = new Rule("[red]Всем привет! Давайте начнем использовать и создавать банки[/]");
            AnsiConsole.Write(rule);
            var app = new CommandApp();
            app.Configure(config =>
            {
                config.AddCommand<CreateClient>("createClient");
                config.AddCommand<CreateBank>("createBank");
                config.AddCommand<CreateAccount>("createAccount");
                config.AddCommand<CreateOperations>("createOperations");
            });
            app.Run(args);
            while (true)
            {
                string command = Console.ReadLine();
                AnsiConsole.WriteLine();
                app.Run(command.Split(" "));
            }
        }
    }
}

// TODO : Команды не должны быть связаны, каждая команда делает свое действие
// TODO : Команда создания, команда транзакции, команда таблички
// TODO : ПРОВЕРИТЬ СИГНАТУРЫ АККАУНТОВ
// TODO : UI для команд оберрунть