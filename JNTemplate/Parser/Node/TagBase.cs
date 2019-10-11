using System;
using System.IO;
using System.Text;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x02000037 RID: 55
	public abstract class TagBase : Tag
	{
		// Token: 0x0600010D RID: 269 RVA: 0x000054C4 File Offset: 0x000036C4
		public override string ToString()
		{
			if (base.LastToken != null && base.FirstToken != base.LastToken)
			{
				StringBuilder sb = new StringBuilder();
				Token t = base.FirstToken;
				sb.Append(t.ToString());
				while ((t = t.Next) != null && t != base.LastToken)
				{
					sb.Append(t.ToString());
				}
				sb.Append(base.LastToken.ToString());
				return sb.ToString();
			}
			return base.FirstToken.ToString();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005547 File Offset: 0x00003747
		public override void Parse(TemplateContext context, TextWriter write)
		{
			write.Write(this.Parse(context));
		}
	}
}
