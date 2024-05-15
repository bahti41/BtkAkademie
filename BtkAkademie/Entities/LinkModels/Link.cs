using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LinkModels
{
    public class Link
    {
        public string? Href { get; set; }
        public string? Ref { get; set; }
        public string? Method { get; set; }
        
        public Link() 
        {

        }

        public Link(string? href, string? @ref, string? method)
        {
            Href = href;
            Ref = @ref;
            Method = method;
        }
    }
}
