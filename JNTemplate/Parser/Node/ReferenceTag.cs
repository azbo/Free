using System;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x02000032 RID: 50
	public class ReferenceTag : SimpleTag
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00005324 File Offset: 0x00003524
		public override object Parse(TemplateContext context)
		{
			if (base.Children.Count > 0)
			{
				object result = base.Children[0].Parse(context);
				int i = 1;
				while (i < base.Children.Count && result != null)
				{
					result = ((SimpleTag)base.Children[i]).Parse(result, context);
					i++;
				}
				return result;
			}
			return null;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005388 File Offset: 0x00003588
		public override object Parse(object baseValue, TemplateContext context)
		{
			object result = baseValue;
			int i = 0;
			while (i < base.Children.Count && result != null)
			{
				result = ((SimpleTag)base.Children[i]).Parse(result, context);
				i++;
			}
			return result;
		}
	}
}
