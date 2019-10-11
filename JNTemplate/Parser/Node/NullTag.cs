using System;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x02000030 RID: 48
	public class NullTag : TagBase
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00005307 File Offset: 0x00003507
		public override object Parse(TemplateContext context)
		{
			return null;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000530A File Offset: 0x0000350A
		public override bool ToBoolean(TemplateContext context)
		{
			return false;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000530D File Offset: 0x0000350D
		public override string ToString()
		{
			return string.Empty;
		}
	}
}
