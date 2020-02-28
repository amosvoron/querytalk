namespace QueryTalk.Mapper
{
    internal enum ParameterMode : int
    {
        None = 0,       // no parameter - should be omitted (only in case of non-parameterized TableValued functions)
                        // Note: We assure that EVERY func/proc has at least one parameter
        In = 1,         // any input parameter
        InOut  = 2,     // OUTPUT parameter
        Out = 3         // return value of the scalar function
    }
}
