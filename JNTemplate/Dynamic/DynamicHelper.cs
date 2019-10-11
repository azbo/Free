using System;
using System.Collections;

namespace JinianNet.JNTemplate.Dynamic
{
	// Token: 0x02000040 RID: 64
	public class DynamicHelper
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00005A6F File Offset: 0x00003C6F
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00005A87 File Offset: 0x00003C87
		internal static IProvider Instance
		{
			get
			{
				if (DynamicHelper.provider == null)
				{
					DynamicHelper.provider = new ReflectionProvider();
				}
				return DynamicHelper.provider;
			}
			set
			{
				DynamicHelper.provider = value;
			}
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005A8F File Offset: 0x00003C8F
		public static object ExcuteMethod(object container, string methodName, object[] args)
		{
			return DynamicHelper.Instance.ExcuteMethod(container, methodName, args);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005A9E File Offset: 0x00003C9E
		public static object GetPropertyOrField(object value, string propertyName)
		{
			return DynamicHelper.Instance.GetPropertyOrField(value, propertyName);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005AAC File Offset: 0x00003CAC
		public static IEnumerable ToIEnumerable(object dataSource)
		{
			if (dataSource == null)
			{
				return null;
			}
			IEnumerable result;
			if ((result = (dataSource as IEnumerable)) != null)
			{
				return result;
			}
			return null;
		}

		// Token: 0x04000084 RID: 132
		private static IProvider provider;
	}
}
