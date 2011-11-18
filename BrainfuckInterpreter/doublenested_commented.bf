,				Read character
.				Print
+++++>+++++			counter up 5
.				Print new character
[	
	>			move to blah spot
	++++++++++++++++++++
	++++++++++++++++++++
	++++++++++++++++++++
	++++++++++++++++++++	make something renderable in there
	.			Print it
	[-]			Set it back to 0
	<<			Back to ptr slot
	-			Decrement ptr slot
	>			Forward to counter
	-			decrement that too
]
<.				Back to ptr and print
