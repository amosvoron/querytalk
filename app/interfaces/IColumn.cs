using System.Collections.Generic;

namespace QueryTalk.Mapper
{
    internal interface IColumn
    {
        string SCHEMA { get; }
        string OBJECT_NAME { get; }
        int OBJECT_TYPE { get; }
        List<Column> Columns { get; set; }  // columns ordered by ORDINAL_POSITION
                                            // (needed for optimization)
        void SetNotCompliant();             
    }
}
