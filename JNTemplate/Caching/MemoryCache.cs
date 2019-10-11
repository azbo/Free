using System;
using System.Collections;
using System.Collections.Generic;

namespace JinianNet.JNTemplate.Caching
{
	// Token: 0x0200004D RID: 77
	public class MemoryCache : ICache, IEnumerable, IDisposable
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00008C67 File Offset: 0x00006E67
		public MemoryCache()
		{
			this.dictionary = new Dictionary<string, object>();
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00008C7A File Offset: 0x00006E7A
		public int Count
		{
			get
			{
				return this.dictionary.Count;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008C87 File Offset: 0x00006E87
		public void Set(string key, object value)
		{
			this.dictionary[key] = value;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008C98 File Offset: 0x00006E98
		public object Get(string key)
		{
			object value;
			if (this.dictionary.TryGetValue(key, out value))
			{
				return value;
			}
			return null;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008CB8 File Offset: 0x00006EB8
		public object Remove(string key)
		{
			object result = this.Get(key);
			this.dictionary.Remove(key);
			return result;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008CCE File Offset: 0x00006ECE
		public IEnumerator GetEnumerator()
		{
			return this.dictionary.GetEnumerator();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008CE0 File Offset: 0x00006EE0
		public void Dispose()
		{
			this.dictionary.Clear();
		}

		// Token: 0x04000096 RID: 150
		private Dictionary<string, object> dictionary;
	}
}
