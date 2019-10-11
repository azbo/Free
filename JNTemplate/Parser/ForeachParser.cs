using System;
using JinianNet.JNTemplate.Common;
using JinianNet.JNTemplate.Exception;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x02000014 RID: 20
	public class ForeachParser : ITagParser
	{
		// Token: 0x06000065 RID: 101 RVA: 0x000032A4 File Offset: 0x000014A4
		public Tag Parse(TemplateParser parser, TokenCollection tc)
		{
			if (tc == null || parser == null || tc.Count <= 0 || !Utility.IsEqual("foreach", tc.First.Text))
			{
				return null;
			}
			if (tc.Count > 5 && tc[1].TokenKind == TokenKind.LeftParentheses && tc[2].TokenKind == TokenKind.TextData && Utility.IsEqual(tc[3].Text, "in") && tc.Last.TokenKind == TokenKind.RightParentheses)
			{
				ForeachTag tag = new ForeachTag();
				tag.Name = tc[2].Text;
				tag.Source = parser.Read(new TokenCollection
				{
					{
						tc,
						4,
						tc.Count - 2
					}
				});
				while (parser.MoveNext())
				{
					Tag item = parser.Current;
					tag.Children.Add(item);
					if (parser.Current is EndTag)
					{
						return tag;
					}
				}
				throw new ParseException("foreach is not properly closed by a end tag:" + tc, tc.First.BeginLine, tc.First.BeginColumn);
			}
			throw new ParseException("syntax error near foreach:" + tc, tc.First.BeginLine, tc.First.BeginColumn);
		}
	}
}
