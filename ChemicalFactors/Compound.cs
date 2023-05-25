using System.Dynamic;
using ChemicalFactors.Controls;

namespace ChemicalFactors
{
    public class Compound
    {
        
        public Dictionary<Element, int> ElementsInCompound{ get; set; }

        public List<IElement> AllElementsOfCompound { get; set; }

        public ChemicalFactor ChemicalFactor { get; set; }

        public SideOfReaction SideOfReaction { get; set; }

        public Compound()
        {
            ElementsInCompound = new Dictionary<Element, int>();
            AllElementsOfCompound = new List<IElement>();
            ChemicalFactor = new ChemicalFactor(1);
            SideOfReaction = SideOfReaction.Substrats;
        }
    }
}
