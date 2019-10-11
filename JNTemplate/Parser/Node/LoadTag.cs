using System;
using System.Collections.Generic;
using System.IO;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x0200002F RID: 47
	public class LoadTag : BlockTag
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000523E File Offset: 0x0000343E
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00005246 File Offset: 0x00003446
		public Tag Path
		{
			get
			{
				return this._path;
			}
			set
			{
				this._path = value;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00005250 File Offset: 0x00003450
		public override object Parse(TemplateContext context)
		{
			object path = this._path.Parse(context);
			this.LoadResource(path, context);
			return base.Parse(context);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000527C File Offset: 0x0000347C
		public override void Parse(TemplateContext context, TextWriter write)
		{
			object path = this._path.Parse(context);
			this.LoadResource(path, context);
			base.Parse(context, write);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000052A8 File Offset: 0x000034A8
		private void LoadResource(object path, TemplateContext context)
		{
			if (path != null)
			{
				IEnumerable<string> paths;
				if (string.IsNullOrEmpty(context.CurrentPath))
				{
					paths = Engine.ResourceDirectories;
				}
				else
				{
					paths = Resources.MergerPaths(Engine.ResourceDirectories, new string[]
					{
						context.CurrentPath
					});
				}
				base.TemplateContent = Resources.Load(paths, path.ToString(), context.Charset);
			}
		}

		// Token: 0x04000071 RID: 113
		private Tag _path;
	}
}
