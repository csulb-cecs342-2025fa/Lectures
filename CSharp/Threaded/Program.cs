using System;
using System.Threading;

namespace Threaded
{
	class Program
	{
		static bool loginResult;
		static void CheckLoginInformation()
		{
			Console.WriteLine("Checking login information...");
			Thread.Sleep(3000);
			loginResult = true;
		}

		static string avatarResult;
		static void DownloadAvatar()
		{
			Console.WriteLine("Downloading avatar...");
			Thread.Sleep(5000);
			avatarResult = "Avatar Url...";
		}

		static void WriteLog()
		{
			Console.WriteLine($"Writing log: login {loginResult}, avatar {avatarResult}");
			Thread.Sleep(2000);
		}

		static void Main(string[] args)
		{
			Thread login = new Thread(CheckLoginInformation);
			Thread avatar = new Thread(DownloadAvatar);

			login.Start();
			avatar.Start();
			login.Join();
			avatar.Join();

			Thread log = new Thread(WriteLog);
			log.Start();
			Console.WriteLine("In the meantime, I'm doing some other code...");
			log.Join();
			Console.WriteLine("Done!");
		}
	}
}
