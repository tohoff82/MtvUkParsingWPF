using System;
using Microsoft.Extensions.DependencyInjection;
using MtvCoUkParser;

namespace WpfMtvUk
{
    public static class Injections
    {
        public static IServiceProvider Provider { get; private set; }

        public static void Startup()
            => Provider = new ServiceCollection()
                .AddTransient<IMtvDriver, MtvDriver>()
                .BuildServiceProvider();
    }
}
