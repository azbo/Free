using System;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x0200001A RID: 26
	public interface ITagTypeResolver
	{
		// Token: 0x06000070 RID: 112
		Tag Resolver(TemplateParser parser, TokenCollection tc);
	}
}
