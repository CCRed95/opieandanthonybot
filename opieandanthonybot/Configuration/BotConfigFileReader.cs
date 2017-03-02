using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace opieandanthonybot.Configuration
{
	public class BotConfigFileReader
	{
		protected FileInfo FileInfo { get; }
		private JObject jObject;

		public string ConnectionString
		{
			get { return (string)jObject.SelectToken(nameof(ConnectionString)); }
		}
		public string RedditPassword
		{
			get { return (string)jObject.SelectToken(nameof(RedditPassword)); }
		}

		public BotConfigFileReader(FileInfo fileInfo)
		{
			
			FileInfo = fileInfo;
			using (var sr = FileInfo.OpenText())
			{
				var fileText = sr.ReadToEnd();
				jObject = JObject.Parse(fileText);
			}
		}
	}
}
