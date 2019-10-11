using System;
using System.Collections;
using System.IO;
using JinianNet.JNTemplate.Dynamic;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x0200002A RID: 42
	public class ForeachTag : TagBase
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004E05 File Offset: 0x00003005
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00004E0D File Offset: 0x0000300D
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004E16 File Offset: 0x00003016
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00004E1E File Offset: 0x0000301E
		public Tag Source
		{
			get
			{
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004E28 File Offset: 0x00003028
		private void Excute(object value, TemplateContext context, TextWriter writer)
		{
			IEnumerable enumerable = DynamicHelper.ToIEnumerable(value);
			if (enumerable != null)
			{
				IEnumerator ienum = enumerable.GetEnumerator();
				TemplateContext ctx = TemplateContext.CreateContext(context);
				int i = 0;
				while (ienum.MoveNext())
				{
					i++;
					ctx.TempData[this._name] = ienum.Current;
					ctx.TempData["foreachIndex"] = i;
					for (int j = 0; j < base.Children.Count; j++)
					{
						base.Children[j].Parse(ctx, writer);
					}
				}
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004EB8 File Offset: 0x000030B8
		public override void Parse(TemplateContext context, TextWriter writer)
		{
			if (this.Source != null)
			{
				this.Excute(this.Source.Parse(context), context, writer);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004ED8 File Offset: 0x000030D8
		public override object Parse(TemplateContext context)
		{
			object result;
			using (StringWriter write = new StringWriter())
			{
				this.Excute(this.Source.Parse(context), context, write);
				result = write.ToString();
			}
			return result;
		}

		// Token: 0x0400006A RID: 106
		private string _name;

		// Token: 0x0400006B RID: 107
		private Tag _source;
	}
}
