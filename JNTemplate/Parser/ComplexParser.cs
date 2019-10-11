using System;
using System.Collections.Generic;
using JinianNet.JNTemplate.Exception;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x02000010 RID: 16
	public class ComplexParser : ITagParser
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002DBC File Offset: 0x00000FBC
		public Tag Parse(TemplateParser parser, TokenCollection tc)
		{
			if (tc != null && parser != null && tc.Count > 2)
			{
				int start;
				int pos = start = 0;
				bool isFunc = false;
				List<Token> data = new List<Token>();
				Queue<TokenCollection> queue = new Queue<TokenCollection>();
				for (int i = 0; i < tc.Count; i++)
				{
					int end = i;
					if (tc[i].TokenKind == TokenKind.LeftParentheses)
					{
						if (pos == 0 && i > 0 && tc[i - 1].TokenKind == TokenKind.TextData)
						{
							isFunc = true;
						}
						pos++;
					}
					else if (tc[i].TokenKind == TokenKind.RightParentheses)
					{
						if (pos <= 0)
						{
							throw new ParseException("syntax error near ):" + tc, data[i].BeginLine, data[i].BeginColumn);
						}
						pos--;
						if (pos == 0)
						{
							TokenCollection coll = new TokenCollection();
							if (!isFunc)
							{
								coll.Add(tc, start + 1, end - 1);
							}
							else
							{
								coll.Add(tc, start, end);
							}
							queue.Enqueue(coll);
							data.Add(null);
							start = i + 1;
						}
					}
					else if (pos == 0 && (tc[i].TokenKind == TokenKind.Dot || tc[i].TokenKind == TokenKind.Operator))
					{
						if (end > start)
						{
							queue.Enqueue(new TokenCollection
							{
								{
									tc,
									start,
									end - 1
								}
							});
							data.Add(null);
						}
						start = i + 1;
						data.Add(tc[i]);
					}
					if (i == tc.Count - 1 && end >= start)
					{
						if (start == 0 && end == i)
						{
							throw new ParseException("Unexpected  tag:" + tc, tc[0].BeginLine, tc[0].BeginColumn);
						}
						queue.Enqueue(new TokenCollection
						{
							{
								tc,
								start,
								end
							}
						});
						data.Add(null);
						start = i + 1;
					}
				}
				if (queue.Count == 1 && queue.Peek().Equals(tc))
				{
					return null;
				}
				List<Tag> tags = new List<Tag>();
				for (int j = 0; j < data.Count; j++)
				{
					if (data[j] == null)
					{
						tags.Add(parser.Read(queue.Dequeue()));
					}
					else if (data[j].TokenKind == TokenKind.Dot)
					{
						if (tags.Count == 0 || j == data.Count - 1 || data[j + 1] != null)
						{
							throw new ParseException("syntax error near .:" + tc, data[j].BeginLine, data[j].BeginColumn);
						}
						if (tags[tags.Count - 1] is ReferenceTag)
						{
							tags[tags.Count - 1].AddChild(parser.Read(queue.Dequeue()));
						}
						else
						{
							ReferenceTag t = new ReferenceTag();
							t.AddChild(tags[tags.Count - 1]);
							t.AddChild(parser.Read(queue.Dequeue()));
							tags[tags.Count - 1] = t;
						}
						j++;
					}
					else if (data[j].TokenKind == TokenKind.Operator)
					{
						tags.Add(new TextTag());
						tags[tags.Count - 1].FirstToken = data[j];
					}
				}
				if (tags.Count == 1)
				{
					return tags[0];
				}
				if (tags.Count > 1)
				{
					ExpressionTag t2 = new ExpressionTag();
					for (int k = 0; k < tags.Count; k++)
					{
						t2.AddChild(tags[k]);
					}
					tags.Clear();
					return t2;
				}
			}
			return null;
		}
	}
}
