// ref: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/default-interface-methods
using static System.Console;

#region Example

Example.IA test = new Example.MyClass();
test.M();
(test as Example.I1)!.M1();

Derived.Main();

namespace Example
{
    
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

}
#endregion

#region Diamond Inheritance 

interface IA2
{
    void M();
}
interface IB2 : IA2
{
    new void M() { WriteLine("IB"); }
}
class Base : IA2
{
    void IA2.M() { WriteLine("Base"); }
}
class Derived : Base, IB2 // allowed?
{
    public static void Main()
    {
        IA2 a = new Derived();
        a.M();           // what does it do? Class takes precedence => Base.M()
    }
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