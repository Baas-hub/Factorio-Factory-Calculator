using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioFactoryCalc.Models
{
    public class Factory
    {
        public string Name { get; set; }
        public decimal CraftingSpeedMultiplier { get; set; }
        public FactoryType FactoryType { get; set; }
    }
}
