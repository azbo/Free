using System;
using JinianNet.JNTemplate.Dynamic;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x0200003C RID: 60
	public class VariableTag : SimpleTag
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005947 File Offset: 0x00003B47
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000594F File Offset: 0x00003B4F
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

		// Token: 0x06000143 RID: 323 RVA: 0x00005958 File Offset: 0x00003B58
		public override object Parse(TemplateContext context)
		{
			return context.TempData[this._name];
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000596B File Offset: 0x00003B6B
		public override object Parse(object baseValue, TemplateContext context)
		{
			if (baseValue == null)
			{
				return null;
			}
			return DynamicHelper.GetPropertyOrField(baseValue, this._name);
		}

		// Token: 0x04000080 RID: 128
		private string _name;
	}
}
