using System.Dynamic;

namespace ChemicalFactors
{
    public class Element : IElement
    {
        public Element(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; set; }

    }
}
