using System;

namespace JinianNet.JNTemplate.Configuration
{
	// Token: 0x02000047 RID: 71
	public class EngineConfig : ConfigBase
	{
		// Token: 0x0600018D RID: 397 RVA: 0x000060A8 File Offset: 0x000042A8
		public static EngineConfig CreateDefault()
		{
			return new EngineConfig
			{
				ResourceDirectories = new string[0],
				StripWhiteSpace = true,
				TagFlag = '$',
				TagPrefix = "${",
				TagSuffix = "}",
				ThrowExceptions = true,
				IgnoreCase = true,
				TagParsers = Field.RSEOLVER_TYPES,
				Charset = "utf-8"
			};
		}
	}
}
