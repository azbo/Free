using System;
using System.Collections.Generic;
using System.Text;
using JinianNet.JNTemplate.Common;
using JinianNet.JNTemplate.Parser;

namespace JinianNet.JNTemplate
{
	// Token: 0x0200000B RID: 11
	public class TemplateContext
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00002748 File Offset: 0x00000948
		public TemplateContext() : this(null)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002754 File Offset: 0x00000954
		public TemplateContext(VariableScope data)
		{
			this._variableScope = (data ?? new VariableScope());
			this._errors = new List<System.Exception>();
			this._currentPath = null;
			this._throwErrors = Utility.ToBoolean(Engine.GetEnvironmentVariable("ThrowErrors"));
			this._stripWhiteSpace = Utility.ToBoolean(Engine.GetEnvironmentVariable("StripWhiteSpace"));
			string charset;
			if (string.IsNullOrEmpty(charset = Engine.GetEnvironmentVariable("Charset")))
			{
				this._charset = Encoding.UTF8;
				return;
			}
			this._charset = Encoding.GetEncoding(charset);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000027DE File Offset: 0x000009DE
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000027E6 File Offset: 0x000009E6
		public bool StripWhiteSpace
		{
			get
			{
				return this._stripWhiteSpace;
			}
			set
			{
				this._stripWhiteSpace = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000027EF File Offset: 0x000009EF
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000027F7 File Offset: 0x000009F7
		public VariableScope TempData
		{
			get
			{
				return this._variableScope;
			}
			set
			{
				this._variableScope = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002800 File Offset: 0x00000A00
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002808 File Offset: 0x00000A08
		public string CurrentPath
		{
			get
			{
				return this._currentPath;
			}
			set
			{
				this._currentPath = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002811 File Offset: 0x00000A11
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002819 File Offset: 0x00000A19
		public Encoding Charset
		{
			get
			{
				return this._charset;
			}
			set
			{
				this._charset = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002822 File Offset: 0x00000A22
		// (set) Token: 0x0600003B RID: 59 RVA: 0x0000282A File Offset: 0x00000A2A
		public bool ThrowExceptions
		{
			get
			{
				return this._throwErrors;
			}
			set
			{
				this._throwErrors = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002833 File Offset: 0x00000A33
		public virtual System.Exception[] AllErrors
		{
			get
			{
				return this._errors.ToArray();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002840 File Offset: 0x00000A40
		public virtual System.Exception Error
		{
			get
			{
				if (this.AllErrors.Length != 0)
				{
					return this.AllErrors[0];
				}
				return null;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002855 File Offset: 0x00000A55
		public void AddError(System.Exception e)
		{
			if (this.ThrowExceptions)
			{
				throw e;
			}
			this._errors.Add(e);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000286D File Offset: 0x00000A6D
		public void ClearError()
		{
			this._errors.Clear();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000287C File Offset: 0x00000A7C
		public static TemplateContext CreateContext(TemplateContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("\"context\" cannot be null.");
			}
			return new TemplateContext
			{
				TempData = new VariableScope(context.TempData),
				Charset = context.Charset,
				CurrentPath = context.CurrentPath,
				ThrowExceptions = context.ThrowExceptions,
				StripWhiteSpace = context.StripWhiteSpace
			};
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028DD File Offset: 0x00000ADD
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x04000032 RID: 50
		private VariableScope _variableScope;

		// Token: 0x04000033 RID: 51
		private string _currentPath;

		// Token: 0x04000034 RID: 52
		private Encoding _charset;

		// Token: 0x04000035 RID: 53
		private bool _throwErrors;

		// Token: 0x04000036 RID: 54
		private bool _stripWhiteSpace;

		// Token: 0x04000037 RID: 55
		private List<System.Exception> _errors;
	}
}
