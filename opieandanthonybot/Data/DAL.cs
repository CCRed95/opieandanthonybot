using System;
using System.Linq;
using opieandanthonybot.Configuration;
using opieandanthonybot.Data.Context;
using opieandanthonybot.Data.Domain;
using opieandanthonybot.Management;
using RedditSharp.Things;

namespace opieandanthonybot.Data
{
	public class DAL
	{
		#region Singleton
		private static DAL instance;
		public static DAL I => instance ?? (instance = new DAL());
		#endregion

		public bool HasPostBeenAnalyzed(Post post)
		{
			if (Config.I.CurrentRunMode == BotRunMode.Simulated)
			{
				return false;
			}
			using (var context = new ServerDataContext(Config.I.ServerConnectionString))
			{
				return context.AnalyzedPosts.Any(t => t.PostID == post.Id);
			}
		}
		public AnalyzedPost SubmitPostAnalysis(Post post, BotAction action)
		{
			if (Config.I.CurrentRunMode == BotRunMode.Simulated)
			{
				return new AnalyzedPost()
				{
					
				};
			}
			using (var context = new ServerDataContext(Config.I.ServerConnectionString))
			{
				var analyzedPost = new AnalyzedPost
				{
					PostID = post.Id,
					AnalysisTimeUTC = DateTime.UtcNow,
					BotAction = (int)action
				};
				context.AnalyzedPosts.InsertOnSubmit(analyzedPost);
				context.SubmitChanges();

				return analyzedPost;
			}
		}

	}
}
