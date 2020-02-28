namespace QueryTalk.Security
{
    internal enum ResponseType
    {
        /// <summary>
        /// El acceso a la aplicación es rechazado. Es invalido: RequestKey o ProductKey o MachineKey
        /// </summary>
        Denied,
        /// <summary>
        /// El acceso a la aplicación es rechazado. El plazo ha caducado.
        /// </summary>
        Expired,
        /// <summary>
        /// Se permite el acceso a la aplicación. 
        /// </summary>
        Allowed,
        /// <summary>
        /// El vínculo entre la ProductKey y MachineKey es borrado.
        /// </summary>
        Reset
    }
}
