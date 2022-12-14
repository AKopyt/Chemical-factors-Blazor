namespace ChemicalFactors;

public class MathElement : IElement
{

    public MathElement(MathSymbols symbol)
    {
        if (symbol == MathSymbols.LeftBracket)
            Symbol = "(";
        if(symbol == MathSymbols.RightBracket)
            Symbol = ")";
        if(symbol == MathSymbols.Add)
            Symbol = "+";

        MathSymbol = symbol;
    }

    public string Symbol { get; set; }

    public MathSymbols MathSymbol { get; set; }
}