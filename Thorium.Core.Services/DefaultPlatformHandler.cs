using System;
using System.Runtime.InteropServices;
using System.Threading;
using Thorium.Core.Services.Abstractions;
using Thorium.Core.Services.Platform;

namespace Thorium.Core.Services
{
    public class DefaultPlatformHandler : IPlatformHandler
    {
        private Action _shutdownServices;
        
        public void Execute(Action shutdownServices)
        {
            _shutdownServices = shutdownServices;
            var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            var handler = new UnixSignalHandler();

            if (!isWindows)
            {
                handler.WaitForSignal(new System.Collections.Generic.List<Mono.Unix.Native.Signum>
                {
                    Mono.Unix.Native.Signum.SIGQUIT, // quit signal
                    Mono.Unix.Native.Signum.SIGTERM, // terminate signal
                    Mono.Unix.Native.Signum.SIGHUP // hangup signal
                }, Exit);
            }
            else
            {
                Console.CancelKeyPress += (sender, args) =>
                {
                   Exit();
                };
                Thread.Sleep(Timeout.Infinite);
            }
        }

        private void Exit()
        {
            _shutdownServices();
            Environment.Exit(0);
        }
    }
}