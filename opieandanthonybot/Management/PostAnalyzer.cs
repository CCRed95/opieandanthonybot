using opieandanthonybot.Data;
using opieandanthonybot.Extensions;
using opieandanthonybot.Management.Flair;
using opieandanthonybot.Management.Qualifiers;
using RedditSharp.Things;

namespace opieandanthonybot.Management
{
	public class PostAnalyzer
	{
		protected Post post;

		//public bool ShouldWaitToAnalyze
		//{
		//	get
		//	{
		//		if (post.Title.ToUpper().Contains("[IMM]"))
		//			return false;
		//		var dateDiff = DateTime.UtcNow - post.CreatedUTC;
		//		return dateDiff < AnalysisBufferTime;
		//	}
		//}

		public bool HasAnyLinkFlair => post.HasAnyLinkFlair();

		public bool HasPostBeenAnalyzed => DAL.I.HasPostBeenAnalyzed(post);

		public PostAnalyzer(Post _post)
		{
			post = _post;
		}

		public void PreformAnalysis()
		{
			var currentFlair = post.GetLinkFlair();
			var hasFlairBeenSet = post.HasAnyLinkFlair() && post.HasInvalidLinkFlair();
			var hasInvalidFlair = post.HasInvalidLinkFlair();

			foreach (var rule in FlairRule.Enumerate())
			{
				if (hasFlairBeenSet)
					break;

				if (hasInvalidFlair)
				{
					post.SetFlair(LinkFlair.InvalidFlair);
					post.Comment($"[DEBUG] This post has custom flair set, and is grouped into the \'Other\' category. " +
											 $"If you believe this has been flaired incorrectly, simply edit the flair selection.");
					break;
				}
				if (rule.Value.Qualifier(post))
				{
					if (!post.HasLinkFlair(rule.Value.Flair))
					{
						post.SetFlair(rule.Value.Flair);
						post.Comment($"[DEBUG] This post has triggered qualifier: \'{rule.Name}\'.\r\r" +
												 $"The post has been automatically set to Flair: \'{rule.Value.Flair.Value.FlairText}\'.\r\r" +
												 $"If you believe this has been flaired incorrectly, simply edit the flair selection.");
						hasFlairBeenSet = true;
					}
					else
					{
						post.Comment($"[DEBUG] Flair requirement for \'{rule.Name}\' met.");
						hasFlairBeenSet = true;
					}
				}
			}
			if (!hasFlairBeenSet)
			{
				post.SetFlair(LinkFlair.OpieAndAnthonyRelated);
				post.Comment($"[DEBUG] This post has not triggered any qualifiers, and is " +
				             $"therefore it is assumed to be at least somewhat O&A related.\r\r" +
										 $"The post has been set to Flair: \'{LinkFlair.OpieAndAnthonyRelated.Value.FlairText}\'.\r\r" +
										 $"If you believe this has been flaired incorrectly, simply edit the flair selection.");
				hasFlairBeenSet = true;
			}
			var analysis = DAL.I.SubmitPostAnalysis(post, BotAction.Approve);
			//post.Comment($"[DEBUG] scanned by bot." +
			//						 $"\r\r" +
			//						 $"Post ID: {analysis.PostID}" +
			//						 $"\r\r" +
			//						 $"Scan Time UTC: {analysis.AnalysisTimeUTC}");
			 
		}

		private void RemovePost(FlairRule violatedRule)
		{
			var analysis = DAL.I.SubmitPostAnalysis(post, BotAction.Remove);
			post.Comment(
				$"rule trigged: {violatedRule.Name}. " +
				$"Must have link flair \'{violatedRule.Value.Flair.Value.FlairText}\'." +
				$"\r\r" +
				$"Post ID: {analysis.PostID}" +
				$"\r\r" +
				$"Scan Time UTC: {analysis.AnalysisTimeUTC}");

		}

	}
}
/*		public void PreformAnalysis()
		{
			foreach (var rule in FlairRule.Enumerate())
			{
				if (rule.Value.qualifier(post))
				{
					if (!post.HasLinkFlair(rule.Value.flair))
					{
						RemovePost(rule);
						return;
					}
					else
					{
						post.Comment($"[DEBUG] Flair requirement for \'{rule.Name}\' met");
					}
				}
			}
			if (Equals(post.GetLinkFlair(), LinkFlair.None))
			{
				post.SetFlair(LinkFlair.OpieAndAnthonyRelated);
				post.Comment($"[DEBUG] no flair set, but no rule was triggered." +
									 $"\r\r" +
									 $"Automatically set flair to \'O&A Related\'.");
			}
			var analysis = DAL.I.SubmitPostAnalysis(post, BotAction.Approve);
			post.Comment($"[DEBUG] scanned by bot." +
									 $"\r\r" +
									 $"Post ID: {analysis.PostID}" +
									 $"\r\r" +
									 $"Scan Time UTC: {analysis.AnalysisTimeUTC}");

		}

		private void RemovePost(FlairRule violatedRule)
		{
			var analysis = DAL.I.SubmitPostAnalysis(post, BotAction.Remove);
			post.Comment(
				$"rule trigged: {violatedRule.Name}. " +
				$"Must have link flair \'{violatedRule.Value.flair.Value.FlairText}\'." +
				$"\r\r" +
				$"Post ID: {analysis.PostID}" +
				$"\r\r" +
				$"Scan Time UTC: {analysis.AnalysisTimeUTC}");

		}*/
