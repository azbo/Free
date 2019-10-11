using System;

namespace JinianNet.JNTemplate.Common
{
	// Token: 0x0200004B RID: 75
	public class Utility
	{
		// Token: 0x060001AC RID: 428 RVA: 0x00008BFD File Offset: 0x00006DFD
		public static bool ToBoolean(string input)
		{
			return "true".Equals(input, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008C10 File Offset: 0x00006E10
		public static bool IsLetter(char value)
		{
			return char.IsLower(value) || char.IsUpper(value);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00008C22 File Offset: 0x00006E22
		public static bool IsWord(char value)
		{
			return char.IsLower(value) || char.IsUpper(value) || char.IsNumber(value) || value == '_';
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00008C43 File Offset: 0x00006E43
		public static bool IsEqual(string x, string y)
		{
			if (x == null || y == null)
			{
				return x == y;
			}
			return string.Equals(x, y, Engine.ComparisonIgnoreCase);
		}
	}
}
