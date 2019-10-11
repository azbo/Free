using System;
using System.IO;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x0200002B RID: 43
	public class ForTag : TagBase
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004F2C File Offset: 0x0000312C
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004F34 File Offset: 0x00003134
		public Tag Initial
		{
			get
			{
				return this._initial;
			}
			set
			{
				this._initial = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004F3D File Offset: 0x0000313D
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00004F45 File Offset: 0x00003145
		public Tag Test
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004F4E File Offset: 0x0000314E
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004F56 File Offset: 0x00003156
		public Tag Do
		{
			get
			{
				return this._dothing;
			}
			set
			{
				this._dothing = value;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004F60 File Offset: 0x00003160
		private void Excute(TemplateContext context, TextWriter writer)
		{
			this._initial.Parse(context);
			bool run = this._test != null && this._test.ToBoolean(context);
			while (run)
			{
				for (int i = 0; i < base.Children.Count; i++)
				{
					base.Children[i].Parse(context, writer);
				}
				if (this._dothing != null)
				{
					this._dothing.Parse(context);
				}
				run = (this._test == null || this._test.ToBoolean(context));
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004FF0 File Offset: 0x000031F0
		public override object Parse(TemplateContext context)
		{
			object result;
			using (StringWriter write = new StringWriter())
			{
				this.Excute(context, write);
				result = write.ToString();
			}
			return result;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005030 File Offset: 0x00003230
		public override void Parse(TemplateContext context, TextWriter write)
		{
			this.Excute(context, write);
		}

		// Token: 0x0400006C RID: 108
		private Tag _initial;

		// Token: 0x0400006D RID: 109
		private Tag _test;

		// Token: 0x0400006E RID: 110
		private Tag _dothing;
	}
}
