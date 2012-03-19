using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Herskind.Model.Helper.FieldTypes
{
    public interface IListFieldWrapper : IFieldWrapper
    {
        IEnumerable<IItemWrapper> Items { get; }
    }
}
