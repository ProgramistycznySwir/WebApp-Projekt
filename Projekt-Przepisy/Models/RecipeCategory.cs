using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [KeyAttribute]
        public int ID { get; set; }
        // CategoryName
        [MaxLength(64)]
        public string Name { get; set; }

        public int AssignedRecipesCount { get; set; }

        //public RecipeCategory(string categoryName = null)
        //{
        //    Name = categoryName;
        //    AssignedRecipesCount = 1;
        //}
    }
}
