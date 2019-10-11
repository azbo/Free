using System;
using System.Reflection;

namespace JinianNet.JNTemplate.Dynamic
{
	// Token: 0x02000041 RID: 65
	public class DynamicMethodInfo
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005AD3 File Offset: 0x00003CD3
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00005ADB File Offset: 0x00003CDB
		public ExcuteMethodDelegate Delegate
		{
			get
			{
				return this._delegate;
			}
			set
			{
				this._delegate = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00005AE4 File Offset: 0x00003CE4
		// (set) Token: 0x0600015E RID: 350 RVA: 0x00005AEC File Offset: 0x00003CEC
		public ParameterInfo[] Parameters
		{
			get
			{
				return this._parameters;
			}
			set
			{
				this._parameters = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00005AF5 File Offset: 0x00003CF5
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00005AFD File Offset: 0x00003CFD
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005B06 File Offset: 0x00003D06
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00005B0E File Offset: 0x00003D0E
		public string FullName
		{
			get
			{
				return this._fullName;
			}
			set
			{
				this._fullName = value;
			}
		}

		// Token: 0x04000085 RID: 133
		private ExcuteMethodDelegate _delegate;

		// Token: 0x04000086 RID: 134
		private ParameterInfo[] _parameters;

		// Token: 0x04000087 RID: 135
		private string _name;

		// Token: 0x04000088 RID: 136
		private string _fullName;
	}
}
