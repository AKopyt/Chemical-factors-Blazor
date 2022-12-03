namespace ChemicalFactors.Controls
{
    public class ReactionViewModel
    {
        public ReactionViewModel()
        {
            SubstratsList = new List<Element>();
            ProductsList = new List<Element>();
            CurrentSide = SideOfReaction.Substrats;
        }

        public List<Element> SubstratsList { get; set; }
        public List<Element> ProductsList { get; set; }

        public SideOfReaction CurrentSide { get; set; }

        public void PushToList(Element element)
        {
            if (CurrentSide==SideOfReaction.Substrats)
                SubstratsList.Add(element);
            else
            {
                ProductsList.Add(element);
            }


        }

        public void ChangeToSubstratsSide()
        {
            CurrentSide = SideOfReaction.Substrats;
        }

        public void ChangeToProductsSide()
        {
            CurrentSide = SideOfReaction.Products;
        }
    }
}
