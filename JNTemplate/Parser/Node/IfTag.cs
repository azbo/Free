using System;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x0200002D RID: 45
	public class IfTag : TagBase
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00005158 File Offset: 0x00003358
		public override object Parse(TemplateContext context)
		{
			for (int i = 0; i < base.Children.Count - 1; i++)
			{
				if (base.Children[i].ToBoolean(context))
				{
					return base.Children[i].Parse(context);
				}
			}
			return null;
		}
	}
}
