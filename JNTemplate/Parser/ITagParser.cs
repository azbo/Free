using System;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x02000019 RID: 25
	public interface ITagParser
	{
		// Token: 0x0600006F RID: 111
		Tag Parse(TemplateParser parser, TokenCollection tc);
	}
}
