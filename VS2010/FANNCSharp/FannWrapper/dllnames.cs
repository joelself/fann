namespace FANNCSharp
{
    internal class dllnames
    {
#if DEBUG
        internal const string floatDLLName = "fannfloatd";
#else
        internal const string floatDLLName = "fannfloat";
#endif


#if DEBUG
        internal const string doubleDLLName = "fanndoubled";
#else
        internal const string doubleDLLName = "fanndouble";
#endif


#if DEBUG
        internal const string fixedDLLName = "fannfixedd";
#else
        internal const string fixedDLLName = "fannfixed";
#endif
    }
}
