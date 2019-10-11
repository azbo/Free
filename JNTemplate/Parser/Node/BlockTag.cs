using System;
using System.IO;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x02000024 RID: 36
	public class BlockTag : TagBase
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004BB2 File Offset: 0x00002DB2
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00004BBF File Offset: 0x00002DBF
		public string TemplateKey
		{
			get
			{
				return this._render.TemplateKey;
			}
			set
			{
				this._render.TemplateKey = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004BCD File Offset: 0x00002DCD
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00004BDA File Offset: 0x00002DDA
		public string TemplateContent
		{
			get
			{
				return this._render.TemplateContent;
			}
			set
			{
				this._render.TemplateContent = value;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004BE8 File Offset: 0x00002DE8
		public BlockTag()
		{
			this._render = new TemplateRender();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004BFC File Offset: 0x00002DFC
		public override object Parse(TemplateContext context)
		{
			object result;
			using (StringWriter writer = new StringWriter())
			{
				this.Render(context, writer);
				result = writer.ToString();
			}
			return result;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004C3C File Offset: 0x00002E3C
		public override void Parse(TemplateContext context, TextWriter write)
		{
			this.Render(context, write);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004C46 File Offset: 0x00002E46
		protected void Render(TemplateContext context, TextWriter writer)
		{
			this._render.Context = context;
			this._render.Render(writer);
		}

		// Token: 0x04000068 RID: 104
		private TemplateRender _render;
	}
}
