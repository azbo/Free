using System;
using JinianNet.JNTemplate.Common;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x0200001D RID: 29
	public class SetParser : ITagParser
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00003A88 File Offset: 0x00001C88
		public Tag Parse(TemplateParser parser, TokenCollection tc)
		{
			if (tc == null || parser == null)
			{
				return null;
			}
			if (tc.Count > 5 && Utility.IsEqual(tc.First.Text, "set") && tc[1].TokenKind == TokenKind.LeftParentheses && tc[3].Text == "=" && tc.Last.TokenKind == TokenKind.RightParentheses)
			{
				return new SetTag
				{
					Name = tc[2].Text,
					Value = parser.Read(new TokenCollection
					{
						{
							tc,
							4,
							tc.Count - 2
						}
					})
				};
			}
			if (tc.Count == 2 && tc.First.TokenKind == TokenKind.TextData && tc.Last.TokenKind == TokenKind.Operator && (tc.Last.Text == "++" || tc.Last.Text == "--"))
			{
				SetTag setTag = new SetTag();
				setTag.Name = tc.First.Text;
				ExpressionTag c = new ExpressionTag();
				c.AddChild(new VariableTag
				{
					FirstToken = tc.First,
					Name = tc.First.Text
				});
				c.AddChild(new TextTag
				{
					FirstToken = new Token(TokenKind.Operator, tc.Last.Text[0].ToString())
				});
				c.AddChild(new NumberTag
				{
					Value = 1,
					FirstToken = new Token(TokenKind.Number, "1")
				});
				setTag.Value = c;
				return setTag;
			}
			if (tc.Count > 2 && tc.First.TokenKind == TokenKind.TextData && tc[1].Text == "=")
			{
				return new SetTag
				{
					Name = tc.First.Text,
					Value = parser.Read(new TokenCollection
					{
						{
							tc,
							2,
							tc.Count - 1
						}
					})
				};
			}
			return null;
		}
	}
}
