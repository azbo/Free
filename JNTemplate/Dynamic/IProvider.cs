using System;

namespace JinianNet.JNTemplate.Dynamic
{
	// Token: 0x02000044 RID: 68
	public interface IProvider
	{
		// Token: 0x0600016C RID: 364
		object ExcuteMethod(object container, string methodName, object[] args);

		// Token: 0x0600016D RID: 365
		object GetPropertyOrField(object value, string propertyName);
	}
}
