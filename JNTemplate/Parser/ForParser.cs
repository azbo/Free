using System;
using System.Collections.Generic;
using JinianNet.JNTemplate.Common;
using JinianNet.JNTemplate.Exception;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x02000015 RID: 21
	public class ForParser : ITagParser
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003400 File Offset: 0x00001600
		public Tag Parse(TemplateParser parser, TokenCollection tc)
		{
			if (tc == null || parser == null || tc.Count <= 3 || !Utility.IsEqual("for", tc.First.Text))
			{
				return null;
			}
			if (tc[1].TokenKind != TokenKind.LeftParentheses || tc.Last.TokenKind != TokenKind.RightParentheses)
			{
				throw new ParseException("syntax error near for:" + tc, tc.First.BeginLine, tc.First.BeginColumn);
			}
			int pos = 0;
			int start = 2;
			List<Tag> ts = new List<Tag>(3);
			ForTag tag = new ForTag();
			for (int i = 2; i < tc.Count - 1; i++)
			{
				int end = i;
				if (tc[i].TokenKind == TokenKind.Punctuation && tc[i].Text == ";" && pos == 0)
				{
					TokenCollection coll = new TokenCollection();
					coll.Add(tc, start, end - 1);
					if (coll.Count > 0)
					{
						ts.Add(parser.Read(coll));
					}
					else
					{
						ts.Add(null);
					}
					start = i + 1;
				}
				else
				{
					if (tc[i].TokenKind == TokenKind.LeftParentheses)
					{
						pos++;
					}
					else if (tc[i].TokenKind == TokenKind.RightParentheses)
					{
						pos--;
					}
					if (i == tc.Count - 2)
					{
						TokenCollection coll2 = new TokenCollection();
						coll2.Add(tc, start, end);
						if (coll2.Count > 0)
						{
							ts.Add(parser.Read(coll2));
						}
						else
						{
							ts.Add(null);
						}
					}
				}
			}
			if (ts.Count != 3)
			{
				throw new ParseException("syntax error near for:" + tc, tc.First.BeginLine, tc.First.BeginColumn);
			}
			tag.Initial = ts[0];
			tag.Test = ts[1];
			tag.Do = ts[2];
			while (parser.MoveNext())
			{
				Tag item = parser.Current;
				tag.Children.Add(item);
				if (parser.Current is EndTag)
				{
					return tag;
				}
			}
			throw new ParseException("for is not properly closed by a end tag:" + tc, tc.First.BeginLine, tc.First.BeginColumn);
		}
	}
}
