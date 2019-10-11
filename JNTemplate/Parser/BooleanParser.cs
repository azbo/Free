using System;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x0200000E RID: 14
	public class BooleanParser : ITagParser
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002A6C File Offset: 0x00000C6C
		public Tag Parse(TemplateParser parser, TokenCollection tc)
		{
			if (tc != null && tc.Count == 1 && (tc.First.Text == "true" || tc.First.Text == "false"))
			{
				return new BooleanTag
				{
					Value = (tc.First.Text == "true")
				};
			}
			return null;
		}
	}
}
