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
        [Key]
        public int RecipeID { get; set; }
        // AssignedCategoryID
        [Key]
        public byte ID { get; set; }

        // <FK>
        public int CategoryID { get; set; }
    }
}
