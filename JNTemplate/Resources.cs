using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JinianNet.JNTemplate
{
	// Token: 0x02000009 RID: 9
	public class Resources
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000023F8 File Offset: 0x000005F8
		public static IEnumerable<string> MergerPaths(IEnumerable<string> oldPaths, params string[] newPaths)
		{
			List<string> list = new List<string>();
			if (newPaths != null)
			{
				list.AddRange(newPaths);
			}
			if (oldPaths != null)
			{
				list.AddRange(oldPaths);
			}
			return list;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002420 File Offset: 0x00000620
		public static int FindPath(string filename, out string fullPath)
		{
			return Resources.FindPath(Engine.ResourceDirectories, filename, out fullPath);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002430 File Offset: 0x00000630
		public static int FindPath(IEnumerable<string> paths, string filename, out string fullPath)
		{
			fullPath = null;
			if (!string.IsNullOrEmpty(filename))
			{
				if ((filename = Resources.NormalizePath(filename)) == null)
				{
					return -1;
				}
				int i = 0;
				foreach (string checkUrl in paths)
				{
					if (checkUrl[checkUrl.Length - 1] != Path.DirectorySeparatorChar)
					{
						fullPath = checkUrl + filename;
					}
					else
					{
						fullPath = checkUrl.Remove(checkUrl.Length - 1, 1) + filename;
					}
					if (File.Exists(fullPath))
					{
						return i;
					}
					i++;
				}
				return -1;
			}
			return -1;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024D8 File Offset: 0x000006D8
		public static string Load(IEnumerable<string> paths, string filename, Encoding encoding)
		{
			if (paths == null && string.IsNullOrEmpty(filename))
			{
				return null;
			}
			if (encoding == null)
			{
				encoding = Encoding.UTF8;
			}
			string full;
			if (Resources.FindPath(paths, filename, out full) != -1)
			{
				return Resources.LoadResource(full, encoding);
			}
			return null;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002511 File Offset: 0x00000711
		public static string Load(string filename, Encoding encoding)
		{
			return Resources.Load(Engine.ResourceDirectories, filename, encoding);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000251F File Offset: 0x0000071F
		public static string LoadResource(string fullPath, Encoding encoding)
		{
			if (!File.Exists(fullPath))
			{
				return null;
			}
			if (encoding == null)
			{
				encoding = Encoding.UTF8;
			}
			return File.ReadAllText(fullPath, encoding);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000253C File Offset: 0x0000073C
		public static string Load(IEnumerable<string> paths, string filename, Encoding encoding, out string fullName)
		{
			if (Resources.IsLocalPath(filename))
			{
				fullName = filename;
			}
			else if (Resources.FindPath(paths, filename, out fullName) == -1)
			{
				return null;
			}
			return Resources.LoadResource(fullName, encoding);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002560 File Offset: 0x00000760
		public static bool IsLocalPath(string path)
		{
			return !string.IsNullOrEmpty(path) && (path.IndexOf(Path.VolumeSeparatorChar) != -1 || path[0] == '/');
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002588 File Offset: 0x00000788
		public static string NormalizePath(string filename)
		{
			if (string.IsNullOrEmpty(filename) || filename.IndexOfAny(Path.GetInvalidPathChars()) != -1)
			{
				return null;
			}
			List<string> values = new List<string>(filename.Split(new char[]
			{
				'/'
			}));
			for (int i = 0; i < values.Count; i++)
			{
				if (values[i] == "." || string.IsNullOrEmpty(values[i]))
				{
					values.RemoveAt(i);
					i--;
				}
				else if (values[i] == "..")
				{
					if (i == 0)
					{
						return null;
					}
					values.RemoveAt(i);
					i--;
					values.RemoveAt(i);
					i--;
				}
			}
			values.Insert(0, string.Empty);
			return string.Join(Path.DirectorySeparatorChar.ToString(), values.ToArray());
		}
	}
}
