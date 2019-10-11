using System;
using JinianNet.JNTemplate.Dynamic;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x0200002C RID: 44
	public class FunctaionTag : SimpleTag
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005042 File Offset: 0x00003242
		// (set) Token: 0x060000DF RID: 223 RVA: 0x0000504A File Offset: 0x0000324A
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00005054 File Offset: 0x00003254
		public override object Parse(TemplateContext context)
		{
			object[] args = new object[base.Children.Count];
			for (int i = 0; i < base.Children.Count; i++)
			{
				args[i] = base.Children[i].Parse(context);
			}
			object result = context.TempData[this._name];
			if (result != null && result is FuncHandler)
			{
				return (result as FuncHandler)(args);
			}
			return null;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000050C8 File Offset: 0x000032C8
		public override object Parse(object baseValue, TemplateContext context)
		{
			if (baseValue != null)
			{
				object[] args = new object[base.Children.Count];
				for (int i = 0; i < base.Children.Count; i++)
				{
					args[i] = base.Children[i].Parse(context);
				}
				object result = DynamicHelper.ExcuteMethod(baseValue, this._name, args);
				if (result != null)
				{
					return result;
				}
				result = DynamicHelper.GetPropertyOrField(baseValue, this._name);
				if (result != null && result is FuncHandler)
				{
					return (result as FuncHandler)(args);
				}
			}
			return null;
		}

		// Token: 0x0400006F RID: 111
		private string _name;
	}
}
