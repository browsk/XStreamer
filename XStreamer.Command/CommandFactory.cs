using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using XStreamer.Protocol.Message;

namespace XStreamer.Command
{
    public class CommandFactory
    {
        public static ICommand<TForMessage> CreateCommand<TForMessage>() where TForMessage : class, IMessage, new()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            IList<Type> types = assembly.GetTypes();

            foreach (var type in types)
            {
                foreach (var @interface in type.GetInterfaces())
                {
                    if (@interface.IsGenericType
                        && @interface.GetGenericTypeDefinition().Name.StartsWith("ICommand"))
                    {
                        Type messageType = @interface.GetGenericArguments().First();

                        if (messageType == typeof(TForMessage))
                        {
                            ConstructorInfo info = type.GetConstructor(new Type[] {});

                            return info.Invoke(null) as ICommand<TForMessage>;
                        }
                    }
                }
            }

            throw new CommandNotFoundException(typeof (TForMessage));
        }
    }
}
