using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiaYao.Authorization
{
    public class MyAuthentication:Attribute,IFilterMetadata
    {
    }

    public class MyNoAuthentication : Attribute, IFilterMetadata
    {
    }
}
