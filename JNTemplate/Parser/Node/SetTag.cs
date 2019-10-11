using System;
using System.IO;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x02000033 RID: 51
	public class SetTag : TagBase
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000053D2 File Offset: 0x000035D2
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x000053DA File Offset: 0x000035DA
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000053E3 File Offset: 0x000035E3
		// (set) Token: 0x060000FB RID: 251 RVA: 0x000053EB File Offset: 0x000035EB
		public Tag Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000053F4 File Offset: 0x000035F4
		public override object Parse(TemplateContext context)
		{
			object value = this.Value.Parse(context);
			if (!context.TempData.SetValue(this.Name, value))
			{
				context.TempData.Push(this.Name, value);
			}
			return null;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005435 File Offset: 0x00003635
		public override void Parse(TemplateContext context, TextWriter write)
		{
			this.Parse(context);
		}

		// Token: 0x04000072 RID: 114
		private string _name;

		// Token: 0x04000073 RID: 115
		private Tag _value;
	}
}
