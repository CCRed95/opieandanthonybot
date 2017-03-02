using System.Linq;
using opieandanthonybot.Management.Flair;
using RedditSharp.Things;

namespace opieandanthonybot.Extensions
{
	public static class PostExtensions
	{
		public static LinkFlair GetLinkFlair(this Post @this)
		{
			if (string.IsNullOrEmpty(@this.LinkFlairText))
				return LinkFlair.None;

			var searchText = @this.LinkFlairText;
			if (searchText.Contains("&amp;"))
			{
				searchText = searchText.Replace("&amp;", "&");
			}
			var matchingFlair = LinkFlair.Enumerate().Where(t => t.Value.FlairText == searchText).ToArray();
			if (matchingFlair.Length != 1)
				return LinkFlair.InvalidFlair;

			return matchingFlair[0];
		}


		public static bool HasAnyLinkFlair(this Post @this)
		{
			return !Equals(LinkFlair.None, @this.GetLinkFlair());
		}
		public static bool HasInvalidLinkFlair(this Post @this)
		{
			return Equals(LinkFlair.InvalidFlair, @this.GetLinkFlair());
		}

		public static bool HasLinkFlair(this Post @this, LinkFlair flair)
		{
			return Equals(flair, @this.GetLinkFlair());
		}

		public static void SetFlair(this Post @this, LinkFlair flair)
		{
			@this.SetFlair(flair.Value.FlairText, flair.Value.CSSClass);
		}

		
		//TODO case sensitive, better comparison, add regex
		public static bool ContainsText(this Post @this, string text, bool caseSensitive = false)
		{
			if (@this.Title.ToLower().Contains(text))
				return true;
			if (@this.IsSelfPost)
			{
				if (@this.SelfText.ToLower().Contains(text))
					return true;
			}
			return false;
		}
		public static bool ContainsAnyText(this Post @this, params string[] textArray)
		{
			return textArray.Any(text => @this.ContainsText(text));
		}
	}
}
