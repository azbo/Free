using System;
using System.Collections.Generic;

namespace JinianNet.JNTemplate.Parser
{
	// Token: 0x02000023 RID: 35
	public class VariableScope
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00004A7F File Offset: 0x00002C7F
		public VariableScope() : this(null, null)
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004A89 File Offset: 0x00002C89
		public VariableScope(IDictionary<string, object> dictionary) : this(null, dictionary)
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004A93 File Offset: 0x00002C93
		public VariableScope(VariableScope parent, IDictionary<string, object> dictionary)
		{
			this._parent = parent;
			this._dic = (dictionary ?? new Dictionary<string, object>(Engine.ComparerIgnoreCase));
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004AB7 File Offset: 0x00002CB7
		public VariableScope(VariableScope parent) : this(parent, null)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004AC1 File Offset: 0x00002CC1
		public void Clear(bool all)
		{
			this._dic.Clear();
			if (all && this._parent != null)
			{
				this._parent.Clear(all);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004AE5 File Offset: 0x00002CE5
		public void Clear()
		{
			this.Clear(false);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004AEE File Offset: 0x00002CEE
		public VariableScope Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x1700001A RID: 26
		public object this[string name]
		{
			get
			{
				object val;
				if (this._dic.TryGetValue(name, out val))
				{
					return val;
				}
				if (this._parent != null)
				{
					return this._parent[name];
				}
				return null;
			}
			set
			{
				this._dic[name] = value;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004B3C File Offset: 0x00002D3C
		internal bool SetValue(string key, object value)
		{
			if (this._dic.ContainsKey(key))
			{
				this[key] = value;
				return true;
			}
			return this._parent != null && this._parent.SetValue(key, value);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004B6D File Offset: 0x00002D6D
		public void Push(string key, object value)
		{
			this._dic.Add(key, value);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004B7C File Offset: 0x00002D7C
		public bool ContainsKey(string key)
		{
			return this._dic.ContainsKey(key) || (this._parent != null && this._parent.ContainsKey(key));
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004BA4 File Offset: 0x00002DA4
		public bool Remove(string key)
		{
			return this._dic.Remove(key);
		}

		// Token: 0x04000066 RID: 102
		private VariableScope _parent;

		// Token: 0x04000067 RID: 103
		private IDictionary<string, object> _dic;
	}
}
