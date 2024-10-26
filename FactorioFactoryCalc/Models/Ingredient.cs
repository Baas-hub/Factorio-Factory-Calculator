using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioFactoryCalc.Models
{
    public class Ingredient
    {
        public string Name { get; set; }
        public bool IsRawMaterial { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
