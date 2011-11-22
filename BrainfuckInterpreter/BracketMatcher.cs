using System;

namespace BrainfuckInterpreter
{
	public class BracketMatcher : IParser
	{		
		private char[] codeArray;
		private string error = "Unmatched brackets in program.";
		
		public bool Parse( string code )
		{
			this.codeArray = code.ToCharArray();
			
			var count = 0;
			for( var i = 0; i < codeArray.Length; i++ )
			{
				if( codeArray[i] == '[' ) count++;
				else if( codeArray[i] == ']' ) count--;
				
			}
			
			return count == 0;
		}
		
		public string Error() { return this.error; }
	}
}

