namespace opieandanthonybot.Data.ProjectedTypes
{
	public class CSSFlairDefinition
	{
		public string FlairText { get; }
		public string CSSClass { get; }

		public CSSFlairDefinition(string flairText, string cssClass)
		{
			FlairText = flairText;
			CSSClass = cssClass;
		}
	}
}
