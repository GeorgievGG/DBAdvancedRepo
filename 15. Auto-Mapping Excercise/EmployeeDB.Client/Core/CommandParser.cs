using EmployeeDB.Client.Commands.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace EmployeeDB.Client.Core
{
    public class CommandParser
    {
        private readonly IServiceProvider serviceProvider;

        public CommandParser(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommand ParseCommand(string commandStr)
        {
            var commandType = Assembly.GetExecutingAssembly()
                                .GetTypes()
                                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)))
                                .SingleOrDefault(t => t.Name == commandStr + "Command");

            var command = InjectServices(commandType);

            return command;
        }

        private ICommand InjectServices(Type commandType)
        {
            var constructor = commandType.GetConstructors()
                                            .First();
            var constructorParameters = constructor
                                            .GetParameters()
                                            .Select(p => p.ParameterType)
                                            .ToArray();

            var services = constructorParameters
                                .Select(x => serviceProvider.GetService(x))
                                .ToArray();

            var command = (ICommand)constructor.Invoke(services);

            return command;
        }
    }
}
