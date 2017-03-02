using System.Linq;
using opieandanthonybot.Configuration;
using opieandanthonybot.Management;
using RedditSharp;
using RedditSharp.Things;
using static opieandanthonybot.Configuration.Config;

namespace opieandanthonybot.Data
{
	public class API
	{
		#region Singleton
		private static API instance;
		public static API I => instance ?? (instance = new API());

		protected API()
		{
			redditProxy = new Reddit();
			redditProxy.LogIn(Username, Config.I.RedditPassword);

			subredditProxy = redditProxy.GetSubreddit(SubredditName);
		}
		#endregion

		private Reddit redditProxy { get; }
		private Subreddit subredditProxy { get; }

		public void ScanRecentPosts()
		{
			foreach (var analyzer in subredditProxy.New
				.Take(ScannerSampleSize).Select(post => new PostAnalyzer(post)))
			{
				//analyzer.ShouldWaitToAnalyze ||
				if (!analyzer.HasAnyLinkFlair)
				{
					analyzer.PreformAnalysis();
				}
				if (analyzer.HasPostBeenAnalyzed)
					continue;
				analyzer.PreformAnalysis();
			}
		}

	}
}