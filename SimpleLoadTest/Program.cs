using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HitAdminStaging
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Console.Out.Write("Username: ");
			var userName = Console.ReadLine();
			Console.Out.Write("Password: ");
			var password = GetPassword();
			Console.Out.WriteLine("");
			Console.Out.Write("Impersonate: ");
			var impersonate = Console.ReadLine();

			if (string.IsNullOrEmpty(impersonate))
			{
				impersonate = userName;
			}
			Console.Out.WriteLine("");

			Console.Out.WriteLine("Here we go...");

			RunTests(userName, password, impersonate);

			Console.Out.WriteLine("Tasks finished...");
			Console.ReadLine();
		}

		private static void RunTests(string userName, string password, string impersonate)
		{
			var tasks = Enumerable.Range(0, 25)
			                      .Select(x => new Task(() => RunCasper(userName, password, impersonate)))
			                      .ToArray();

			foreach (var task in tasks)
			{
				task.Start();
			}

			Task.WaitAll(tasks);
		}


		private static void RunCasper(string userName, string password, string impersonate)
		{
			var path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "loginAndNavigateToAFewPlaces.js");
			var startInfo = new ProcessStartInfo
				                {
					                FileName = @"C:\casperjs\batchbin\casperjs.bat",
					                Arguments = string.Format("{0} {1} {2} {3}", path, userName, password, impersonate),
					                RedirectStandardOutput = false,
					                CreateNoWindow = false,
					                UseShellExecute = false,
				                };
			startInfo.EnvironmentVariables["PATH"] += @"C:\casperjs;C:\phantomjs;C:\casperjs\batchbin;";
			Process.Start(startInfo).WaitForExit();
		}

		public static string GetPassword()
		{
			var pwd = new StringBuilder();
			while (true)
			{
				ConsoleKeyInfo i = Console.ReadKey(true);
				if (i.Key == ConsoleKey.Enter)
				{
					break;
				}
				else if (i.Key == ConsoleKey.Backspace)
				{
					pwd.Remove(pwd.Length - 1, 1);
					Console.Write("\b \b");
				}
				else
				{
					pwd.Append(i.KeyChar);
					Console.Write("*");
				}
			}
			return pwd.ToString();
		}
	}
}