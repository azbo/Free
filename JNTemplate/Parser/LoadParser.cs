using System;
using JinianNet.JNTemplate.Common;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x0200001B RID: 27
	public class LoadParser : ITagParser
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003984 File Offset: 0x00001B84
		public Tag Parse(TemplateParser parser, TokenCollection tc)
		{
			if (Utility.IsEqual(tc.First.Text, "load") && tc != null && parser != null && tc.Count > 2 && tc[1].TokenKind == TokenKind.LeftParentheses && tc.Last.TokenKind == TokenKind.RightParentheses)
			{
				return new LoadTag
				{
					Path = parser.Read(new TokenCollection(tc, 2, tc.Count - 2))
				};
			}
			return null;
		}
	}
}
