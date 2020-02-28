namespace QueryTalk.Mapper
{
    // .Finalizer method on object to finalize object processing 
    // ending by setting the IsCompliant flag.
    internal interface IFinalizer
    {
        void Finalizer();
    }
}
