using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Przepisy.Models
{
    /// <summary>
    /// Entity of table RecipeAssignedCategories.
    /// </summary>
    public class RecipeAssignedCategory
    {
        //TODO: Place PK here!

        // AssignedCategoryID
        public byte ID { get; set; }
        // <FK>
        public int CategoryID { get; set; }
    }
}
