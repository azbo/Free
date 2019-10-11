using System;
using System.Collections;
using System.Collections.Generic;
using JinianNet.JNTemplate.Exception;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x02000021 RID: 33
	public class TemplateParser : IEnumerator<Tag>, IEnumerator, IDisposable
	{
		// Token: 0x06000097 RID: 151 RVA: 0x000047B7 File Offset: 0x000029B7
		public TemplateParser(Token[] ts)
		{
			if (ts == null)
			{
				throw new ArgumentNullException("\"ts\" cannot be null.");
			}
			this._tokens = ts;
			this.Reset();
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000047DA File Offset: 0x000029DA
		public Tag Current
		{
			get
			{
				return this._tag;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000047E4 File Offset: 0x000029E4
		public bool MoveNext()
		{
			if (this._index < this._tokens.Length)
			{
				Tag t = this.Read();
				if (t != null)
				{
					this._tag = t;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004815 File Offset: 0x00002A15
		public void Reset()
		{
			this._index = 0;
			this._tag = null;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004828 File Offset: 0x00002A28
		private Tag Read()
		{
			Tag t = null;
			if (this.IsTagStart())
			{
				Token t3;
				Token t2 = t3 = this.GetToken();
				TokenCollection tc = new TokenCollection();
				do
				{
					this._index++;
					t2.Next = this.GetToken();
					t2 = t2.Next;
					tc.Add(t2);
				}
				while (!this.IsTagEnd());
				tc.Remove(tc.Last);
				this._index++;
				try
				{
					t = this.Read(tc);
				}
				catch (TemplateException)
				{
					throw;
				}
				catch (System.Exception e)
				{
					throw new ParseException(string.Concat(new object[]
					{
						"Parse error:",
						tc,
						"\r\nError message:",
						e.Message
					}), tc.First.BeginLine, tc.First.BeginColumn);
				}
				if (t == null)
				{
					throw new ParseException("Unexpected  tag:" + tc, tc.First.BeginLine, tc.First.BeginColumn);
				}
				t.FirstToken = t3;
				if (t.Children.Count == 0 || t.LastToken == null || t2.CompareTo(t.LastToken) > 0)
				{
					t.LastToken = t2;
				}
			}
			else
			{
				t = new TextTag();
				t.FirstToken = this.GetToken();
				t.LastToken = null;
				this._index++;
			}
			return t;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004994 File Offset: 0x00002B94
		public Tag Read(TokenCollection tc)
		{
			if (tc == null || tc.Count == 0)
			{
				throw new ParseException("Invalid TokenCollection!");
			}
			return Engine.TagResolver.Resolver(this, tc);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000049B8 File Offset: 0x00002BB8
		private bool IsTagEnd()
		{
			return this.IsTagEnd(this.GetToken());
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000049C6 File Offset: 0x00002BC6
		private bool IsTagStart()
		{
			return this.IsTagStart(this.GetToken());
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000049D4 File Offset: 0x00002BD4
		private bool IsTagEnd(Token t)
		{
			return t == null || t.TokenKind == TokenKind.TagEnd || t.TokenKind == TokenKind.EOF;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000049EE File Offset: 0x00002BEE
		private bool IsTagStart(Token t)
		{
			return t.TokenKind == TokenKind.TagStart;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000049F9 File Offset: 0x00002BF9
		private Token GetToken()
		{
			return this._tokens[this._index];
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004A08 File Offset: 0x00002C08
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004A10 File Offset: 0x00002C10
		public void Dispose()
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004A14 File Offset: 0x00002C14
		public Tag[] ToArray()
		{
			List<Tag> arr = new List<Tag>();
			while (this.MoveNext())
			{
				Tag item = this.Current;
				arr.Add(item);
			}
			return arr.ToArray();
		}

		// Token: 0x04000063 RID: 99
		private Tag _tag;

		// Token: 0x04000064 RID: 100
		private Token[] _tokens;

		// Token: 0x04000065 RID: 101
		private int _index;
	}
}
