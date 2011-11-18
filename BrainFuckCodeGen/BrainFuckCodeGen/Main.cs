using System;

namespace BrainFuckCodeGen
{
	class MainClass
	{
		private string upToA ="++++++++++++++++++++++++++++++++>";
		private string print = ".";
		private string clearOutAndCounter = "[<->-]";
		
		public string GenerateProgram(string toOutput)
		{
			var code = upToA;
			var chars = toOutput.ToCharArray();
			for( int i = 0; i < toOutput.Length; i++ ){
				var count = chars[i] - 32;
				
				var incrementCount = "";
				for( int j = 0; j < count; j++ ){ incrementCount += "+"; }
				code += incrementCount + "<" + incrementCount + print + ">" + clearOutAndCounter;
			}			
			
			return code;
		}
		
		public static void Main (string[] args)
		{
			var output = "Brainfuck is awesome. Which is why you should write some codegen.";
			var gen = new MainClass();
			var program = gen.GenerateProgram(output);
			
			Console.WriteLine (program);
		}
	}
}
