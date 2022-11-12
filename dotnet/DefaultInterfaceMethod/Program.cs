// ref: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/default-interface-methods
using static System.Console;

#region Example
IA test = new MyClass();
test.M();
(test as I1)!.M1();
class MyClass : IA, I1 { }
interface IA
{
    void M() { WriteLine("IA.M"); }
}
interface IB : IA
{
    void IA.M() { WriteLine("IB.M"); } // Explicit implementation
}
interface IC : IA
{
    void M() { WriteLine("IC.M"); } // Creates a new M, unrelated to `IA.M`. Warning
}

interface I1
{
    void M1() {WriteLine("M1");}
}
#endregion

#region Reabstraction
interface IA1
{
    void M() { WriteLine("IA.M"); }
}
interface IB1 : IA1
{
    abstract void IA1.M();
}

class C : IB1 { } // error: class 'C' does not implement 'IA.M'.

#endregion