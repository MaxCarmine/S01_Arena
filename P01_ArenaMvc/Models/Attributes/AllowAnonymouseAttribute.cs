using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace P01_ArenaMvc.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymouseAttribute : Attribute
    {
    }
}
