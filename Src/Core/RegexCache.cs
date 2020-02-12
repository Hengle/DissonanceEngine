﻿using System.Text.RegularExpressions;

namespace Dissonance.Engine
{
	internal static class RegexCache
	{
		public static Regex shaderFSuffixA = new Regex(@"([^.]|^)([\d]+)f(?=[^\w])",RegexOptions.Compiled);
		public static Regex shaderFSuffixB = new Regex(@"(\.)([\d]+)f(?=[^\w])",RegexOptions.Compiled);

		public static Regex commandArguments = new Regex(@"-(\w+)(?:[\s]+|\b)(\"".+\""|[^-\s][^\s]*)?",RegexOptions.Compiled);
	}
}
