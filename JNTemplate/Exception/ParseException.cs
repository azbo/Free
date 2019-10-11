using System;

namespace JinianNet.JNTemplate.Exception
{
	// Token: 0x0200003E RID: 62
	public class ParseException : TemplateException
	{
		// Token: 0x06000147 RID: 327 RVA: 0x0000598E File Offset: 0x00003B8E
		public ParseException()
		{
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005996 File Offset: 0x00003B96
		public ParseException(string message, int line, int column) : base(message, line, column)
		{
			base.Column = column;
			base.Line = line;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000059AF File Offset: 0x00003BAF
		public ParseException(string message) : base(message)
		{
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000059B8 File Offset: 0x00003BB8
		public ParseException(string message, System.Exception innerException) : base(message, innerException)
		{
		}
	}
}
