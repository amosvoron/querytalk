namespace QueryTalk.Mapper
{
    // used for nodes and columns
    internal interface INode
    {
        // table/column identifier of the DB3 definition
        int NodeID { get; set; }
    }
}
