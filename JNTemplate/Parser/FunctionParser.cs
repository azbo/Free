using System;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x02000016 RID: 22
	public class FunctionParser : ITagParser
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00003648 File Offset: 0x00001848
		public Tag Parse(TemplateParser parser, TokenCollection tc)
		{
			if (tc != null && parser != null && tc.First.TokenKind == TokenKind.TextData && tc.Count > 2 && tc[1].TokenKind == TokenKind.LeftParentheses && tc.Last.TokenKind == TokenKind.RightParentheses)
			{
				FunctaionTag tag = new FunctaionTag();
				tag.Name = tc.First.Text;
				int pos = 0;
				int start = 2;
				for (int i = 2; i < tc.Count; i++)
				{
					int end = i;
					TokenKind tokenKind = tc[i].TokenKind;
					if (tokenKind == TokenKind.Comma)
					{
						if (pos == 0)
						{
							TokenCollection coll = new TokenCollection();
							coll.Add(tc, start, end - 1);
							if (coll.Count > 0)
							{
								tag.AddChild(parser.Read(coll));
							}
							start = i + 1;
						}
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
						if (i == tc.Count - 1)
						{
							TokenCollection coll2 = new TokenCollection();
							coll2.Add(tc, start, end - 1);
							if (coll2.Count > 0)
							{
								tag.AddChild(parser.Read(coll2));
							}
						}
					}
				}
				return tag;
			}
			return null;
		}
	}
}
