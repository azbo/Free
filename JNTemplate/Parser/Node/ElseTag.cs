using System;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x02000027 RID: 39
	public class ElseTag : ElseifTag
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x00004D54 File Offset: 0x00002F54
		public override bool ToBoolean(TemplateContext context)
		{
			return true;
		}
	}
}
