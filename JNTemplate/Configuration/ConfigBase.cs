using System;
using System.Collections.Generic;
using System.Reflection;

namespace JinianNet.JNTemplate.Configuration
{
	// Token: 0x02000046 RID: 70
	public class ConfigBase
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00005F81 File Offset: 0x00004181
		// (set) Token: 0x06000178 RID: 376 RVA: 0x00005F89 File Offset: 0x00004189
		[Variable]
		public string Charset
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00005F92 File Offset: 0x00004192
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00005F9A File Offset: 0x0000419A
		public string[] ResourceDirectories
		{
			get
			{
				return this._resourceDirectories;
			}
			set
			{
				this._resourceDirectories = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00005FA3 File Offset: 0x000041A3
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00005FAB File Offset: 0x000041AB
		[Variable]
		public string TagPrefix
		{
			get
			{
				return this._tagPrefix;
			}
			set
			{
				this._tagPrefix = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00005FB4 File Offset: 0x000041B4
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00005FBC File Offset: 0x000041BC
		[Variable]
		public string TagSuffix
		{
			get
			{
				return this._tagSuffix;
			}
			set
			{
				this._tagSuffix = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00005FC5 File Offset: 0x000041C5
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00005FCD File Offset: 0x000041CD
		[Variable]
		public char TagFlag
		{
			get
			{
				return this._tagFlag;
			}
			set
			{
				this._tagFlag = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00005FD6 File Offset: 0x000041D6
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00005FDE File Offset: 0x000041DE
		[Variable]
		public bool ThrowExceptions
		{
			get
			{
				return this._throwExceptions;
			}
			set
			{
				this._throwExceptions = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00005FE7 File Offset: 0x000041E7
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00005FEF File Offset: 0x000041EF
		[Variable]
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005FF8 File Offset: 0x000041F8
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00006000 File Offset: 0x00004200
		[Variable]
		public bool IgnoreCase
		{
			get
			{
				return this._ignoreCase;
			}
			set
			{
				this._ignoreCase = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00006009 File Offset: 0x00004209
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00006011 File Offset: 0x00004211
		[Variable]
		public string CachingProvider
		{
			get
			{
				return this._cachingProvider;
			}
			set
			{
				this._cachingProvider = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000601A File Offset: 0x0000421A
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00006022 File Offset: 0x00004222
		public string[] TagParsers
		{
			get
			{
				return this._tagParsers;
			}
			set
			{
				this._tagParsers = value;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000602C File Offset: 0x0000422C
		public virtual Dictionary<string, string> ToDictionary()
		{
			Dictionary<string, string> dic = new Dictionary<string, string>();
			foreach (PropertyInfo pi in ((IEnumerable<PropertyInfo>)TypeExtensions.GetProperties(base.GetType())))
			{
				dic[pi.Name] = (pi.GetValue(this, null) ?? string.Empty).ToString();
			}
			return dic;
		}

		// Token: 0x0400008A RID: 138
		private string[] _resourceDirectories;

		// Token: 0x0400008B RID: 139
		private char _tagFlag;

		// Token: 0x0400008C RID: 140
		private string _tagPrefix;

		// Token: 0x0400008D RID: 141
		private string _tagSuffix;

		// Token: 0x0400008E RID: 142
		private bool _throwExceptions;

		// Token: 0x0400008F RID: 143
		private bool _stripWhiteSpace;

		// Token: 0x04000090 RID: 144
		private bool _ignoreCase;

		// Token: 0x04000091 RID: 145
		private string _charset;

		// Token: 0x04000092 RID: 146
		private string _cachingProvider;

		// Token: 0x04000093 RID: 147
		private string[] _tagParsers;
	}
}
