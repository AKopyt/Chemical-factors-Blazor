using ChemicalFactors.Controls;

namespace ChemicalFactors
{
    public class Compound
    {
        public Dictionary<Element, int> CoumpoundInReaction { get; set; }

        public ChemicalFactor chemicalFactor = new(1);

        public SideOfReaction SideOfReaction { get; set; }



    }
}
