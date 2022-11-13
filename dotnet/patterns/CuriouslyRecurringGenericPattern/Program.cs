// See https://aka.ms/new-console-template for more information
// ref: https://blog.stephencleary.com/2022/09/modern-csharp-techniques-1-curiously-recurring-generic-pattern.html
Console.WriteLine("Placeholder");

#region Example with interface
interface IExample<TDerived>
{
}

class MyExample : IExample<MyExample>
{
}
#endregion

#region Example with basetypes
abstract class ExampleBase<TDerived>
    where TDerived : ExampleBase<TDerived>
{
    // Methods in here can use `(TDerived)this` freely.
    // This is particularly useful if this interface wants to *return* a value of TDerived.
    public virtual TDerived Something() => (TDerived)this;
}

class AnotherExample : ExampleBase<AnotherExample>
{
    // Implicitly has `public AnotherExample Something();` defined.
    // The base class method already has the correct return type.
    // (Can still override if desired).
}
#endregion
