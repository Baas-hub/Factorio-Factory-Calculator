using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioFactoryCalc.Models
{
    public class Recipe
    {
        public string Name { get; set; }
        public int OutputQuantity { get; set; }
        public decimal CraftingTime { get; set; }
        public List<RecipeComponent> Components { get; set; }
        public FactoryType RequiredFactoryType { get; set; }
    }
}
