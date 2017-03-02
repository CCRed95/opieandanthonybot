using System.Collections.Generic;
using opieandanthonybot.Management.Qualifiers;

namespace opieandanthonybot.Management
{
	public class AnalyzerContext
	{
		private readonly FlairRule[] qualifiers;

		public IEnumerable<FlairRule> Qualifiers => qualifiers;


		public AnalyzerContext(params FlairRule[] _qualifiers)
		{
			qualifiers = _qualifiers;
		}
		
		public static AnalyzerContext Default = new AnalyzerContext(
			);

	}
}
