using System;
using JinianNet.JNTemplate.Common;
using JinianNet.JNTemplate.Exception;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x02000017 RID: 23
	public class IfParser : ITagParser
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00003798 File Offset: 0x00001998
		public Tag Parse(TemplateParser parser, TokenCollection tc)
		{
			if (tc == null || parser == null || tc.Count <= 3 || !Utility.IsEqual(tc.First.Text, "if"))
			{
				return null;
			}
			if (tc[1].TokenKind == TokenKind.LeftParentheses && tc.Last.TokenKind == TokenKind.RightParentheses)
			{
				IfTag tag = new IfTag();
				ElseifTag t = new ElseifTag();
				TokenCollection coll = new TokenCollection();
				coll.Add(tc, 2, tc.Count - 2);
				t.Test = parser.Read(coll);
				t.FirstToken = coll.First;
				tag.AddChild(t);
				while (parser.MoveNext())
				{
					if (parser.Current is EndTag)
					{
						tag.AddChild(parser.Current);
						return tag;
					}
					if (parser.Current is ElseifTag || parser.Current is ElseTag)
					{
						tag.AddChild(parser.Current);
					}
					else
					{
						tag.Children[tag.Children.Count - 1].AddChild(parser.Current);
					}
				}
				throw new ParseException("if is not properly closed by a end tag:" + tc, tc.First.BeginLine, tc.First.BeginColumn);
			}
			throw new ParseException("syntax error near if:" + tc, tc.First.BeginLine, tc.First.BeginColumn);
		}
	}
}
