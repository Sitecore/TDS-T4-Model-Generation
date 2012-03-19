using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Herskind.Model.Helper
{
    public class TemplateMappingAttribute : System.Attribute
    {
        public readonly string id;

        public string Id { get; set; }

        public TemplateMappingAttribute(string id)
        {
            this.Id = id;
        }
    }
}