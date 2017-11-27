namespace PhotoShare.Client.Core
{
    using PhotoShare.Services.Contracts;
    using System;
    using System.Linq;

    public class Engine
    {
        private readonly CommandDispatcher commandDispatcher;
        private readonly IServiceProvider serviceProvider;

        public Engine(CommandDispatcher commandDispatcher, IServiceProvider serviceProvider)
        {
            this.commandDispatcher = commandDispatcher;
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            Console.WriteLine("Engine started. DB Reset in process...");
            var DbInitializer = (IDbInitializer)serviceProvider.GetService(typeof(IDbInitializer));
            DbInitializer.Reset();
            //DbInitializer.Initialize();
            Console.WriteLine("DB reset executed successfully");
            while (true)
            {
                try
                {
                    string input = Console.ReadLine().Trim();
                    string[] data = input.Split(' ');
                    var command = this.commandDispatcher.DispatchCommand(data);
                    var commandParams = data.Skip(1).ToArray();
                    var result = string.Empty;
                    result = command.Execute(commandParams);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(result);
                    Console.ResetColor();
                }
                catch (Exception e)
                {
                    if (e.InnerException is null)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(e.InnerException.Message);
                        Console.ResetColor();
                    }
                }
            }
        }
    }
}
