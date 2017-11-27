using PhotoShare.Client.Core.Contracts;
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace PhotoShare.Client.Core
{

    public class CommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICommand DispatchCommand(string[] commandParameters)
        {
            var commandTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)))
                .ToArray();

            if (commandParameters[0] == "MakeFriends")
            {
                commandParameters[0] = "AddFriend";
            }

            var commandType = commandTypes.SingleOrDefault(ct => ct.Name == $"{commandParameters[0]}Command");

            if (commandType == null)
            {
                throw new InvalidOperationException("Invalid command!");
            }

            var command = InjectServices(commandType);

            return command;
        }

        private ICommand InjectServices(Type commandType)
        {
            var commandConstructor = commandType.GetConstructors().First();

            var reqParameters = commandConstructor
                                    .GetParameters()
                                    .Select(p => p.ParameterType)
                                    .ToArray();

            var services = reqParameters
                            .Select(sp => serviceProvider.GetService(sp))
                            .ToArray();

            var command = (ICommand)commandConstructor.Invoke(services);

            return command;
        }
    }
}
