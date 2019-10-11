using System;

namespace JinianNet.JNTemplate
{
	// Token: 0x02000003 RID: 3
	public class Field
	{
		// Token: 0x0400000A RID: 10
		public const string Version = "1.3";

		// Token: 0x0400000B RID: 11
		internal const string KEY_FOREACH = "foreach";

		// Token: 0x0400000C RID: 12
		internal const string KEY_IF = "if";

		// Token: 0x0400000D RID: 13
		internal const string KEY_ELSEIF = "elseif";

		// Token: 0x0400000E RID: 14
		internal const string KEY_ELSE = "else";

		// Token: 0x0400000F RID: 15
		internal const string KEY_SET = "set";

		// Token: 0x04000010 RID: 16
		internal const string KEY_LOAD = "load";

		// Token: 0x04000011 RID: 17
		internal const string KEY_INCLUDE = "include";

		// Token: 0x04000012 RID: 18
		internal const string KEY_END = "end";

		// Token: 0x04000013 RID: 19
		internal const string KEY_FOR = "for";

		// Token: 0x04000014 RID: 20
		internal const string KEY_IN = "in";

		// Token: 0x04000015 RID: 21
		internal static readonly string[] RSEOLVER_TYPES = new string[]
		{
			"JinianNet.JNTemplate.Parser.BooleanParser",
			"JinianNet.JNTemplate.Parser.NumberParser",
			"JinianNet.JNTemplate.Parser.EleseParser",
			"JinianNet.JNTemplate.Parser.EndParser",
			"JinianNet.JNTemplate.Parser.VariableParser",
			"JinianNet.JNTemplate.Parser.StringParser",
			"JinianNet.JNTemplate.Parser.ForeachParser",
			"JinianNet.JNTemplate.Parser.ForParser",
			"JinianNet.JNTemplate.Parser.SetParser",
			"JinianNet.JNTemplate.Parser.IfParser",
			"JinianNet.JNTemplate.Parser.ElseifParser",
			"JinianNet.JNTemplate.Parser.LoadParser",
			"JinianNet.JNTemplate.Parser.IncludeParser",
			"JinianNet.JNTemplate.Parser.FunctionParser",
			"JinianNet.JNTemplate.Parser.ComplexParser"
		};
	}
}
