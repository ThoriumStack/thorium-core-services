using System;

namespace Thorium.Core.Services.Abstractions
{
    public interface IPlatformHandler
    {
        /// <summary>
        /// Listen to OS events to enable termination of application.
        /// </summary>
        /// <param name="shutdownServices"></param>
        /// <returns></returns>
        void Execute(Action shutdownServices);
    }
}