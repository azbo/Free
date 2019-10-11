using System;
using System.Collections;

namespace JinianNet.JNTemplate.Caching
{
	// Token: 0x0200004C RID: 76
	public interface ICache : IEnumerable, IDisposable
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001B1 RID: 433
		int Count { get; }

		// Token: 0x060001B2 RID: 434
		void Set(string key, object value);

		// Token: 0x060001B3 RID: 435
		object Get(string key);

		// Token: 0x060001B4 RID: 436
		object Remove(string key);
	}
}
