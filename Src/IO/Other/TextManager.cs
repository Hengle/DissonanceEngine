using System.IO;
using Dissonance.Engine.IO;

namespace Dissonance.Engine.IO.Other
{
	public class TextManager : AssetManager<string>
	{
		public override string[] Extensions => new[] { ".txt" };

		public override string Import(Stream stream,string fileName)
		{
			using var reader = new StreamReader(stream);

			return reader.ReadToEnd();
		}
		public override void Export(string text,Stream stream)
		{
			using var writer = new StreamWriter(stream);

			writer.Write(text);
		}
	}
}