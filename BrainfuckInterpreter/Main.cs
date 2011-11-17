using System;

namespace BrainfuckInterpreter
{
	class MainClass
	{
		private int[] buffer = new int[30000];
		private int dataPointer = 0;
		private int codePointer = -1;
		
		private string program;
		private char[] codeArray;
		
		public MainClass( string program )
		{
			this.program = program;
			this.codeArray = program.ToCharArray();
		}
		
		
		public void Execute()
		{
			Console.WriteLine("Executing the program");			
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
						Console.WriteLine("Read in {0}", read);
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
		
		/**
		 * Called when a [ character is hit, loop start.
		 * If the data pointer is currently -> 0, jump to
		 * just past the next ].
		 * Otherwise, truck forward.
		 **/
		private void LoopStart()
		{
			if( buffer[dataPointer] == 0 )
			{
				//scan forward to next ]
				while( codeArray[codePointer] != ']' ) codePointer++;				
			}
			//else proceed normally				
		}
		
		/**
		 * Called when a ] character is hit. Scan back to the
		 * most recent [ character.
		 **/
		private void LoopEnd()
		{
			while( codeArray[codePointer] != '[') codePointer--;
			//And back so the next tick puts us on the loop start.
			codePointer--;
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
