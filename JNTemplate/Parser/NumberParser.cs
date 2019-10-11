using System;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x0200001C RID: 28
	public class NumberParser : ITagParser
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003A00 File Offset: 0x00001C00
		public Tag Parse(TemplateParser parser, TokenCollection tc)
		{
			if (tc != null && tc.Count == 1 && tc.First.TokenKind == TokenKind.Number)
			{
				NumberTag tag = new NumberTag();
				if (tc.First.Text.IndexOf('.') == -1)
				{
					tag.Value = int.Parse(tc.First.Text);
				}
				else
				{
					tag.Value = double.Parse(tc.First.Text);
				}
				return tag;
			}
			return null;
		}
	}
}
