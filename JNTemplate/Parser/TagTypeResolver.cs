using System;
using System.Collections;
using System.Collections.Generic;
using JinianNet.JNTemplate.Parser.Node;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x0200001F RID: 31
	public class TagTypeResolver : ITagTypeResolver, ICollection<ITagParser>, IEnumerable<ITagParser>, IEnumerable
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003D13 File Offset: 0x00001F13
		public TagTypeResolver() : this(null)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003D1C File Offset: 0x00001F1C
		public TagTypeResolver(IEnumerable<ITagParser> parsers)
		{
			if (parsers != null)
			{
				this._collection = new List<ITagParser>(parsers);
				return;
			}
			this._collection = new List<ITagParser>();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003D40 File Offset: 0x00001F40
		public Tag Resolver(TemplateParser parser, TokenCollection tc)
		{
			for (int i = 0; i < this._collection.Count; i++)
			{
				if (this._collection[i] != null)
				{
					Tag t = this._collection[i].Parse(parser, tc);
					if (t != null)
					{
						t.FirstToken = tc.First;
						if (t.Children.Count == 0 || (t.LastToken = (t.Children[t.Children.Count - 1].LastToken ?? t.Children[t.Children.Count - 1].FirstToken)) == null || tc.Last.CompareTo(t.LastToken) > 0)
						{
							t.LastToken = tc.Last;
						}
						return t;
					}
				}
			}
			return null;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003E19 File Offset: 0x00002019
		public void Add(ITagParser item)
		{
			this._collection.Add(item);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003E27 File Offset: 0x00002027
		public void Insert(int index, ITagParser item)
		{
			this._collection.Insert(index, item);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003E36 File Offset: 0x00002036
		public void Clear()
		{
			this._collection.Clear();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003E43 File Offset: 0x00002043
		public bool Contains(ITagParser item)
		{
			return this._collection.Contains(item);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003E51 File Offset: 0x00002051
		public void CopyTo(ITagParser[] array, int arrayIndex)
		{
			this._collection.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003E60 File Offset: 0x00002060
		public int Count
		{
			get
			{
				return this._collection.Count;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003E6D File Offset: 0x0000206D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000016 RID: 22
		public ITagParser this[int index]
		{
			get
			{
				return this._collection[index];
			}
			set
			{
				this._collection[index] = value;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003E8D File Offset: 0x0000208D
		public bool Remove(ITagParser item)
		{
			return this._collection.Remove(item);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003E9B File Offset: 0x0000209B
		public IEnumerator<ITagParser> GetEnumerator()
		{
			return this._collection.GetEnumerator();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003EAD File Offset: 0x000020AD
		IEnumerator IEnumerable.GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Count; i = num + 1)
			{
				yield return this[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x04000055 RID: 85
		private readonly List<ITagParser> _collection;
	}
}
