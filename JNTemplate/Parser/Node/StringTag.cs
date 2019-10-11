using System;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x02000035 RID: 53
	public class StringTag : TypeTag<string>
	{
		// Token: 0x06000101 RID: 257 RVA: 0x0000544F File Offset: 0x0000364F
		public override bool ToBoolean(TemplateContext context)
		{
			return !string.IsNullOrEmpty(base.Value);
		}
	}
}
