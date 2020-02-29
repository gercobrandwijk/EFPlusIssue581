using EFPlusIssue581.Data.Model.Base;
using System;

namespace EFPlusIssue581.Data
{
    public class Device : ModelBase
    {
        public string Name { get; set; }

        public bool Active { get; set; }
    }
}
