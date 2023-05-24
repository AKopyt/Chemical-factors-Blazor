using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;
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

        public void CheckIfElementIsLeftOrRightBracket(IElement element, ref bool skipElements, ref bool skipIndexAfterBracket)
        {
            if (element.GetType() == typeof(MathElement))
            {
                var castedItem = (MathElement)element;
                if (castedItem.MathSymbol == MathSymbols.LeftBracket)
                {
                    skipElements = true;
                }
                else if (castedItem.MathSymbol == MathSymbols.RightBracket)
                {
                    skipElements = false;
                    skipIndexAfterBracket = true;
                }
            }


        }

        public void UpdateElementsInDictionaryByIndexValue(IElement element, ref Dictionary<Element, int> dictionary, ref bool skipIndexAfterBracket, List<IElement> compounds)
        {

            if (element.GetType() == typeof(IndexInReaction))
            {
                IndexInReaction indexInReaction = (IndexInReaction)element;

                if (skipIndexAfterBracket == false)
                {
                    int index = compounds.IndexOf(element);
                    IElement oneItemBefore = compounds.ElementAt(index - 1);
                    if (oneItemBefore.GetType() == typeof(Element))
                    {
                        var castedItem = (Element)oneItemBefore;
                        dictionary[castedItem] += indexInReaction.Value;
                    }
                }
                else
                {
                    skipIndexAfterBracket = false;
                }
            }


        }
        public void AddOrUpdateElementDictionary(IElement element, ref Dictionary<Element, int> dictionary)
        {
            if (element.GetType() == typeof(Element))
            {
                var castedItem = (Element)element;

                if (dictionary.ContainsKey(castedItem))
                {
                    if (dictionary[castedItem] > 0)
                    {
                        dictionary[castedItem] += 1;
                    }
                    else
                    {
                        dictionary[castedItem] = 2;
                    }
                }
                else
                {
                    dictionary.Add(castedItem, 0);
                }
            }

        }

        public void ChangeZeroToOneInDictionary(ref Dictionary<Element, int> dictionary)
        {
            foreach (var pair in dictionary)
            {
                if (pair.Value == 0)
                {
                    dictionary[pair.Key] = 1;
                }
            }
        }

        public Dictionary<Element, int> GetElementsExceptBracketInCompound(List<IElement> compound)
        {
            Dictionary<Element, int> elementsExceptBracketInCompound = new Dictionary<Element, int>();

            bool skipElements = false;
            bool skipIndexAfterBracket = false;

                foreach (var element in compound)
                {
                    CheckIfElementIsLeftOrRightBracket(element, ref skipElements, ref skipIndexAfterBracket);

                    if (skipElements == false)
                    {
                        AddOrUpdateElementDictionary(element, ref elementsExceptBracketInCompound);
                        UpdateElementsInDictionaryByIndexValue(element, ref elementsExceptBracketInCompound, ref skipIndexAfterBracket, compound);
                    }
                }
            

            ChangeZeroToOneInDictionary(ref elementsExceptBracketInCompound);
            return elementsExceptBracketInCompound; 

        }

        public void CheckIfElementsAreBetweenBracket(IElement element, ref bool elementsBetweenBracketsFounded, ref bool endOfBracketsFounded)
        {
            if (element.GetType() == typeof(MathElement))
            {
                var castedItem = (MathElement)element;
                if (castedItem.MathSymbol == MathSymbols.LeftBracket)
                {
                    elementsBetweenBracketsFounded = true;
                }
                else if (castedItem.MathSymbol == MathSymbols.RightBracket)
                {
                    elementsBetweenBracketsFounded = false;
                    endOfBracketsFounded = true;

                }
            }
           
            
        }

        public void MultiplyElementBetweenBracketByIndex(IElement element, ref Dictionary<Element, int> dictionary, ref bool skipIndexAfterBracket, List<IElement> compounds)
        {
            if (element.GetType() == typeof(IndexInReaction))
            {
                IndexInReaction indexInReaction = (IndexInReaction)element;

                if (skipIndexAfterBracket == false)
                {
                     
                    int index = compounds.IndexOf(element);
                    foreach (var pair in dictionary)
                    {
                        dictionary[pair.Key] *= index;

                    }

                }
                else
                {
                    skipIndexAfterBracket = true;
                }
            }


        }

        public Dictionary<Element, int> GetElementsBetweenBracketInCompound( List<IElement> compounds)
        {

            Dictionary<Element, int> elementsBetweenBracketInSingleCompound = new Dictionary<Element, int>();
            bool elementsBetweenBracketsFounded = false;
            bool endOfBracketsFounded = false;
            
            foreach (var element in compounds)
            {
                CheckIfElementsAreBetweenBracket(element, ref elementsBetweenBracketsFounded, ref endOfBracketsFounded);

                if (elementsBetweenBracketsFounded == true)
                {
                    AddOrUpdateElementDictionary(element, ref elementsBetweenBracketInSingleCompound);
                    ChangeZeroToOneInDictionary(ref elementsBetweenBracketInSingleCompound);

                }
                else if (elementsBetweenBracketsFounded == false && endOfBracketsFounded ==true)
                {
                    if (element.GetType() == typeof(IndexInReaction))
                    {
                        IndexInReaction indexInReaction = (IndexInReaction)element;

                        foreach (var pair in elementsBetweenBracketInSingleCompound)
                        {
                            elementsBetweenBracketInSingleCompound[pair.Key] *= indexInReaction.Value;
                        }

                        endOfBracketsFounded = false;
                    }
                }

                
            }

            return elementsBetweenBracketInSingleCompound;
        }

        public Dictionary<Element, int> GetElementsBetweenBracketInReactionFromOneSide(List<List<IElement>> listOfCompoundsInReactionFromOneSide)
        {
            Dictionary<Element, int> elementsBetweenBrackets = new Dictionary<Element, int>();

            
            foreach (var compound in listOfCompoundsInReactionFromOneSide)
            {
                var singleCompundElementsDict = GetElementsBetweenBracketInCompound( compound);

                foreach (var pair in singleCompundElementsDict)
                {
                    if (elementsBetweenBrackets.ContainsKey(pair.Key))
                        elementsBetweenBrackets[pair.Key] += pair.Value;
                    else
                    {
                        elementsBetweenBrackets.Add(pair.Key, pair.Value);
                    }
                }

            }
            return elementsBetweenBrackets;
        }



        public Dictionary<Element, int> AllElementsInCompound(ref Dictionary<Element, int> singleCompundElementsDict, ref Dictionary<Element, int> elementsExceptBracket)
        {
            Dictionary<Element, int> allElementsInCompound = new Dictionary<Element, int>();

            

            return allElementsInCompound;

        }




    }

}




