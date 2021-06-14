using System;

namespace Architecture.Domain
{
    [Flags]
    public enum Roles
    {
        None = 0,
        Worker = 1,
        Admin = 2
    }
}
