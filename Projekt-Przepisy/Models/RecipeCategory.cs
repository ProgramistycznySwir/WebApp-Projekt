using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Przepisy.Models
{
    /// <summary>
    /// Entity of table Categories.
    /// </summary>
    public class RecipeCategory
    {
        // <PK>
        // CategoryID
        public int ID { get; set; }
        // CategoryName
        public string Name { get; set; }
    }
}
