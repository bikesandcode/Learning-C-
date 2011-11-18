using System;

namespace BrainfuckInterpreter
{
	class MainClass
	{
		private int[] buffer = new int[30000];
		private int dataPointer = 0;
		private int codePointer = -1;
		private int nestedLoopDepth = 0;
		
		private string program;
		private char[] codeArray;
		
		public MainClass( string program )
		{
			this.program = program;
			this.codeArray = program.ToCharArray();
		}
		
		
		public void Execute()
		{
			try{
				while( codePointer < program.Length-1 )
				{
					codePointer++;
					char next = codeArray[codePointer];
					switch (next)
					{
						case '+': 
						{ 
							//Console.WriteLine("++");
							buffer[dataPointer]++; 
							break; 
						}
						case '-': 
						{ 
							//Console.WriteLine("--");
							buffer[dataPointer]--; 
							break;
						}
						case '.': 
						{ 
							//Console.WriteLine("Print ptr={0}", dataPointer);
							Console.Write((char)(buffer[dataPointer])); 
							break;
						}
						case ',': 
						{
							int read = Console.Read();
							buffer[dataPointer] = read;
							break;
						}
						case '[': 
						{ 
							//Console.WriteLine("{");
							LoopStart(); 
							break;
						}
						case ']': 
						{ 
							//Console.WriteLine("}");
							LoopEnd(); 
							break;
						}
						case '<': 
						{ 						
							//Console.WriteLine("ptr--");
							dataPointer--; 
							break;
						}
						case '>': 
						{
							//Console.WriteLine("ptr++");
							dataPointer++; 
							break;
						}
						default: {break;} //Ignore all else
					}				
				}
			}
			catch(System.IndexOutOfRangeException e)
			{
				Console.WriteLine("Array index out of range hit. Dump: ");
				Console.WriteLine("DataPtr: {0}", dataPointer);
				Console.WriteLine("CodePtr: {0}", codePointer);
			}
		}
		
		/**
		 * Called when a [ character is hit, loop start.
		 * If the data pointer is currently -> 0, jump to
		 * just past the next ].
		 * Otherwise, truck forward.
		 **/
		private void LoopStart()
		{
			nestedLoopDepth++;
			if( buffer[dataPointer] == 0 ) ScanToMatchingClose(false);
		}
		
		/**
		 * Called when a ] character is hit. Scan back to the
		 * most recent [ character.
		 **/
		private void LoopEnd()
		{
			ScanToMatchingOpen(false);
			//And back so the next tick puts us on the loop start.
			codePointer--;
			nestedLoopDepth--;
		}
		
		/**
		 * Scan forward to the matching close loop character
		 **/
		private void ScanToMatchingClose(bool nested)
		{
			while( codeArray[codePointer] != ']' )
			{
				codePointer++;
				if( codeArray[codePointer] == '[' ) ScanToMatchingClose(true);
			}
			if( nested ) codePointer++;
		}
		
		private void ScanToMatchingOpen(bool nested)
		{
			while( codeArray[codePointer] != '[' )
			{
				codePointer--;
				if( codeArray[codePointer] == ']' ) ScanToMatchingOpen(true);
			}
			if( nested ) codePointer--;
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
