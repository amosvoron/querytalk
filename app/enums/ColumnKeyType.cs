namespace QueryTalk.Mapper
{
    internal enum ColumnKeyType : int
    {
        // no key
        NoKey = 0,
  
        // PK/UK only
        RK = 1,    
 
        // FK only
        FK = 2,     

        // PK/UK and FK
        RKFK = 3   
    }
}
