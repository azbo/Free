using System;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x0200001E RID: 30
	public class StringParser : ITagParser
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00003CAC File Offset: 0x00001EAC
		public Tag Parse(TemplateParser parser, TokenCollection tc)
		{
			if (tc != null && tc.Count == 3 && tc.First.TokenKind == TokenKind.StringStart && tc[1].TokenKind == TokenKind.String && tc.Last.TokenKind == TokenKind.StringEnd)
			{
				return new StringTag
				{
					Value = tc[1].Text
				};
			}
			return null;
		}
	}
}
