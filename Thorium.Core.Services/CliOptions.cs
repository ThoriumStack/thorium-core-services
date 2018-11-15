using McMaster.Extensions.CommandLineUtils;

namespace Thorium.Core.Services
{
    public class CliOptions
    {
        private readonly string _appInfoApplicationName;
        private CommandLineApplication _commandLineApp;

        public CliOptions(string appInfoApplicationName)
        {
            _appInfoApplicationName = appInfoApplicationName;
            _commandLineApp = new CommandLineApplication();
            ApplicationName = _appInfoApplicationName;
        }

        /// <summary>
        /// The name of the command line application. Defaults to the Assembly name.
        /// </summary>
        public string ApplicationName { get; set; }
        
        
        
        


    }
}