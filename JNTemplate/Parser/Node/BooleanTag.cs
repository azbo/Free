using System;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x02000025 RID: 37
	public class BooleanTag : TypeTag<bool>
	{
		// Token: 0x060000BC RID: 188 RVA: 0x00004C60 File Offset: 0x00002E60
		public override bool ToBoolean(TemplateContext context)
		{
			return base.Value;
		}
	}
}
