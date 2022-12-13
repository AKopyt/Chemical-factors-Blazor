namespace ChemicalFactors.Controls
{
    public class ReactionViewModel
    {
        public ReactionViewModel()
        {
            SubstratsList = new List<IElement>();
            ProductsList = new List<IElement>();
            CurrentSide = SideOfReaction.Substrats;
        }

        public List<IElement> SubstratsList { get; set; }
        public List<IElement> ProductsList { get; set; }

        public SideOfReaction CurrentSide { get; set; }

        public void PushToList(IElement element)
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

        public ReactionResult CalculateReaction(List<IElement> substratsList, List<IElement> productList)
        {
            throw new NotImplementedException();
        }

        public Dictionary<IElement, int> GetAmountOfElementsOnList(List<IElement> list)
        {

            Dictionary<IElement,int> amountOfElements = new Dictionary<IElement,int>();
           
            foreach (var item in list)
            {
                if (item.GetType() == typeof(Element))
                {
                    amountOfElements.Add(item, 0);
                }
                else if (item.GetType() == typeof(IndexInReaction))
                {
                    IndexInReaction indexInReaction = (IndexInReaction)item;
                    int index = list.IndexOf(item);
                    IElement element = list.ElementAt(index - 1);
                    amountOfElements[element] += indexInReaction.Index;
                }

            }

            foreach (var pair in amountOfElements)
            {
                if (pair.Value == 0)
                {
                    amountOfElements[pair.Key] = 1;
                }
                   

            }

            return amountOfElements;
        }

        public void CompareBothSideOfReaction()
        {



        }

        public void ChemicalReactionWithSelectedFactors()
        {



        }









    }
}
