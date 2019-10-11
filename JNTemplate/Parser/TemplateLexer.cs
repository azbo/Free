using System;
using System.Collections.Generic;
using JinianNet.JNTemplate.Common;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x02000020 RID: 32
	public class TemplateLexer
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00003EBC File Offset: 0x000020BC
		public TemplateLexer(string text)
		{
			this._document = text;
			this._prefix = Engine.GetEnvironmentVariable("TagPrefix");
			this._flag = Engine.GetEnvironmentVariable("TagFlag")[0];
			this._suffix = Engine.GetEnvironmentVariable("TagSuffix");
			this.Reset();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003F14 File Offset: 0x00002114
		public void Reset()
		{
			this._flagMode = FlagMode.None;
			this._line = 1;
			this._column = 1;
			this._kind = TokenKind.Text;
			this._startColumn = 1;
			this._startLine = 1;
			this._scanner = new CharScanner(this._document);
			this._collection = new List<Token>();
			this._pos = new Stack<string>();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003F74 File Offset: 0x00002174
		private Token GetToken(TokenKind tokenKind)
		{
			Token token;
			if (tokenKind == TokenKind.StringEnd)
			{
				token = new Token(this._kind, this._scanner.GetEscapeString());
			}
			else
			{
				token = new Token(this._kind, this._scanner.GetString());
			}
			token.BeginLine = this._startLine;
			token.BeginColumn = this._startColumn;
			token.EndColumn = this._column;
			token.BeginLine = this._line;
			this._kind = tokenKind;
			this._startColumn = this._column;
			this._startLine = this._line;
			return token;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004006 File Offset: 0x00002206
		private bool Next()
		{
			return this.Next(1);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004010 File Offset: 0x00002210
		private bool Next(int i)
		{
			if (this._scanner.Next(i))
			{
				if (this._scanner.Read() == '\n')
				{
					this._line++;
					this._column = 1;
				}
				else
				{
					this._column++;
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004064 File Offset: 0x00002264
		private bool IsTagStart()
		{
			if (this._scanner.IsEnd() || this._flagMode != FlagMode.None)
			{
				return false;
			}
			bool find = true;
			for (int i = 0; i < this._prefix.Length; i++)
			{
				if (this._prefix[i] != this._scanner.Read(i))
				{
					find = false;
					break;
				}
			}
			if (find)
			{
				this._flagMode = FlagMode.Full;
				return true;
			}
			if (this._scanner.Read() == this._flag)
			{
				if (this._scanner.Read(1) == '*')
				{
					this._flagMode = FlagMode.Comment;
					return true;
				}
				if (char.IsLetter(this._scanner.Read(1)))
				{
					this._flagMode = FlagMode.Logogram;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004114 File Offset: 0x00002314
		private bool IsTagEnd()
		{
			if (this._flagMode != FlagMode.None && this._pos.Count == 0)
			{
				if (this._scanner.IsEnd())
				{
					return true;
				}
				if (this._scanner.Read() != '.')
				{
					if (this._flagMode == FlagMode.Full)
					{
						for (int i = 0; i < this._suffix.Length; i++)
						{
							if (this._suffix[i] != this._scanner.Read(i))
							{
								return false;
							}
						}
						return true;
					}
					if (this._flagMode == FlagMode.Comment)
					{
						return this._scanner.Read() == '*' && this._scanner.Read(1) == this._flag;
					}
					char value = this._scanner.Read();
					return ((value != '(' && !Utility.IsWord(value)) || !Utility.IsWord(this._scanner.Read(-1))) && (!Utility.IsWord(value) || this._scanner.Read(-1) != '.');
				}
			}
			return false;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004214 File Offset: 0x00002414
		public Token[] Parse()
		{
			if (this._kind != TokenKind.EOF)
			{
				do
				{
					if (this._flagMode != FlagMode.None)
					{
						if (this._flagMode == FlagMode.Comment)
						{
							this.Next(1);
							this.GetToken(TokenKind.TextData);
							this.ReadCommentToken();
						}
						else
						{
							if (this._flagMode == FlagMode.Full)
							{
								this.Next(this._prefix.Length - 1);
							}
							this.AddToken(this.GetTokenKind(this._scanner.Read()));
							TokenKind kind = this._kind;
							if (kind != TokenKind.LeftParentheses)
							{
								if (kind == TokenKind.StringStart)
								{
									this._pos.Push("\"");
								}
							}
							else
							{
								this._pos.Push("(");
							}
							this.ReadToken();
						}
					}
					else if (this.IsTagStart())
					{
						this.AddToken(TokenKind.TagStart);
					}
				}
				while (this.Next());
				this.AddToken(TokenKind.EOF);
				if (this._flagMode != FlagMode.None)
				{
					this._flagMode = FlagMode.None;
					this.AddToken(new Token(TokenKind.TagEnd, string.Empty));
				}
			}
			return this._collection.ToArray();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004318 File Offset: 0x00002518
		private void AddToken(TokenKind kind)
		{
			Token token = this.GetToken(kind);
			this.AddToken(token);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004334 File Offset: 0x00002534
		private void AddToken(Token token)
		{
			if (this._collection.Count > 0 && this._collection[this._collection.Count - 1].Next == null)
			{
				this._collection[this._collection.Count - 1].Next = token;
			}
			this._collection.Add(token);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004398 File Offset: 0x00002598
		private bool ReadEndToken()
		{
			if (this.IsTagEnd())
			{
				bool add = true;
				if (this._flagMode == FlagMode.Full)
				{
					this.AddToken(TokenKind.TagEnd);
					this.Next(this._suffix.Length);
				}
				else if (this._flagMode == FlagMode.Comment)
				{
					this.GetToken(TokenKind.TagEnd);
					this.Next(2);
					add = false;
				}
				else
				{
					this.AddToken(TokenKind.TagEnd);
				}
				this._flagMode = FlagMode.None;
				Token token;
				if (this.IsTagStart())
				{
					token = this.GetToken(TokenKind.TagStart);
				}
				else
				{
					token = this.GetToken(TokenKind.Text);
				}
				if (add)
				{
					this.AddToken(token);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004425 File Offset: 0x00002625
		private void ReadCommentToken()
		{
			while (this.Next() && !this.ReadEndToken())
			{
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000443C File Offset: 0x0000263C
		private int GetPrevCharCount(char c)
		{
			int i = 1;
			while (this._scanner.Read(-i) == c)
			{
				i++;
			}
			return i - 1;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004468 File Offset: 0x00002668
		private void ReadToken()
		{
			while (this.Next())
			{
				if (this._scanner.Read() == '"')
				{
					if (this._pos.Count > 0 && this._pos.Peek() == "\"")
					{
						if (this._scanner.Read(-1) != '\\' || this.GetPrevCharCount('\\') % 2 == 0)
						{
							if (this._kind == TokenKind.StringStart)
							{
								this.AddToken(TokenKind.String);
							}
							this.AddToken(TokenKind.StringEnd);
							this._pos.Pop();
							continue;
						}
						continue;
					}
					else if (this._kind == TokenKind.TagStart || this._kind == TokenKind.LeftBracket || this._kind == TokenKind.LeftParentheses || this._kind == TokenKind.Operator || this._kind == TokenKind.Punctuation || this._kind == TokenKind.Comma || this._kind == TokenKind.Space)
					{
						this.AddToken(TokenKind.StringStart);
						this._pos.Push("\"");
						continue;
					}
				}
				if (this._kind == TokenKind.StringStart)
				{
					this.AddToken(TokenKind.String);
				}
				else if (this._kind != TokenKind.String)
				{
					if (this._scanner.Read() == '(')
					{
						this._pos.Push("(");
					}
					else if (this._scanner.Read() == ')' && this._pos.Count > 0 && this._pos.Peek() == "(")
					{
						this._pos.Pop();
						if (this._pos.Count == 1)
						{
						}
					}
					else if (this.ReadEndToken())
					{
						break;
					}
					TokenKind tk;
					if (this._scanner.Read() == '+' || this._scanner.Read() == '-')
					{
						if (char.IsNumber(this._scanner.Read(1)) && this._kind != TokenKind.Number && this._kind != TokenKind.RightBracket && this._kind != TokenKind.RightParentheses && this._kind != TokenKind.String && this._kind != TokenKind.Tag && this._kind != TokenKind.TextData)
						{
							tk = TokenKind.Number;
						}
						else
						{
							tk = TokenKind.Operator;
						}
					}
					else
					{
						tk = this.GetTokenKind(this._scanner.Read());
					}
					if ((this._kind != tk || this._kind == TokenKind.LeftParentheses || this._kind == TokenKind.RightParentheses) && (tk != TokenKind.Number || this._kind != TokenKind.TextData) && (tk != TokenKind.Dot || this._kind != TokenKind.Number))
					{
						this.AddToken(tk);
					}
				}
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000046C8 File Offset: 0x000028C8
		private TokenKind GetTokenKind(char c)
		{
			if (this._flagMode == FlagMode.None)
			{
				return TokenKind.Text;
			}
			if (c <= '^')
			{
				switch (c)
				{
				case ' ':
					return TokenKind.Space;
				case '!':
				case '%':
				case '&':
				case '*':
				case '+':
				case '-':
				case '/':
				case '<':
				case '=':
				case '>':
				case '?':
					break;
				case '"':
					return TokenKind.StringStart;
				case '#':
				case '$':
				case '\'':
				case ':':
					return TokenKind.TextData;
				case '(':
					return TokenKind.LeftParentheses;
				case ')':
					return TokenKind.RightParentheses;
				case ',':
					return TokenKind.Comma;
				case '.':
					return TokenKind.Dot;
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					return TokenKind.Number;
				case ';':
					return TokenKind.Punctuation;
				default:
					switch (c)
					{
					case '[':
						return TokenKind.LeftBracket;
					case '\\':
						return TokenKind.TextData;
					case ']':
						return TokenKind.RightBracket;
					case '^':
						break;
					default:
						return TokenKind.TextData;
					}
					break;
				}
			}
			else if (c != '|' && c != '~')
			{
				return TokenKind.TextData;
			}
			return TokenKind.Operator;
		}

		// Token: 0x04000056 RID: 86
		private FlagMode _flagMode;

		// Token: 0x04000057 RID: 87
		private string _document;

		// Token: 0x04000058 RID: 88
		private int _column;

		// Token: 0x04000059 RID: 89
		private int _line;

		// Token: 0x0400005A RID: 90
		private TokenKind _kind;

		// Token: 0x0400005B RID: 91
		private int _startColumn;

		// Token: 0x0400005C RID: 92
		private int _startLine;

		// Token: 0x0400005D RID: 93
		private CharScanner _scanner;

		// Token: 0x0400005E RID: 94
		private List<Token> _collection;

		// Token: 0x0400005F RID: 95
		private Stack<string> _pos;

		// Token: 0x04000060 RID: 96
		private string _prefix;

		// Token: 0x04000061 RID: 97
		private char _flag;

		// Token: 0x04000062 RID: 98
		private string _suffix;
	}
}
