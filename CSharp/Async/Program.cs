using System;
using System.Threading;
using System.Threading.Tasks;

namespace Async
{
	class Program
	{
		static bool CheckLoginInformation()
		{
			Console.WriteLine("Checking login information...");
			Thread.Sleep(3000);
			return true;
		}

		static string DownloadAvatar()
		{
			Console.WriteLine("Downloading avatar...");
			Thread.Sleep(5000);
			return "Avatar URL";
		}

		static void WriteLog()
		{
			Console.WriteLine("Writing log...");
			Thread.Sleep(2000);
		}
		static async Task Main(string[] args)
		{
			var x = Task.Run(CheckLoginInformation);
			var y = Task.Run(DownloadAvatar);
			bool result = await x;
			string url = await y;

			var z = Task.Run(WriteLog);
			Console.WriteLine("In the meantime, I'm doing some other code...");
			await z;

			Console.WriteLine("Done!");
		}

	}
}
