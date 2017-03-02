using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using opieandanthonybot.Collections;
using opieandanthonybot.Data.ProjectedTypes;
using opieandanthonybot.Extensions;
using opieandanthonybot.Management.Flair;
using RedditSharp.Things;

namespace opieandanthonybot.Management.Qualifiers
{
	public class FlairRule : FlexEnum<FlairRuleDefinition>
	{
		public static FlairRule Shitpost = new FlairRule(
			post =>
			{
				if (post.Title.Length < 10)
				{
					if (post.IsSelfPost)
						return post.SelfText.Length < 10;
					return true;
				}
				return post.ContainsAnyText("shitpost", "shit");
			}, 
			LinkFlair.Shitpost);

		public static FlairRule BrotherJoe = new FlairRule(
			post =>
			{
				return post.ContainsAnyText("brother joe", "brojoe");
			},
			LinkFlair.BrotherJoe);

		public static FlairRule Politics = new FlairRule(
			post =>
			{
				return post.ContainsAnyText("politics", "trump", "hilary", "clinton");
			},
			LinkFlair.Politics);

		public static FlairRule Society = new FlairRule(
			post =>
			{
				return post.ContainsAnyText("amy schumer", "schumer", "big amy", "lena dunham");
			},
			LinkFlair.Society);
		

		private FlairRule(Func<Post, bool> qualifier, LinkFlair flair,
			[CallerMemberName] string fieldName = null,
			[CallerLineNumber] int line = 0) : base(new FlairRuleDefinition(qualifier, flair), fieldName, line)
		{
		}

		public static IEnumerable<FlairRule> Enumerate()
		{
			return Enumerate<FlairRule>();
		}
	}
}
