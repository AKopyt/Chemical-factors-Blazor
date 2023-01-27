namespace ChemicalFactors;

public record ReactionResult
{
    public List<IElement> SubstratsList { get; set; }
    public List<IElement> ProductsList { get; set; }

    public ReactionResult()
    {

        SubstratsList = new List<IElement>();
        ProductsList = new List<IElement>();

    }   
}