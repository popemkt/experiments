ref: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/proposals/csharp-8.0/default-interface-methods
- Key takeaways
    - Provide default implementation in interfaces
    - Enables **traits** programming
    - Can be reabstracted, ie. abstracted by derived interface
    - Classes **don't inherit** default methods, hence you **cannot call it directly** from class - must be **casted** to appropriate Interface to use such method

- Pros
    - Easier inheritance behaviour modeling
    - Better support for multi-inheritance
    - Allowed
        - member declarations that declare constants, operators, static constructors, and nested types;
        - a  __body__  for a method or indexer, property, or event accessor (that is, a "default" implementation);
        - member declarations that declare static fields, methods, properties, indexers, and events;
        - member declarations using the explicit interface implementation syntax; and
        - Explicit access modifiers (the default access is `public`).
- Limitations
  - In keytakeaways //TODO: work on this