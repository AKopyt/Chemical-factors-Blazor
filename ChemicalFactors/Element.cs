using System.Dynamic;

namespace ChemicalFactors
{
    public record Element : IElement
    {
        public Element(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; set; }

    }
}
