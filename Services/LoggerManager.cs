using NLog;
using Services.Contrats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LoggerManager : ILogerService
    {
        private static ILogger loger=LogManager.GetCurrentClassLogger();
        public void LogDebug(string message)=>loger.Debug(message);

        public void LogError(string message)=>loger.Error(message);

        public void LogInfo(string message)=> loger.Info(message);
      
        public void LogWarning(string message)=>loger.Warn(message);
       
    }
}
