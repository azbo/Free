using System;
using System.Collections.Generic;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x0200000F RID: 15
	public class CharScanner
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002ADD File Offset: 0x00000CDD
		public CharScanner(string text)
		{
			this._document = (text ?? string.Empty);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002AF5 File Offset: 0x00000CF5
		public int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002AFD File Offset: 0x00000CFD
		public bool Next()
		{
			return this.Next(1);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B06 File Offset: 0x00000D06
		public bool Next(int i)
		{
			if (this._index + i > this._document.Length)
			{
				return false;
			}
			this._index += i;
			return true;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B2E File Offset: 0x00000D2E
		public bool Back()
		{
			return this.Back(1);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B37 File Offset: 0x00000D37
		public bool Back(int i)
		{
			if (this._index < i)
			{
				return false;
			}
			this._index -= i;
			return true;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B53 File Offset: 0x00000D53
		public char Read()
		{
			return this.Read(0);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B5C File Offset: 0x00000D5C
		public char Read(int i)
		{
			if (this._index + i >= this._document.Length)
			{
				return '\0';
			}
			return this._document[this._index + i];
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B88 File Offset: 0x00000D88
		public bool IsMatch(char[] list)
		{
			return this.IsMatch(list, 0);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B92 File Offset: 0x00000D92
		public bool IsEnd()
		{
			return this._index >= this._document.Length;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002BAC File Offset: 0x00000DAC
		public bool IsMatch(char[] list, int n)
		{
			n = this._index + n;
			if (this._document.Length >= n + list.Length)
			{
				for (int i = 0; i < list.Length; i++)
				{
					if (this._document[n + i] != list[i])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002BFB File Offset: 0x00000DFB
		public string GetEscapeString()
		{
			string escapeString = this.GetEscapeString(this._start, this._index);
			this._start = this._index;
			return escapeString;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002C1B File Offset: 0x00000E1B
		public string GetString()
		{
			string @string = this.GetString(this._start, this._index);
			this._start = this._index;
			return @string;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002C3C File Offset: 0x00000E3C
		public string GetEscapeString(int x, int y)
		{
			List<char> cs = new List<char>();
			for (int i = x; i < y; i++)
			{
				if (this._document[i] == '\\')
				{
					char c = this._document[i + 1];
					if (c <= 'a')
					{
						if (c <= '0')
						{
							if (c == '"')
							{
								cs.Add('"');
								i++;
								goto IL_14A;
							}
							if (c == '0')
							{
								cs.Add('\0');
								i++;
								goto IL_14A;
							}
						}
						else
						{
							if (c == '\\')
							{
								cs.Add('\\');
								i++;
								goto IL_14A;
							}
							if (c == 'a')
							{
								cs.Add('\a');
								i++;
								goto IL_14A;
							}
						}
					}
					else if (c <= 'f')
					{
						if (c == 'b')
						{
							cs.Add('\b');
							i++;
							goto IL_14A;
						}
						if (c == 'f')
						{
							cs.Add('\f');
							i++;
							goto IL_14A;
						}
					}
					else
					{
						if (c == 'n')
						{
							cs.Add('\n');
							i++;
							goto IL_14A;
						}
						switch (c)
						{
						case 'r':
							cs.Add('\r');
							i++;
							goto IL_14A;
						case 't':
							cs.Add('\t');
							i++;
							goto IL_14A;
						case 'v':
							cs.Add('\v');
							i++;
							goto IL_14A;
						}
					}
					cs.Add(this._document[i]);
				}
				else
				{
					cs.Add(this._document[i]);
				}
				IL_14A:;
			}
			return new string(cs.ToArray());
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002DA9 File Offset: 0x00000FA9
		public string GetString(int x, int y)
		{
			return this._document.Substring(x, y - x);
		}

		// Token: 0x04000051 RID: 81
		private const char EOF = '\0';

		// Token: 0x04000052 RID: 82
		private int _index;

		// Token: 0x04000053 RID: 83
		private int _start;

		// Token: 0x04000054 RID: 84
		private string _document;
	}
}
