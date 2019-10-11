using System;
using System.IO;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x02000038 RID: 56
	public class TextTag : Tag
	{
		// Token: 0x06000110 RID: 272 RVA: 0x0000555E File Offset: 0x0000375E
		public override object Parse(TemplateContext context)
		{
			if (context.StripWhiteSpace)
			{
				return (this.ToString() ?? string.Empty).Trim();
			}
			return this.ToString();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005584 File Offset: 0x00003784
		public override void Parse(TemplateContext context, TextWriter write)
		{
			string value = this.ToString();
			if (context.StripWhiteSpace && value != null)
			{
				value = value.Trim();
			}
			write.Write(value);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000055B1 File Offset: 0x000037B1
		public override string ToString()
		{
			return base.FirstToken.ToString();
		}
	}
}
