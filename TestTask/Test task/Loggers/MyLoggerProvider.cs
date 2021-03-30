using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_task.Loggers
{
    public class MyLoggerProvider : ILoggerProvider
    {
		private ILogger _logger;

		public MyLoggerProvider(ILogger logger)
		{
			_logger = logger;
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new MyLogger(_logger);
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}
