using InsuranceCorp.Data;
using Microsoft.Extensions.Logging;

namespace InsuranceCorp.Blazor.Services
{
	public class TimedHostedService : IHostedService, IDisposable
	{
		private readonly ILogger<TimedHostedService> _logger;
		private readonly EmailService emailService;
		private readonly IServiceProvider services;
		private Timer? _timer = null;

		public TimedHostedService(ILogger<TimedHostedService> logger,
			EmailService emailService,
			IServiceProvider services)
		{
			_logger = logger;
			this.emailService = emailService;
			this.services = services;
		}

		public Task StartAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("Timed Hosted Service running.");

			_timer = new Timer(DoWork, null, TimeSpan.Zero,
				TimeSpan.FromSeconds(15));

			return Task.CompletedTask;
		}

		private void DoWork(object? state)
		{

			using (var scope = services.CreateScope())
			{
				var db =
					scope.ServiceProvider
						.GetRequiredService<InsCorpDbContext>();

				while (emailService.Emails.TryDequeue(out var email))
				{
					_logger.LogInformation($"posílám email {email}");
					//db. 
				}
			}

			
	
		}

		public Task StopAsync(CancellationToken stoppingToken)
		{
			_logger.LogInformation("Timed Hosted Service is stopping.");

			_timer?.Change(Timeout.Infinite, 0);

			return Task.CompletedTask;
		}

		public void Dispose()
		{
			_timer?.Dispose();
		}
	}
}
