using System;
using System.Collections.Generic;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x0200002E RID: 46
	public class IncludeTag : TagBase
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000051AD File Offset: 0x000033AD
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x000051B5 File Offset: 0x000033B5
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

		// Token: 0x060000E7 RID: 231 RVA: 0x000051C0 File Offset: 0x000033C0
		private string LoadResource(object path, TemplateContext context)
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
				return Resources.Load(paths, path.ToString(), context.Charset);
			}
			return null;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00005214 File Offset: 0x00003414
		public override object Parse(TemplateContext context)
		{
			object path = this._path.Parse(context);
			return this.LoadResource(path, context);
		}

		// Token: 0x04000070 RID: 112
		private Tag _path;
	}
}
