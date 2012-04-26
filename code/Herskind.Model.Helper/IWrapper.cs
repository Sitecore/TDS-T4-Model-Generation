using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Herskind.Model.Helper
{
    public interface IWrapper
    {
        object Original { get; set; }
        IItemFactory ItemFactory { get; set; }
    }
}
