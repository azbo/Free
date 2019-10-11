namespace JinianNet.JNTemplate.Exception
{
	// Token: 0x0200003F RID: 63
	public class TemplateException : System.Exception
    {
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000059C2 File Offset: 0x00003BC2
		// (set) Token: 0x0600014C RID: 332 RVA: 0x000059CA File Offset: 0x00003BCA
		public int Line
		{
			get
			{
				return this._errorLine;
			}
			set
			{
				this._errorLine = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000059D3 File Offset: 0x00003BD3
		// (set) Token: 0x0600014E RID: 334 RVA: 0x000059DB File Offset: 0x00003BDB
		public int Column
		{
			get
			{
				return this._errorColumn;
			}
			set
			{
				this._errorColumn = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000059E4 File Offset: 0x00003BE4
		// (set) Token: 0x06000150 RID: 336 RVA: 0x000059EC File Offset: 0x00003BEC
		public string Code
		{
			get
			{
				return this._errorCode;
			}
			set
			{
				this._errorCode = value;
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000059F5 File Offset: 0x00003BF5
		public TemplateException()
		{
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005A00 File Offset: 0x00003C00
		public TemplateException(string message, int line, int column) : base(string.Concat(new string[]
		{
			"Line:",
			line.ToString(),
			" Column:",
			column.ToString(),
			"\r\n",
			message
		}))
		{
			this._errorColumn = column;
			this._errorLine = line;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005A5C File Offset: 0x00003C5C
		public TemplateException(string message) : base(message)
		{
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005A65 File Offset: 0x00003C65
		public TemplateException(string message, System.Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x04000081 RID: 129
		private int _errorLine;

		// Token: 0x04000082 RID: 130
		private int _errorColumn;

		// Token: 0x04000083 RID: 131
		private string _errorCode;
	}
}
