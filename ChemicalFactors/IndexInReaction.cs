namespace ChemicalFactors;

public class IndexInReaction : IElement
{
    public IndexInReaction(int symbol)
    {
        Symbol = symbol.ToString();
        Value = symbol;
    }

    public int Value { get; set; }
    public string Symbol { get; set; }
}