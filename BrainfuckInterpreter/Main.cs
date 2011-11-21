using System;

namespace BrainfuckInterpreter
{
	class MainClass
	{
		private byte[] buffer = new byte[30000];
		private int dataPointer = 0;
		private int maxDataPointerSet = 0;
		private int codePointer = -1;
		
		private string program;
		private char[] codeArray;
		
		public MainClass( string program )
		{
			this.program = program;
			this.codeArray = program.ToCharArray();
		}
		
		/**
		 * Increments value under the data pointer
		 **/
		private void increment()
		{
			if( dataPointer > maxDataPointerSet ) maxDataPointerSet = dataPointer;
			buffer[dataPointer]++;	
		}
		
		/**
		 * Increments value under the data pointer
		 **/
		private void decrement(){ buffer[dataPointer]--; }
		private int bufferValue(){ return buffer[dataPointer];}
		
		private void incrementCodePointer(){ codePointer++; }
		private void decrementCodePointer(){ 
			codePointer--; 
			if( codePointer < 0 ){
				Console.WriteLine("Code Pointer has gone < 0 at codePoint {0}", codePointer);
				WriteMemory();
			}
		}
		private char codeValue(){ return codeArray[codePointer]; }
		
		private void incrementDataPointer(){ dataPointer++; }
		private void decrementDataPointer(){ 			
			dataPointer--; 
			if( dataPointer < 0 ){
				Console.WriteLine("Data Pointer has gone < 0 at codePoint {0}", codePointer);
				WriteMemory();
			}
		}
		
		private string padInt( int number )
		{
			if( number == 0 ) return "000";
			if( number < 10 ) return "00" + number.ToString();
			if( number < 100 ) return "0" + number.ToString();
			return number.ToString();
		}
		
		/**
		 * Console.WriteLines a snapshot of memory, pointer locations, etc.
		 **/
		public void WriteMemory()
		{
			///*
			Console.WriteLine("DataPtr: {0}", dataPointer);
			Console.WriteLine("CodePtr: {0}", codePointer);			
			
			var output = "";
			for( var i = 0; i <= maxDataPointerSet; i++ )
			{
				if( i % 20 == 0 ){
					Console.WriteLine(output);
					output = "";
				}
				output = output + padInt(buffer[i]) + " ";
			}			
			Console.WriteLine(output);
			Console.WriteLine("*********************");
			//*/
		}
		
		public void Execute()
		{
			while( codePointer < program.Length-1 )
			{
				incrementCodePointer();
				char next = codeValue();
				switch (next)
				{
					case '+': 
					{ 
						increment();
						break; 
					}
					case '-': 
					{ 
						decrement(); 
						break;
					}
					case '.': 
					{ 
						Console.Write((char)(buffer[dataPointer])); 
						break;
					}
					case ',': 
					{
						int read = Console.Read();
						buffer[dataPointer] = (byte)read;
						break;
					}
					case '[': 
					{ 
						LoopStart(); 
						break;
					}
					case ']': 
					{ 
						LoopEnd(); 
						break;
					}
					case '<': 
					{ 						
						decrementDataPointer();
						break;
					}
					case '>': 
					{
						incrementDataPointer();
						break;
					}
					default: {break;} //Ignore all else
				}				
			}

		}
		
		/**
		 * Called when a [ character is hit, loop start.
		 * Otherwise, truck forward.
		 **/
		private void LoopStart()
		{
			if( bufferValue() == 0 ){
				ScanToLoopEnd(); 				
			}
			
		}
		
		private void ScanToLoopEnd()
		{
			incrementCodePointer();
			while(codeValue() != ']')
			{				
				if( codeValue() == '[' ) ScanToLoopEnd();
				incrementCodePointer();
			}			
		}
		
		/**
		 * Called when a ] character is hit. Scan back to the
		 * most recent [ character.
		 **/
		private void LoopEnd()
		{
			if( bufferValue() != 0 ) ScanToLoopStart();
		}
		
		private void ScanToLoopStart()
		{
			decrementCodePointer();
			while( codeValue() != '[')
			{				
				if( codeValue() == ']' ) ScanToLoopStart(); 
				decrementCodePointer();
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
			//bf.WriteMemory();
			Console.WriteLine("\nExecution finished");
		}
	}
}
