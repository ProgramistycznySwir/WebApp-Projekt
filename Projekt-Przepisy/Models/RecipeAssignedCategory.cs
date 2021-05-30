using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Przepisy.Models
{
    /// <summary>
    /// Entity of table RecipeAssignedCategories.
    /// </summary>
    public class RecipeAssignedCategory
    {
        [KeyAttribute]
        public uint RecipeID { get; set; }
        // AssignedCategoryID
        [KeyAttribute]
        public byte ID { get; set; }

        // <FK>
        public uint CategoryID { get; set; }
    }
}
