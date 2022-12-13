namespace ChemicalFactors;

public class ChemicalFactor : IElement
{
    public ChemicalFactor(int symbol)
    {
        Symbol = symbol.ToString();
        Factor = symbol;
    }
    public int Factor { get; set; }
    public string Symbol { get; set; }


}