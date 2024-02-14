using System.Collections.Concurrent;

namespace InsuranceCorp.Blazor.Services
{
	public class EmailService
	{
		public ConcurrentQueue<string> Emails { get; set; }
	}
}
