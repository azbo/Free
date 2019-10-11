using System;
using JinianNet.JNTemplate.Common;
using JinianNet.JNTemplate.Exception;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x02000012 RID: 18
	public class ElseifParser : ITagParser
	{
		// Token: 0x06000061 RID: 97 RVA: 0x000031BC File Offset: 0x000013BC
		public Tag Parse(TemplateParser parser, TokenCollection tc)
		{
			if (tc == null || parser == null || tc.Count <= 3 || !Utility.IsEqual(tc.First.Text, "elseif"))
			{
				return null;
			}
			if (tc[1].TokenKind == TokenKind.LeftParentheses && tc.Last.TokenKind == TokenKind.RightParentheses)
			{
				return new ElseifTag
				{
					Test = parser.Read(new TokenCollection
					{
						{
							tc,
							2,
							tc.Count - 2
						}
					})
				};
			}
			throw new ParseException("syntax error near if:" + tc, tc.First.BeginLine, tc.First.BeginColumn);
		}
	}
}
