using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opieandanthonybot.Configuration
{
	public sealed class Config
	{
		public static readonly string configFilePath =
			Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\BotSettings.json";

		private BotConfigFileReader _botConfigFileReader;

		#region Singleton
		private static Config instance;
		public static Config I => instance ?? (instance = new Config());



		private Config()
		{
			var fileInfo = new FileInfo(configFilePath);
			if (fileInfo.Exists)
			{
				CurrentRunMode = BotRunMode.Verified;
				_botConfigFileReader = new BotConfigFileReader(fileInfo);
			}
			else
			{
				CurrentRunMode = BotRunMode.Simulated;
				_botConfigFileReader = null;
			}
		}
		#endregion


		public const string Username = "opieandanthonybot";
		
		public const string SubredditName = "/r/opieandanthonytest";

		public const int ScannerSampleSize = 20;

		public static readonly TimeSpan AnalyzerDispatchTime = TimeSpan.FromSeconds(5);

		public static readonly TimeSpan AnalysisBufferTime = TimeSpan.FromMinutes(5);


		public BotRunMode CurrentRunMode { get; }

		public string ServerConnectionString
		{
			get { return _botConfigFileReader.ConnectionString; }
		}

		public string RedditPassword
		{
			get { return _botConfigFileReader.RedditPassword; }
		}

	}
}
