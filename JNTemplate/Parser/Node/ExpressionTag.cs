using System;
using JinianNet.JNTemplate.Common;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x02000029 RID: 41
	public class ExpressionTag : TagBase
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00004D70 File Offset: 0x00002F70
		public override object Parse(TemplateContext context)
		{
			object[] value = new object[base.Children.Count];
			for (int i = 0; i < base.Children.Count; i++)
			{
				if (base.Children[i] is TextTag)
				{
					value[i] = OperatorConvert.Parse(base.Children[i].Parse(context).ToString());
				}
				else
				{
					value[i] = base.Children[i].Parse(context);
				}
			}
			return ExpressionEvaluator.Calculate(ExpressionEvaluator.ProcessExpression(value));
		}
	}
}
