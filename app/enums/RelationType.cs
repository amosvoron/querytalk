namespace QueryTalk.Mapper
{
    internal enum RelationType : int
    {
        // relation between FK node and RK node
        ManyToOne   = 0,  // PersonJob : Person

        // relation between RK node in FK node (collection)
        OneToMany   = 1,  // Person : PersonJob

        // relation between single FK node and RK node
        SingleToOne = 2,  // PersonExtra : Person

        // relation between RK node and single FK node
        OneToSingle = 3,  // Person : PersonExtra

        // self-relation between FK and RK (= SelfToOne)
        Self        = 10,  // Child : Parent

        // -----------------------------------------------------------------------------------------
        // IMPORTANT!
        // The self-relation between the parent and children of the same table IS NOT SUPPORTED.
        // -----------------------------------------------------------------------------------------
        // self-relation between RK and FK
        //OneToSelf   = 5,  // Parent : Child
    }
}
