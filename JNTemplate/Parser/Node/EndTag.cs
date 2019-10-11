using System;
using System.IO;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x02000028 RID: 40
	public class EndTag : TagBase
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00004D5F File Offset: 0x00002F5F
		public override object Parse(TemplateContext context)
		{
			return null;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004D62 File Offset: 0x00002F62
		public override bool ToBoolean(TemplateContext context)
		{
			return false;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004D65 File Offset: 0x00002F65
		public override void Parse(TemplateContext context, TextWriter write)
		{
		}
	}
}
