using System;
using System.Collections.Generic;
using System.IO;

namespace JinianNet.JNTemplate
{
	// Token: 0x0200000A RID: 10
	public class Template : TemplateRender, ITemplate
	{
		// Token: 0x0600002A RID: 42 RVA: 0x0000265D File Offset: 0x0000085D
		public Template() : this(null)
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002666 File Offset: 0x00000866
		public Template(string text) : this(new TemplateContext(), text)
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002674 File Offset: 0x00000874
		public Template(TemplateContext ctx, string text)
		{
			if (ctx == null)
			{
				throw new ArgumentNullException("\"ctx\" cannot be null.");
			}
			base.Context = ctx;
			base.TemplateContent = text;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002698 File Offset: 0x00000898
		public string Render()
		{
			string document;
			using (StringWriter writer = new StringWriter())
			{
				this.Render(writer);
				document = writer.ToString();
			}
			return document;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026D8 File Offset: 0x000008D8
		public void Set(string key, object value)
		{
			base.Context.TempData[key] = value;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026EC File Offset: 0x000008EC
		public void Set(Dictionary<string, object> dic)
		{
			foreach (KeyValuePair<string, object> value in dic)
			{
				this.Set(value.Key, value.Value);
			}
		}
	}
}
