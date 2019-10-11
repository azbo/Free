using System;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x02000039 RID: 57
	public class Token : IComparable<Token>
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000055C6 File Offset: 0x000037C6
		// (set) Token: 0x06000115 RID: 277 RVA: 0x000055CE File Offset: 0x000037CE
		public int BeginLine
		{
			get
			{
				return this._beginline;
			}
			set
			{
				this._beginline = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000055D7 File Offset: 0x000037D7
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000055DF File Offset: 0x000037DF
		public int BeginColumn
		{
			get
			{
				return this._begincolumn;
			}
			set
			{
				this._begincolumn = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000055E8 File Offset: 0x000037E8
		// (set) Token: 0x06000119 RID: 281 RVA: 0x000055F0 File Offset: 0x000037F0
		public int EndLine
		{
			get
			{
				return this._endline;
			}
			set
			{
				this._endline = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000055F9 File Offset: 0x000037F9
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00005601 File Offset: 0x00003801
		public int EndColumn
		{
			get
			{
				return this._endcolumn;
			}
			set
			{
				this._endcolumn = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000560A File Offset: 0x0000380A
		public string Text
		{
			get
			{
				return this._text;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005612 File Offset: 0x00003812
		public TokenKind TokenKind
		{
			get
			{
				return this._tokenkind;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000561A File Offset: 0x0000381A
		public Token(TokenKind kind, string text)
		{
			this._tokenkind = kind;
			this._text = text;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00005630 File Offset: 0x00003830
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00005638 File Offset: 0x00003838
		public Token Next
		{
			get
			{
				return this._next;
			}
			set
			{
				this._next = value;
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005641 File Offset: 0x00003841
		public override string ToString()
		{
			return this.Text;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000564C File Offset: 0x0000384C
		public int CompareTo(Token other)
		{
			if (this.BeginLine > other.BeginLine)
			{
				return 1;
			}
			if (this.BeginLine < other.BeginLine)
			{
				return -1;
			}
			if (this.BeginColumn > other.BeginColumn)
			{
				return 1;
			}
			if (this.BeginColumn < other.BeginColumn)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x04000077 RID: 119
		private string _text;

		// Token: 0x04000078 RID: 120
		private int _beginline;

		// Token: 0x04000079 RID: 121
		private int _begincolumn;

		// Token: 0x0400007A RID: 122
		private int _endline;

		// Token: 0x0400007B RID: 123
		private int _endcolumn;

		// Token: 0x0400007C RID: 124
		private TokenKind _tokenkind;

		// Token: 0x0400007D RID: 125
		private Token _next;
	}
}
