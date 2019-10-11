using System;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x02000022 RID: 34
	public class VariableParser : ITagParser
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00004A43 File Offset: 0x00002C43
		public Tag Parse(TemplateParser parser, TokenCollection tc)
		{
			if (tc != null && tc.Count == 1 && tc.First.TokenKind == TokenKind.TextData)
			{
				return new VariableTag
				{
					Name = tc.First.Text
				};
			}
			return null;
		}
	}
}
