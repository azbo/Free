using System;
using System.IO;

namespace JinianNet.JNTemplate
{
	// Token: 0x02000006 RID: 6
	public interface ITemplate
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27
		// (set) Token: 0x0600001C RID: 28
		TemplateContext Context { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29
		// (set) Token: 0x0600001E RID: 30
		string TemplateContent { get; set; }

		// Token: 0x0600001F RID: 31
		void Render(TextWriter writer);
	}
}
