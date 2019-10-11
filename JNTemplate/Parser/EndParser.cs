using System;
using JinianNet.JNTemplate.Common;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x02000013 RID: 19
	public class EndParser : ITagParser
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00003270 File Offset: 0x00001470
		public Tag Parse(TemplateParser parser, TokenCollection tc)
		{
			if (tc != null && tc.Count == 1 && Utility.IsEqual(tc.First.Text, "end"))
			{
				return new EndTag();
			}
			return null;
		}
	}
}
