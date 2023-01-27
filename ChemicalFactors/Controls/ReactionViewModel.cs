using System.ComponentModel.DataAnnotations;
using System.Text;
using ChemicalFactors;

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
            if (CurrentSide == SideOfReaction.Substrats)
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

        public List<List<IElement>> SplitIntoSmallerList(List<IElement> list)
        {
            List<List<IElement>> MainList = new List<List<IElement>>();
            List<IElement> SubList = new List<IElement>();

            foreach (IElement item in list)
            {
                if (item.GetType() == typeof(MathElement))
                {
                    var castedItem = (MathElement)item;
                    if (castedItem.MathSymbol == MathSymbols.Add)
                    {
                        MainList.Add(SubList);
                        SubList = new List<IElement>();
                    }
                }
                else
                {
                    SubList.Add(item);
                    if (item == list.Last())
                    {
                        MainList.Add(SubList);
                    }
                }
            }

            return MainList;
        }

        public Dictionary<Element, int> GetElementsExceptBracket(List<List<IElement>> list)
        {
            Dictionary<Element, int> elementsExceptBracket = new Dictionary<Element, int>();

            bool skipElements = false;
            foreach (var compounds in list)
            {
                foreach (var element in compounds)
                {

                    if (element.GetType() == typeof(MathElement))
                    {
                        var castedItem = (MathElement)element;
                        if (castedItem.MathSymbol == MathSymbols.LeftBracket)
                        {
                            skipElements = true; 
                            //?????????????
                        }
                    }

                    if (element.GetType() == typeof(Element))
                    {
                        var castedItem = (Element)element;
                        elementsExceptBracket.Add(castedItem, 0);

                    }
                    else if (element.GetType() == typeof(IndexInReaction))
                    {

                        int index = compounds.IndexOf(element);
                        IElement oneItemBefore = compounds.ElementAt(index - 1);
                        if (oneItemBefore.GetType() == typeof(Element))
                        {
                            IndexInReaction indexInReaction = (IndexInReaction)element;

                            var castedItem = (Element)oneItemBefore;
                            elementsExceptBracket[castedItem] += indexInReaction.Index;
                        }
                    }

                }
            }

            foreach (var pair in elementsExceptBracket)
            {
                if (pair.Value == 0)
                {
                    elementsExceptBracket[pair.Key] = 1;
                }
            }

            return elementsExceptBracket;

        }


        public Dictionary<Element, int> GetElementsBetweenBracket(List<List<IElement>> list)
        {

            Dictionary<Element, int> elementsBetweenBrackets = new Dictionary<Element, int>();

            foreach (var compounds in list)
            {
                foreach (var element in compounds)
                {
                   





                }



            }



            return elementsBetweenBrackets;
        }




    }

}




