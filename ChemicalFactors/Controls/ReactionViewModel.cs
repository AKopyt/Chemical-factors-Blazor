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

        public Dictionary<Element, int> GetAmountOfElementsOnList(List<IElement> list)
        {

            Dictionary<Element,int> amountOfElements = new Dictionary<Element,int>();
           
            foreach (var item in list)
            {
                if (item.GetType() == typeof(Element))
                {
                    var castedItem = (Element)item;
                    amountOfElements.Add(castedItem, 0);
                }
                else if (item.GetType() == typeof(IndexInReaction))
                {
                    IndexInReaction indexInReaction = (IndexInReaction)item;
                    int index = list.IndexOf(item);
                    IElement element = list.ElementAt(index - 1);
                    var castedItem = (Element)element;
                    amountOfElements[castedItem] += indexInReaction.Index;
                }

            }

            foreach (var pair in amountOfElements)
            {
                if (pair.Value == 0)
                {
                    amountOfElements[pair.Key] = 1;
                }
            }
            
            int chemicalfactor = 1;

            List<Element> elementsToMultiply = new List<Element>();

            foreach (var item in list)
            {
                if (item.GetType() == typeof(ChemicalFactor))
                {
                    var castedItem = (ChemicalFactor)item;
                    chemicalfactor = castedItem.Factor;
                }

                else if (item.GetType() == typeof(Element))
                {
                    Element element = (Element)item;
                    elementsToMultiply.Add(element);
                }
                else if (item.GetType() == typeof(MathElement))
                {
                    MathElement mathElement = (MathElement)item;
                    if (mathElement.MathSymbol == MathSymbols.Add)
                    {
                        foreach (var foundedElement in elementsToMultiply)
                        {
                            amountOfElements[foundedElement] = amountOfElements[foundedElement]* chemicalfactor; 
                           
                            
                        }
                        elementsToMultiply.Clear();
                    }
                }

                if (list.Last() == item)
                {
                    foreach (var foundedElement in elementsToMultiply)
                    {
                        amountOfElements[foundedElement] = amountOfElements[foundedElement]* chemicalfactor;


                    }
                    elementsToMultiply.Clear();

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
