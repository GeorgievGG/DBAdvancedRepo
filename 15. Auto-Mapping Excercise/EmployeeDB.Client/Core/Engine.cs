using EmployeeDB.Services.Contracts;
using System;
using System.Linq;

namespace EmployeeDB.Client.Core
{
    public class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            Console.WriteLine("Initializing Database...");
            var DbInitSrv = (IDbInitializerService)serviceProvider.GetService(typeof(IDbInitializerService));
            DbInitSrv.Initialize();
            Console.WriteLine("DB Initialized successfully!");

            while (true)
            {
                var inputParams = Console.ReadLine().Split();

                var commandStr = inputParams.First();

                var data = inputParams.Skip(1).ToArray();

                var commandInterpreter = new CommandParser(serviceProvider);

                var command = commandInterpreter.ParseCommand(commandStr);

                var result = command.Execute(data);

                Console.WriteLine(result);
            }
        }
    }
}
