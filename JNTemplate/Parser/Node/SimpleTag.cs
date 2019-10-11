using System;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x02000034 RID: 52
	public abstract class SimpleTag : TagBase
	{
		// Token: 0x060000FF RID: 255
		public abstract object Parse(object baseValue, TemplateContext context);
	}
}
