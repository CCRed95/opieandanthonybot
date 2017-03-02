using System.Collections.Generic;
using System.Runtime.CompilerServices;
using opieandanthonybot.Collections;
using opieandanthonybot.Data.ProjectedTypes;

namespace opieandanthonybot.Management.Flair
{
	public class LinkFlair : FlexEnum<CSSFlairDefinition>
	{

		public static LinkFlair InvalidFlair = new LinkFlair("Other", "other");

		public static LinkFlair None = new LinkFlair(null, null);

		public static LinkFlair OpieAndAnthonyRelated = new LinkFlair("O&A Related", "oandarelated");

		public static LinkFlair Shitpost = new LinkFlair("Shitpost", "shitpost");

		public static LinkFlair BrotherJoe = new LinkFlair("Brother Joe", "brotherjoe");

		public static LinkFlair Politics = new LinkFlair("Politics", "politics");

		public static LinkFlair Society = new LinkFlair("Society", "society");


		private LinkFlair(string flairText, 
			string cssClass,
			[CallerMemberName] string fieldName = null,
			[CallerLineNumber] int line = 0) : base(new CSSFlairDefinition(flairText, cssClass), fieldName, line)
		{
		} 

		public static IEnumerable<LinkFlair> Enumerate()
		{
			return Enumerate<LinkFlair>();
		}
	}
}
