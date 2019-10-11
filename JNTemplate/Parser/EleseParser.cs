using System;
using JinianNet.JNTemplate.Common;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x02000011 RID: 17
	public class EleseParser : ITagParser
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003185 File Offset: 0x00001385
		public Tag Parse(TemplateParser parser, TokenCollection tc)
		{
			if (tc != null && parser != null && tc.Count == 1 && Utility.IsEqual(tc.First.Text, "else"))
			{
				return new ElseTag();
			}
			return null;
		}
	}
}
