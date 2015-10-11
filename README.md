# Unification
### About the project
This repository is part of a project as preparation for my bachelor thesis.
### Goals
The goal of the full project is to show off advantages/disadvantages of functional programming languages and how functional, object-oriented and imperative languages can be combined for best results.

### About this subproject
Unification is used for type inference inside many compilers. It's easy to implement in functional languages but needs a bit more effort in imperative or object-oriented languages.

Let's say input1 = f(X,f(3,2)) and input2 =  f(g(Y,5),Y) where lowercase strings are functions and uppercase strings are variables.
The most general unifier (mgu) of input1 and input2 would be f(g(f(3,2),5),f(3,2)).
The mgu for two functions is unique.

### Programming languages in use
##### (Some may not be implemented yet):
C, C#, F#, Haskell, Java, Nemerle, Scala

### Documentation
##### C# 
The program takes two arguments to not only show the usage instructions.

Possible tokens:
- Values:   "quoted string", 3.24E2(float), 42(int), true/false(bool) 
- Variable: SOMETHING, A, B, X, (Any uppercase string without symbols or spaces) 
- Function: foo(bar(3.141,true, Y), X, 42), lowercase("letters"), f(g(X)) 

The project contains a highly reusablabe lexer, Regex patterns are defined in attributes on the tokens. 


### Related repos:

[QuickSort -- Comming soon](https://www.github.com/h3ll5ur7er/)

[GuiApplication -- Comming soon](https://www.github.com/h3ll5ur7er/)

[Project2ToolBox -- Comming soon](https://www.github.com/h3ll5ur7er/)


### Note
The full project containing all program sources and the documentation is hosted on a private repository within the [BFH](http://www.ti.bfh.ch/).
