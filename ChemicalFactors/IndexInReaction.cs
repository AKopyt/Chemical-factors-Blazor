namespace ChemicalFactors;

public class IndexInReaction : IElement
{
    public IndexInReaction(int symbol)
    {
        Symbol = symbol.ToString();
        Index = symbol;
    }

    public int Index { get; set; }
    public string Symbol { get; set; }
}