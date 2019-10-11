using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using JinianNet.JNTemplate.Caching;
using JinianNet.JNTemplate.Common;
using JinianNet.JNTemplate.Configuration;
using JinianNet.JNTemplate.Parser;

namespace JinianNet.JNTemplate
{
	// Token: 0x02000002 RID: 2
	public class Engine
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static void Configure(ConfigBase conf, VariableScope scope)
		{
			if (conf == null)
			{
				throw new ArgumentNullException("\"conf\" cannot be null.");
			}
			Engine.Configure(conf.ToDictionary(), conf.ResourceDirectories, conf.TagParsers, scope);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002078 File Offset: 0x00000278
		public static void Configure(IDictionary<string, string> conf, string[] directories, string[] parsers, VariableScope scope)
		{
			if (conf == null)
			{
				throw new ArgumentNullException("\"conf\" cannot be null.");
			}
			Engine._scope = scope;
			Engine.InitializationEnvironment(conf);
			if (Engine._cache != null)
			{
				Engine._cache.Dispose();
			}
			Engine._cache = null;
			if (!string.IsNullOrEmpty(Engine.GetEnvironmentVariable("CachingProvider")))
			{
				Engine._cache = (ICache)Activator.CreateInstance(Type.GetType(Engine.GetEnvironmentVariable("CachingProvider")));
			}
			if (Utility.ToBoolean(Engine.GetEnvironmentVariable("IgnoreCase")))
			{
				Engine._bindingFlags = BindingFlags.IgnoreCase;
				Engine._stringComparer = StringComparer.OrdinalIgnoreCase;
				Engine._stringComparison = StringComparison.OrdinalIgnoreCase;
			}
			else
			{
				Engine._stringComparison = StringComparison.Ordinal;
				Engine._bindingFlags = BindingFlags.DeclaredOnly;
				Engine._stringComparer = StringComparer.Ordinal;
			}
			if (directories == null)
			{
				Engine._resourceDirectories = new string[0];
			}
			else
			{
				Engine._resourceDirectories = directories;
			}
			Engine.InitializationParser(parsers);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002140 File Offset: 0x00000340
		private static void InitializationEnvironment(IDictionary<string, string> conf)
		{
			Engine._variable = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			foreach (KeyValuePair<string, string> node in conf)
			{
				Engine.SetEnvironmentVariable(node.Key, node.Value);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021A4 File Offset: 0x000003A4
		private static void InitializationParser(string[] parsers)
		{
			if (parsers == null)
			{
				parsers = Field.RSEOLVER_TYPES;
			}
			ITagParser[] tps = new ITagParser[parsers.Length];
			for (int i = 0; i < tps.Length; i++)
			{
				tps[i] = (ITagParser)Activator.CreateInstance(Type.GetType(parsers[i]));
			}
			Engine._tagResolver = new TagTypeResolver(tps);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021F2 File Offset: 0x000003F2
		public static void Configure(ConfigBase conf)
		{
			Engine.Configure(conf, null);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021FB File Offset: 0x000003FB
		public static TemplateContext CreateContext()
		{
			if (Engine._scope == null)
			{
				return new TemplateContext();
			}
			return new TemplateContext(Engine._scope);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002214 File Offset: 0x00000414
		public static ITemplate CreateTemplate(string text)
		{
			return new Template(Engine.CreateContext(), text);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002221 File Offset: 0x00000421
		public static ITemplate LoadTemplate(string path)
		{
			return Engine.LoadTemplate(path, Engine.CreateContext());
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002230 File Offset: 0x00000430
		public static ITemplate LoadTemplate(string path, TemplateContext ctx)
		{
			string fullPath;
			string text = Resources.Load(Engine.ResourceDirectories, path, ctx.Charset, out fullPath);
			Template template = new Template(ctx, text);
			if (fullPath != null)
			{
				template.TemplateKey = fullPath;
				ctx.CurrentPath = Path.GetDirectoryName(fullPath);
			}
			return template;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002270 File Offset: 0x00000470
		public static ICache Cache
		{
			get
			{
				return Engine._cache;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002277 File Offset: 0x00000477
		public static ITagTypeResolver TagResolver
		{
			get
			{
				return Engine._tagResolver;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000227E File Offset: 0x0000047E
		internal static StringComparison ComparisonIgnoreCase
		{
			get
			{
				return Engine._stringComparison;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002285 File Offset: 0x00000485
		internal static BindingFlags BindIgnoreCase
		{
			get
			{
				return Engine._bindingFlags;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000228C File Offset: 0x0000048C
		internal static StringComparer ComparerIgnoreCase
		{
			get
			{
				return Engine._stringComparer;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002293 File Offset: 0x00000493
		public static string[] ResourceDirectories
		{
			get
			{
				return Engine._resourceDirectories;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000229C File Offset: 0x0000049C
		public static string GetEnvironmentVariable(string variable)
		{
			string value;
			if (((Dictionary<string, string>)Engine.GetEnvironmentVariables()).TryGetValue(variable, out value))
			{
				return value;
			}
			return null;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022C0 File Offset: 0x000004C0
		public static IDictionary GetEnvironmentVariables()
		{
			if (Engine._variable == null)
			{
				object lockObject = Engine._lockObject;
				lock (lockObject)
				{
					if (Engine._variable == null)
					{
						Engine.Configure(EngineConfig.CreateDefault());
					}
				}
			}
			return Engine._variable;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002318 File Offset: 0x00000518
		public static void SetEnvironmentVariable(string variable, string value)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("\"variable\" cannot be null.");
			}
			if (value == null)
			{
				Engine.GetEnvironmentVariables().Remove(variable);
				return;
			}
			Engine.GetEnvironmentVariables()[variable] = value;
		}

		// Token: 0x04000001 RID: 1
		private static Dictionary<string, string> _variable;

		// Token: 0x04000002 RID: 2
		private static string[] _resourceDirectories;

		// Token: 0x04000003 RID: 3
		private static object _lockObject = new object();

		// Token: 0x04000004 RID: 4
		private static VariableScope _scope;

		// Token: 0x04000005 RID: 5
		private static ITagTypeResolver _tagResolver;

		// Token: 0x04000006 RID: 6
		private static StringComparison _stringComparison;

		// Token: 0x04000007 RID: 7
		private static BindingFlags _bindingFlags;

		// Token: 0x04000008 RID: 8
		private static StringComparer _stringComparer;

		// Token: 0x04000009 RID: 9
		private static ICache _cache;
	}
}
