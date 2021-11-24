
using System;
using System.Reflection;
using System.Reflection.Emit;


namespace Error_Checker
{
    internal static class Program
    {
        static List<MethodInfo> AllMethods = new List<MethodInfo>();
        
        static void Main() {
            Type t = typeof(PlanningTests);
            AllMethods.AddRange(t.GetMethods(BindingFlags.Public | BindingFlags.Static));

            foreach (MethodInfo m in AllMethods) {
                RunTest(m);
                Thread.Sleep(5);
            }

        }

        static void RunTest(Func<bool> func) {
            bool result = func();

            Console.Write(func.Method.Name + ": ");
            if (result) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\t\tPASSED");
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("FAILED");
            }
            Console.ResetColor();
        }
        static void RunTest(MethodInfo m) {
            bool result = (bool)m.Invoke(null, null);

            Console.Write(m.Name + ": ");
            if (result) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("PASSED");
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("FAILED");
            }
            Console.ResetColor();
        }

        static bool F() => true;
        static bool G() => false;
    }
}