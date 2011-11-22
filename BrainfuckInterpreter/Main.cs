using System;
using System.Collections.Generic;

namespace BrainfuckInterpreter
{
	class MainClass
	{
		private List<IParser> parsers = new List<IParser>();
		private string program;
		
		public MainClass( string program )
		{
			parsers.Add(new BracketMatcher());
			parsers.Add(new Interpreter());
			this.program = program;
		}
		
		public void Execute()
		{
			foreach( IParser parser in parsers )
			{
				var success = parser.Parse( program );
				if( !success ) {
					Console.WriteLine(parser.Error());
					break;
				}
			}
		}
		
		public static void Main (string[] args)
		{
			var filename = args[0];
			var myFile = new System.IO.StreamReader(filename);
			var contents = myFile.ReadToEnd();
			myFile.Close();
			
			var bf = new MainClass(contents);
			bf.Execute();			
			Console.WriteLine("\nExecution finished");
		}
	}
}
