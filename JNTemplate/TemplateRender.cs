using System;
using System.IO;
using JinianNet.JNTemplate.Exception;
using JinianNet.JNTemplate.Parser;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate
{
	// Token: 0x0200000C RID: 12
	public class TemplateRender
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000028E5 File Offset: 0x00000AE5
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000028ED File Offset: 0x00000AED
		public string TemplateKey
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000028F6 File Offset: 0x00000AF6
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000028FE File Offset: 0x00000AFE
		public TemplateContext Context
		{
			get
			{
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002907 File Offset: 0x00000B07
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000290F File Offset: 0x00000B0F
		public string TemplateContent
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002918 File Offset: 0x00000B18
		public virtual void Render(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("\"writer\" cannot be null.");
			}
			if (this._content == null)
			{
				return;
			}
			Tag[] collection = null;
			if (!string.IsNullOrEmpty(this._content))
			{
				if (Engine.Cache != null && !string.IsNullOrEmpty(this._key))
				{
					object value;
					if ((value = Engine.Cache.Get(this._key)) != null)
					{
						collection = (Tag[])value;
					}
					else
					{
						collection = this.ParseTag();
						Engine.Cache.Set(this._key, collection);
					}
				}
				else
				{
					collection = this.ParseTag();
				}
			}
			else
			{
				collection = new Tag[0];
			}
			if (collection.Length != 0)
			{
				for (int i = 0; i < collection.Length; i++)
				{
					try
					{
						collection[i].Parse(this._context, writer);
					}
					catch (TemplateException e)
					{
						this.ThrowException(e, collection[i], writer);
					}
					catch (System.Exception ex2)
					{
                        System.Exception baseException = ex2.GetBaseException();
						ParseException ex = new ParseException(baseException.Message, baseException);
						this.ThrowException(ex, collection[i], writer);
					}
				}
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A1C File Offset: 0x00000C1C
		private Tag[] ParseTag()
		{
			return new TemplateParser(new TemplateLexer(this._content).Parse()).ToArray();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002A38 File Offset: 0x00000C38
		private void ThrowException(TemplateException e, Tag tag, TextWriter writer)
		{
			if (this._context.ThrowExceptions)
			{
				throw e;
			}
			this._context.AddError(e);
			writer.Write(tag.ToString());
		}

		// Token: 0x04000038 RID: 56
		private TemplateContext _context;

		// Token: 0x04000039 RID: 57
		private string _content;

		// Token: 0x0400003A RID: 58
		private string _key;
	}
}
