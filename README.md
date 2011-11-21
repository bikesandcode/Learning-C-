**Brainfuck Interpreter**
=====

My first real program in any new language is an interpreter for this horrible language.

Project files are committed for MonoDevelop, I'm not terribly familiar with the C# build
tools yet so I wanted to be able to build it later in life.

Usage:

BrainfuckInterpreter.exe bfsourcefile.bf

This is a fairly vanilla BF interpreter. Data overflow/underflows wrap around. Going
off the left hand side of the code or data arrays will likely lead to badness. No
code validation features are really implemented short of a warning if you go outside
of the array bounds (lower bound).


