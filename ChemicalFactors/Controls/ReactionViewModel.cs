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

        public Dictionary<Element, int> GetElementsBetweenBracket(List<IElement> list)
        {

            Dictionary<Element, int> elementsBetweenBrackets = new Dictionary<Element, int>();

            foreach (var item in list)
            {
                if (item.GetType() == typeof(MathSymbols))
                {
                    // var castedItem = (MathSymbols.LeftBracket)
                    //castedItem = list.First();
                }

            }

            return elementsBetweenBrackets;
        }


        public Dictionary<Element, int> CountElementsByIndex(List<IElement> list)
        {
            Dictionary<Element, int> amountOfElements = new();

            foreach (var item in list)
            {
                if (item.GetType() == typeof(Element))
                {
                    var castedItem = (Element)item;
                    amountOfElements.Add(castedItem, 0);
                }
                else if (item.GetType() == typeof(IndexInReaction))
                {

                    int index = list.IndexOf(item);
                    IElement oneItemBefore = list.ElementAt(index - 1);
                    if (oneItemBefore.GetType() == typeof(Element))
                    {
                        IndexInReaction indexInReaction = (IndexInReaction)item;

                        var castedItem = (Element)oneItemBefore;
                        amountOfElements[castedItem] += indexInReaction.Index;
                    }
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
    }
}




    