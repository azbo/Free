using System;
using System.IO;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x0200003B RID: 59
	public abstract class TypeTag<T> : Tag
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000058FB File Offset: 0x00003AFB
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00005903 File Offset: 0x00003B03
		public T Value
		{
			get
			{
				return this._baseValue;
			}
			set
			{
				this._baseValue = value;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000590C File Offset: 0x00003B0C
		public override object Parse(TemplateContext context)
		{
			return this._baseValue;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005919 File Offset: 0x00003B19
		public override void Parse(TemplateContext context, TextWriter write)
		{
			if (this._baseValue != null)
			{
				write.Write(this._baseValue.ToString());
			}
		}

		// Token: 0x0400007F RID: 127
		private T _baseValue;
	}
}
