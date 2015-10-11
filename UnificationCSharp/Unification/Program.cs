using System;

namespace Unification
{
    /// <summary>
    /// Unification unifies two given expressions
    /// valid tokens:
    /// values   : "quoted string", 12.345E6 (float), 42 (int), true/false(bool)
    /// variable : SOMETHING, A, B, X, (Any lowercase string without symbols or spaces)
    /// function : foo(bar(3.141,true, Y), X, 42), lowercase("letters"), f(g(X))
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">arg[0] = function 1, arg[1] = function 2</param>
        static void Main(string[] args)
        {
            //var input1 = "f(X,f(3,2))";
            //var input2 = "f(g(Y,5),Y)";

            if (args.Length != 2)
            {
                Console.WriteLine("usage:   unification expr1 expr2\r\n" +
                                  "example: unification f(X,f(3,2)) f(g(Y,5),Y)\r\n" +
                                  "value:   \"quoted string\", 3.24E2(float), 42(int), true/false(bool)\r\n" +
                                  "vars:    EXAMPLE, ANOTHER, X, Y(All Uppercase)\r\n" +
                                  "funcs:   afunction(anther), f(\"value\", 123), g(3.141) (All lowercase, parameters contained within braces, separated by commas)");
                Environment.ExitCode = -1;
                return;
            }

            Console.WriteLine($"input 1: {args[0]}");
            Console.WriteLine($"input 2: {args[1]}");

            Console.WriteLine($"unified: {Parser.Unify(args[0], args[1])}");

            Environment.ExitCode = 0;
        }
    }
}
