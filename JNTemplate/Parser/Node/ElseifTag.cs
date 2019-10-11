using System;
using System.IO;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x02000026 RID: 38
	public class ElseifTag : TagBase
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004C70 File Offset: 0x00002E70
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00004C78 File Offset: 0x00002E78
		public virtual Tag Test
		{
			get
			{
				return this._test;
			}
			set
			{
				this._test = value;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004C84 File Offset: 0x00002E84
		public override object Parse(TemplateContext context)
		{
			if (base.Children.Count == 1)
			{
				return base.Children[0].Parse(context);
			}
			object result;
			using (StringWriter write = new StringWriter())
			{
				for (int i = 0; i < base.Children.Count; i++)
				{
					base.Children[i].Parse(context, write);
				}
				result = write.ToString();
			}
			return result;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004D08 File Offset: 0x00002F08
		public override void Parse(TemplateContext context, TextWriter write)
		{
			for (int i = 0; i < base.Children.Count; i++)
			{
				base.Children[0].Parse(context, write);
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004D3E File Offset: 0x00002F3E
		public override bool ToBoolean(TemplateContext context)
		{
			return this._test.ToBoolean(context);
		}

		// Token: 0x04000069 RID: 105
		private Tag _test;
	}
}
