using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        //Ref :Collection Navigation Property
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
