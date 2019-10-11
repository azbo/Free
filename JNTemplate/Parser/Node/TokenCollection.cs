using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace JinianNet.JNTemplate.Parser.Node
{
	// Token: 0x0200003A RID: 58
	public class TokenCollection : IList<Token>, ICollection<Token>, IEnumerable<Token>, IEnumerable, IEquatable<TokenCollection>
	{
		// Token: 0x06000123 RID: 291 RVA: 0x0000569A File Offset: 0x0000389A
		public TokenCollection()
		{
			this._list = new List<Token>();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000056AD File Offset: 0x000038AD
		public TokenCollection(int capacity)
		{
			this._list = new List<Token>(capacity);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000056C1 File Offset: 0x000038C1
		public TokenCollection(IEnumerable<Token> collection)
		{
			this._list = new List<Token>(collection);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000056D8 File Offset: 0x000038D8
		public TokenCollection(IList<Token> collection, int start, int end)
		{
			this._list = new List<Token>(end + 1 - start);
			int i = start;
			while (i <= end && i < collection.Count)
			{
				this.Add(collection[i]);
				i++;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000571D File Offset: 0x0000391D
		public Token First
		{
			get
			{
				if (this.Count == 0)
				{
					return null;
				}
				return this[0];
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00005730 File Offset: 0x00003930
		public Token Last
		{
			get
			{
				if (this.Count == 0)
				{
					return null;
				}
				return this[this.Count - 1];
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000574C File Offset: 0x0000394C
		public void Add(IList<Token> list, int start, int end)
		{
			int i = start;
			while (i <= end && i < list.Count)
			{
				this.Add(list[i]);
				i++;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000577C File Offset: 0x0000397C
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < this.Count; i++)
			{
				sb.Append(this[i].ToString());
			}
			return sb.ToString();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000057B9 File Offset: 0x000039B9
		public int IndexOf(Token item)
		{
			return this._list.IndexOf(item);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000057C7 File Offset: 0x000039C7
		public void Insert(int index, Token item)
		{
			if (item.TokenKind != TokenKind.Space)
			{
				this._list.Insert(index, item);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000057E0 File Offset: 0x000039E0
		public void RemoveAt(int index)
		{
			this._list.RemoveAt(index);
		}

		// Token: 0x17000034 RID: 52
		public Token this[int index]
		{
			get
			{
				return this._list[index];
			}
			set
			{
				if (value.TokenKind != TokenKind.Space)
				{
					this._list[index] = value;
				}
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005815 File Offset: 0x00003A15
		public void Add(Token item)
		{
			if (item.TokenKind != TokenKind.Space)
			{
				this._list.Add(item);
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000582D File Offset: 0x00003A2D
		public void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000583A File Offset: 0x00003A3A
		public bool Contains(Token item)
		{
			return this._list.Contains(item);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005848 File Offset: 0x00003A48
		public void CopyTo(Token[] array, int arrayIndex)
		{
			this._list.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005857 File Offset: 0x00003A57
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00005864 File Offset: 0x00003A64
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005867 File Offset: 0x00003A67
		public bool Remove(Token item)
		{
			return this._list.Remove(item);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005875 File Offset: 0x00003A75
		public IEnumerator<Token> GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005887 File Offset: 0x00003A87
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005890 File Offset: 0x00003A90
		public bool Equals(TokenCollection other)
		{
			if (other == null)
			{
				return false;
			}
			if (this.Count != other.Count)
			{
				return false;
			}
			for (int i = 0; i < other.Count; i++)
			{
				if (this[i] != other[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000058D6 File Offset: 0x00003AD6
		public override bool Equals(object obj)
		{
			return obj != null && obj is TokenCollection && this.Equals((TokenCollection)obj);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000058F3 File Offset: 0x00003AF3
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400007E RID: 126
		private List<Token> _list;
	}
}
