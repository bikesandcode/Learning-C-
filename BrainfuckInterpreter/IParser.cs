using System;

namespace BrainfuckInterpreter
{
	public interface IParser
	{
		bool Parse( string code );
		string Error();
	}
}

