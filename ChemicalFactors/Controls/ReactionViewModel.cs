﻿using System.Collections;
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

       

        public Dictionary<Element, int> GetElementsBetweenBracketInCompound( List<IElement> compound)
        {

            Dictionary<Element, int> elementsBetweenBracketInSingleCompound = new Dictionary<Element, int>();
            bool elementsBetweenBracketsFounded = false;
            bool endOfBracketsFounded = false;
            
            foreach (var element in compound)
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

        public Dictionary<Element, int> MergeDictionary(Dictionary<Element, int> dictFirst,
            Dictionary<Element, int> dictSecond)
        {
            Dictionary<Element, int> mergedDictionary = new Dictionary<Element, int>();

            foreach (var pair in dictSecond)
            {
                if (dictFirst.ContainsKey(pair.Key))
                {
                    dictFirst[pair.Key] += pair.Value;
                }
                else
                {
                    dictFirst.Add(pair.Key, pair.Value);
                }
            }

            mergedDictionary = dictFirst;

            return mergedDictionary;

        }

        public Dictionary<Element, int> GetElementsFromCompound(List<IElement> compound)
        {
            Dictionary<Element, int> elementsInCompound = new Dictionary<Element, int>();

            var elementsBetweenBracket = GetElementsBetweenBracketInCompound(compound);
            var elementsExceptBracket = GetElementsExceptBracketInCompound(compound);

            elementsInCompound =  MergeDictionary(elementsExceptBracket, elementsBetweenBracket);


            return elementsInCompound;

        }

        
        public List<Compound> GetCompoundsFromReaction(List<IElement> listOfAllElementsInReaction, SideOfReaction sideOfReaction)
        {
           
            List<Compound> returnCompounds = new List<Compound>();
            List<IElement> SubList = new List<IElement>();

            foreach (IElement item in listOfAllElementsInReaction)
            {
                if (item.GetType() == typeof(MathElement))
                {

                    var castedItem = (MathElement)item;
                    if (castedItem.MathSymbol == MathSymbols.Add)
                    {
                        Compound compound = new Compound();
                        compound.SideOfReaction=sideOfReaction;
                        compound.AllElementsOfCompound = SubList;
                        compound.ElementsInCompound = GetElementsFromCompound(compound.AllElementsOfCompound);
                        returnCompounds.Add(compound);
                        SubList = new List<IElement>();

                    }
                    
                }
                else
                {
                    SubList.Add(item);
                    if (item == listOfAllElementsInReaction.Last())
                    {
                        Compound compound = new Compound();
                        compound.SideOfReaction=sideOfReaction;
                        compound.AllElementsOfCompound = SubList;
                        compound.ElementsInCompound = GetElementsFromCompound(compound.AllElementsOfCompound);
                        returnCompounds.Add(compound);
                    }

                }

            }

            return returnCompounds;


        }

       
    }

}




