using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace JinianNet.JNTemplate.Dynamic
{
	// Token: 0x02000045 RID: 69
	public class ReflectionProvider : IProvider
	{
		// Token: 0x0600016E RID: 366 RVA: 0x00005B1F File Offset: 0x00003D1F
		public ReflectionProvider()
		{
			this.expressionPartSeparator = new char[]
			{
				'.'
			};
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005B38 File Offset: 0x00003D38
		private object GetIndexedProperty(object container, bool isNumber, object propIndex)
		{
			IList list;
			if (isNumber && (list = (container as IList)) != null)
			{
				return list[(int)propIndex];
			}
			IDictionary dic;
			if ((dic = (container as IDictionary)) != null)
			{
				return dic[propIndex];
			}
			MethodInfo info = TypeExtensions.GetMethod(container.GetType(), "get_Item", new Type[]
			{
				propIndex.GetType()
			});
			if (info != null)
			{
				return info.Invoke(container, new object[]
				{
					propIndex
				});
			}
			return null;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005BA8 File Offset: 0x00003DA8
		public object GetPropertyOrField(object container, string propName)
		{
			Type t = container.GetType();
			if (!char.IsDigit(propName[0]))
			{
				PropertyInfo p = TypeExtensions.GetProperty(t, propName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | Engine.BindIgnoreCase);
				if (p != null)
				{
					return p.GetValue(container, null);
				}
			}
			int index;
			if (int.TryParse(propName, out index))
			{
				return this.GetIndexedProperty(container, true, index);
			}
			return this.GetIndexedProperty(container, false, propName);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005C08 File Offset: 0x00003E08
		public string Eval(object container, string expression, string format)
		{
			object obj = this.Eval(container, expression);
			if (obj == null)
			{
				return string.Empty;
			}
			if (string.IsNullOrEmpty(format))
			{
				return obj.ToString();
			}
			return string.Format(format, obj);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005C40 File Offset: 0x00003E40
		public object Eval(object container, string expression)
		{
			if (expression == null)
			{
				return null;
			}
			expression = expression.Trim();
			if (expression.Length == 0)
			{
				return null;
			}
			if (container == null)
			{
				return null;
			}
			string[] expressionParts = expression.Split(this.expressionPartSeparator);
			return this.Eval(container, expressionParts);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005C7E File Offset: 0x00003E7E
		public object Eval(object container, string[] expressionParts)
		{
			return this.Eval(container, expressionParts, 0, expressionParts.Length);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005C8C File Offset: 0x00003E8C
		private object Eval(object container, string[] expressionParts, int start, int end)
		{
			object property = container;
			int i = start;
			while (i < end && property != null)
			{
				if (property == null)
				{
					return null;
				}
				property = this.GetPropertyOrField(property, expressionParts[i]);
				i++;
			}
			return property;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005CBC File Offset: 0x00003EBC
		public MethodInfo GetMethod(Type type, string methodName, ref Type[] args, out bool hasParam)
		{
			hasParam = false;
			if (args == null || Array.LastIndexOf<Type>(args, null) == -1)
			{
				MethodInfo method = TypeExtensions.GetMethod(type, methodName, args);
				if (method != null)
				{
					return method;
				}
			}
			foreach (MethodInfo i in ((IEnumerable<MethodInfo>)TypeExtensions.GetMethods(type, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | Engine.BindIgnoreCase)))
			{
				if (i.Name.Equals(methodName, Engine.ComparisonIgnoreCase))
				{
					ParameterInfo[] pi = i.GetParameters();
					if (pi.Length >= pi.Length - 1 && (pi.Length == args.Length | hasParam))
					{
						bool accord = true;
						for (int j = 0; j < pi.Length - 1; j++)
						{
							if (args[j] != null && args[j] != pi[j].ParameterType && !args[j].GetTypeInfo().IsSubclassOf(pi[j].ParameterType))
							{
								accord = false;
								break;
							}
						}
						if (accord)
						{
							if (hasParam)
							{
								if (args.Length != pi.Length - 1)
								{
									Type arrType = pi[pi.Length - 1].ParameterType.GetElementType();
									for (int k = pi.Length - 1; k < args.Length; k++)
									{
										if (args[k] != null && args[k] != arrType && !args[k].GetTypeInfo().IsSubclassOf(arrType))
										{
											accord = false;
											break;
										}
									}
								}
								if (accord)
								{
									args = new Type[pi.Length];
									for (int l = 0; l < pi.Length; l++)
									{
										args[l] = pi[l].ParameterType;
									}
									return i;
								}
							}
							else if (args[args.Length - 1] == pi[pi.Length - 1].ParameterType)
							{
								return i;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005E88 File Offset: 0x00004088
		public object ExcuteMethod(object container, string methodName, object[] args)
		{
			Type[] types = new Type[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] != null)
				{
					types[i] = args[i].GetType();
				}
			}
			Type t = container.GetType();
			bool hasParam;
			MethodInfo method = this.GetMethod(t, methodName, ref types, out hasParam);
			if (method != null)
			{
				if (!hasParam)
				{
					return method.Invoke(container, args);
				}
				if (types.Length - 1 != args.Length)
				{
					Array arr = Array.CreateInstance(types[types.Length - 1].GetElementType(), args.Length - types.Length + 1);
					for (int j = types.Length - 1; j < args.Length; j++)
					{
						arr.SetValue(args[j], j - (types.Length - 1));
					}
					object[] newArgs = new object[types.Length];
					for (int k = 0; k < newArgs.Length - 1; k++)
					{
						newArgs[k] = args[k];
					}
					newArgs[newArgs.Length - 1] = arr;
					return method.Invoke(container, newArgs);
				}
			}
			return null;
		}

		// Token: 0x04000089 RID: 137
		private readonly char[] expressionPartSeparator;
	}
}
