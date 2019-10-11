using System;
using JinianNet.JNTemplate.Common;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x02000018 RID: 24
	public class IncludeParser : ITagParser
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003908 File Offset: 0x00001B08
		public Tag Parse(TemplateParser parser, TokenCollection tc)
		{
			if (Utility.IsEqual(tc.First.Text, "include") && tc != null && parser != null && tc.Count > 2 && tc[1].TokenKind == TokenKind.LeftParentheses && tc.Last.TokenKind == TokenKind.RightParentheses)
			{
				return new IncludeTag
				{
					Path = parser.Read(new TokenCollection(tc, 2, tc.Count - 2))
				};
			}
			return null;
		}
	}
}
