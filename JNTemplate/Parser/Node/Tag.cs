using System;
using System.Collections.ObjectModel;
using System.IO;
using JinianNet.JNTemplate.Common;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x02000036 RID: 54
	public abstract class Tag
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005467 File Offset: 0x00003667
		public Collection<Tag> Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x06000104 RID: 260
		public abstract object Parse(TemplateContext context);

		// Token: 0x06000105 RID: 261
		public abstract void Parse(TemplateContext context, TextWriter write);

		// Token: 0x06000106 RID: 262 RVA: 0x0000546F File Offset: 0x0000366F
		public virtual bool ToBoolean(TemplateContext context)
		{
			return ExpressionEvaluator.CalculateBoolean(this.Parse(context));
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000547D File Offset: 0x0000367D
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00005485 File Offset: 0x00003685
		public Token FirstToken
		{
			get
			{
				return this._first;
			}
			set
			{
				this._first = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00005497 File Offset: 0x00003697
		// (set) Token: 0x06000109 RID: 265 RVA: 0x0000548E File Offset: 0x0000368E
		public Token LastToken
		{
			get
			{
				return this._last;
			}
			set
			{
				this._last = value;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000549F File Offset: 0x0000369F
		public void AddChild(Tag node)
		{
			if (node != null)
			{
				this.Children.Add(node);
			}
		}

		// Token: 0x04000074 RID: 116
		private Token _first;

		// Token: 0x04000075 RID: 117
		private Token _last;

		// Token: 0x04000076 RID: 118
		private Collection<Tag> _children = new Collection<Tag>();
	}
}
