using System;

namespace JinianNet.JNTemplate
{
	// Token: 0x0200000D RID: 13
	public enum TokenKind
	{
		// Token: 0x0400003C RID: 60
		None,
		// Token: 0x0400003D RID: 61
		Text,
		// Token: 0x0400003E RID: 62
		TextData,
		// Token: 0x0400003F RID: 63
		Tag,
		// Token: 0x04000040 RID: 64
		TagStart,
		// Token: 0x04000041 RID: 65
		TagEnd,
		// Token: 0x04000042 RID: 66
		String,
		// Token: 0x04000043 RID: 67
		Number,
		// Token: 0x04000044 RID: 68
		LeftBracket,
		// Token: 0x04000045 RID: 69
		RightBracket,
		// Token: 0x04000046 RID: 70
		LeftParentheses,
		// Token: 0x04000047 RID: 71
		RightParentheses,
		// Token: 0x04000048 RID: 72
		NewLine,
		// Token: 0x04000049 RID: 73
		Dot,
		// Token: 0x0400004A RID: 74
		StringStart,
		// Token: 0x0400004B RID: 75
		StringEnd,
		// Token: 0x0400004C RID: 76
		Space,
		// Token: 0x0400004D RID: 77
		Punctuation,
		// Token: 0x0400004E RID: 78
		Operator,
		// Token: 0x0400004F RID: 79
		Comma,
		// Token: 0x04000050 RID: 80
		EOF
	}
}
