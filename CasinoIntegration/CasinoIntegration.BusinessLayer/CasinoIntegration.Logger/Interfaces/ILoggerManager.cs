using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoIntegration.BusinessLayer.CasinoIntegration.Logger.Interfaces
{
    public interface ILoggerManager
    {
        /// <summary>
        /// Method for logging information message
        /// </summary>
        /// <param name="message"></param>
        void LogInfo(string message);

        /// <summary>
        /// Method for logging warning message
        /// </summary>
        /// <param name="message"></param>
        void LogWarn(string message);

        /// <summary>
        /// Method for logging debug message
        /// </summary>
        /// <param name="message"></param>
        void LogDebug(string message);

        /// <summary>
        /// Method for logging error messages
        /// </summary>
        /// <param name="message"></param>
        void LogError(string message);
    }
}
