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
			}
			var analysis = DAL.I.SubmitPostAnalysis(post, BotAction.Approve);
			 
		}

	}
}