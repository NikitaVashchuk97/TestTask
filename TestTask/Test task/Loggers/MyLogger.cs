using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_task.Loggers
{
	public class MyLogger : ILogger
	{
		private ILogger _logger;

		public MyLogger(ILogger logger)
		{
			_logger = logger;
		}

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
			return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId,
			TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			var msg = formatter(state, exception);
			_logger.LogInformation("DB-REQUEST: " + msg);
		}
	}
}
