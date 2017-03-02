using System;
using opieandanthonybot.Management.Flair;
using RedditSharp.Things;

namespace opieandanthonybot.Data.ProjectedTypes
{
	public class FlairRuleDefinition
	{
		public Func<Post, bool> Qualifier { get; }

		public LinkFlair Flair { get; }

		public FlairRuleDefinition(Func<Post, bool> qualifier, LinkFlair flair)
		{
			Qualifier = qualifier;
			Flair = flair;
		}
	}
}
