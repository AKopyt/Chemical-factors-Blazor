using System.ComponentModel;
using System.Configuration;
using ChemicalFactors;
using ChemicalFactors.Controls;
using FluentAssertions;

namespace ChemicalFactorsTests
{
    public class ReactionViewModelTest
    {
        [Fact]
        public void PushingToList()
        {
            //arrange
            Element element = new Element("H");
            ReactionViewModel sut = new ReactionViewModel();
            sut.CurrentSide = SideOfReaction.Products;

            //act
            sut.PushToList(element);
           
            //assert

            sut.SubstratsList.Contains(element).Should().BeFalse();
            sut.ProductsList.Contains(element).Should().BeTrue();
        }

        [Fact]
        public void ChangeSideOfReactionToSubstrats()
        {

            //arrange
            ReactionViewModel sut = new ReactionViewModel();
            //act

            sut.ChangeToSubstratsSide();
            //assert
            
            sut.CurrentSide.Should().Be(SideOfReaction.Substrats);

        }
        [Fact]
        public void ChangeSideOfReactionToProducts()
        {

            //arrange
            ReactionViewModel sut = new ReactionViewModel();
            //act
            sut.ChangeToProductsSide();
            //assert
            sut.CurrentSide.Should().Be(SideOfReaction.Products);

        }

        [Fact]
        public void SplitIntoSmallerListTets()
        {

            //arrange 

            List<IElement> SubstratList = new List<IElement>();
            List<IElement> HydrogenList = new List<IElement>();
            List<IElement> OxygenList = new List<IElement>();
            List<List<IElement>> ResultList = new List<List<IElement>>();
            ReactionViewModel reactionViewModel = new ReactionViewModel();

            HydrogenList.Add(new Element("H"));
            HydrogenList.Add(new IndexInReaction(2));

            SubstratList.AddRange(HydrogenList);
            SubstratList.Add(new MathElement(MathSymbols.Add));

            OxygenList.Add(new Element("O"));
            OxygenList.Add(new IndexInReaction(2));

            SubstratList.AddRange(OxygenList);

            ResultList.Add(HydrogenList);
            ResultList.Add(OxygenList);

            //act

            var result = reactionViewModel.SplitIntoSmallerList(SubstratList);


            //assert

            result.Count.Should().Be(2);
            result[0].Count.Should().Be(2);
            result[1].Count.Should().Be(2);

            
        }

        [Fact]
        public void GetElementsExceptBracketInCompoundTest()
        {
            //arrange
            //Ca(OH)2 
            List<IElement> FirstCompound = new List<IElement>();
            Dictionary<Element, int> expectedResult = new Dictionary<Element, int>();

            ReactionViewModel reactionViewModel = new ReactionViewModel();

            FirstCompound.Add(new Element("Ca"));
            FirstCompound.Add(new MathElement(MathSymbols.LeftBracket));
            FirstCompound.Add(new Element("O"));
            FirstCompound.Add(new Element("H"));
            FirstCompound.Add(new MathElement(MathSymbols.RightBracket));
            FirstCompound.Add(new IndexInReaction(2));


            //act
            var result = reactionViewModel.GetElementsExceptBracketInCompound(FirstCompound);
            //assert

            expectedResult.Add(new Element("Ca"), 1);

            expectedResult.Count.Should().Be(1);
            foreach (var pair in expectedResult)
            {
                result.Contains(pair).Should().BeTrue();
             
            }

        }

        [Fact]
        public void GetElementsBetweenBracketTest()
        {
            //arrange
            //Ca(OH)2 + COOH
            List<IElement> FirstCompound = new List<IElement>();
            List<IElement> SecondCOmpound = new List<IElement>();
            List<List<IElement>> MainList = new List<List<IElement>>();
            Dictionary<Element, int> expectedResult = new Dictionary<Element, int>();

            ReactionViewModel reactionViewModel = new ReactionViewModel();

            FirstCompound.Add(new Element("Ca"));
            FirstCompound.Add(new MathElement(MathSymbols.LeftBracket));
            FirstCompound.Add(new Element("O"));
            FirstCompound.Add(new Element("H"));
            FirstCompound.Add(new MathElement(MathSymbols.RightBracket));
            FirstCompound.Add(new IndexInReaction(2));

            SecondCOmpound.Add(new Element("C"));
            SecondCOmpound.Add(new Element("O"));
            SecondCOmpound.Add(new Element("O"));
            SecondCOmpound.Add(new Element("H"));

            MainList.Add(FirstCompound);
            MainList.Add(SecondCOmpound);

            //act
            var result = reactionViewModel.GetElementsBetweenBracketInReactionFromOneSide(MainList);
            //assert

            expectedResult.Add(new Element("O"), 2);
            expectedResult.Add(new Element("H"), 2);
           

            foreach (var pair in expectedResult)
            {
                result.Contains(pair).Should().BeTrue();

            }



        }
        [Fact]
        public void GetElementsBetweenBracketTest2()
        {
            //arrange
            //Ca(OH)2 + Ca(COOH)2
            List<IElement> FirstCompound = new List<IElement>();
            List<IElement> SecondCOmpound = new List<IElement>();
            List<List<IElement>> MainList = new List<List<IElement>>();
            Dictionary<Element, int> expectedResult = new Dictionary<Element, int>();

            ReactionViewModel reactionViewModel = new ReactionViewModel();

            FirstCompound.Add(new Element("Ca"));
            FirstCompound.Add(new MathElement(MathSymbols.LeftBracket));
            FirstCompound.Add(new Element("O"));
            FirstCompound.Add(new Element("H"));
            FirstCompound.Add(new MathElement(MathSymbols.RightBracket));
            FirstCompound.Add(new IndexInReaction(2));

            SecondCOmpound.Add(new Element("Ca"));
            SecondCOmpound.Add(new MathElement(MathSymbols.LeftBracket));
            SecondCOmpound.Add(new Element("C"));
            SecondCOmpound.Add(new Element("O"));
            SecondCOmpound.Add(new Element("O"));
            SecondCOmpound.Add(new Element("H"));
            SecondCOmpound.Add(new MathElement(MathSymbols.RightBracket));
            SecondCOmpound.Add(new IndexInReaction(2));

            MainList.Add(FirstCompound);
            MainList.Add(SecondCOmpound);

            //act
            var result = reactionViewModel.GetElementsBetweenBracketInReactionFromOneSide(MainList);
            //assert

            expectedResult.Add(new Element("O"), 6);
            expectedResult.Add(new Element("H"), 4);
            expectedResult.Add(new Element("C"), 2);


            foreach (var pair in expectedResult)
            {
                result.Contains(pair).Should().BeTrue();

            }



        }

       

        [Fact]
        public void GetElementsBetweenBracketInCompoundTest()
        {
            //arrange
            //Ca(OH)2 + CO(OH)2
            List<IElement> FirstCompound = new List<IElement>();
            List<IElement> SecondCOmpound = new List<IElement>();
            List<List<IElement>> MainList = new List<List<IElement>>();
            Dictionary<Element, int> expectedResult = new Dictionary<Element, int>();


            ReactionViewModel reactionViewModel = new ReactionViewModel();

            FirstCompound.Add(new Element("Ca"));
            FirstCompound.Add(new MathElement(MathSymbols.LeftBracket));
            FirstCompound.Add(new Element("O"));
            FirstCompound.Add(new Element("H"));
            FirstCompound.Add(new MathElement(MathSymbols.RightBracket));
            FirstCompound.Add(new IndexInReaction(2));


            //act
            var result = reactionViewModel.GetElementsBetweenBracketInCompound(FirstCompound);
            //assert

            expectedResult.Add(new Element("O"), 2);
            expectedResult.Add(new Element("H"), 2);


            foreach (var pair in expectedResult)
            {
                result.Contains(pair).Should().BeTrue();

            }



        }

        [Fact]
        public void AllElementsInCompound()
        {
            //elementsBetweenBracketInSingleCompound {H:1, O:3, Ca:5}
            //elementsExceptBracketInSingleCompound {H:2, Cl:2, Ca:2, Na:2}

            //arrange
            Dictionary<Element, int> elementsBetweenBracketInSingleCompound = new Dictionary<Element, int>();
            Dictionary<Element, int> elementsExceptBracketInSingleCompound = new Dictionary<Element, int>();
            Dictionary<Element, int> allElementsInCompound = new Dictionary<Element, int>();
            
            ReactionViewModel reactionViewModel = new ReactionViewModel();

            elementsBetweenBracketInSingleCompound.Add(new Element("H"), 1);
            elementsBetweenBracketInSingleCompound.Add(new Element("O"), 3);
            elementsBetweenBracketInSingleCompound.Add(new Element("Ca"), 5);

            elementsExceptBracketInSingleCompound.Add(new Element("H"),2);
            elementsExceptBracketInSingleCompound.Add(new Element("Cl"),2);
            elementsExceptBracketInSingleCompound.Add(new Element("Ca"),2);
            elementsExceptBracketInSingleCompound.Add(new Element("Na"),2);

            //act

            var result = reactionViewModel.AllElementsInCompound(ref elementsBetweenBracketInSingleCompound,
                ref elementsExceptBracketInSingleCompound);


            //assert

            allElementsInCompound.Add(new Element("H"), 3);
            allElementsInCompound.Add(new Element("O"), 3);
            allElementsInCompound.Add(new Element("Ca"), 7);
            allElementsInCompound.Add(new Element("Cl"), 2);
            allElementsInCompound.Add(new Element("Na"), 2);


            foreach (var pair in allElementsInCompound)
            {
                result.Contains(pair).Should().BeTrue();

            }

        }
        
        [Fact]
        public void CalculateReactionFirstTest()
        {
            //arrange
            List<IElement> substratsList = new List<IElement>();
            List<IElement> productList = new List<IElement>();


            ReactionResult expectedResult = new ReactionResult();
            ReactionViewModel reactionViewModel = new ReactionViewModel();

            //Al(Cl3) + K2(SO4) -> Al2(SO4)3 + K(CL)
            //result should be
            //2Al(Cl3) + 3K2(SO4) ->Al2(SO4)3 + 6K(Cl)

            substratsList.Add(new Element("Al"));
            substratsList.Add(new MathElement(MathSymbols.LeftBracket));
            substratsList.Add(new Element("Cl"));
            substratsList.Add(new IndexInReaction(3));
            substratsList.Add(new MathElement(MathSymbols.RightBracket));
            substratsList.Add(new MathElement(MathSymbols.Add));
            substratsList.Add(new Element("K"));
            substratsList.Add(new IndexInReaction(2));
            substratsList.Add(new MathElement(MathSymbols.LeftBracket));
            substratsList.Add(new Element("S"));
            substratsList.Add(new Element("O"));
            substratsList.Add(new IndexInReaction(4));
            substratsList.Add(new MathElement(MathSymbols.RightBracket));

            productList.Add(new Element("Al"));
            productList.Add(new IndexInReaction(2));
            productList.Add(new MathElement(MathSymbols.LeftBracket));
            productList.Add(new Element("S"));
            productList.Add(new Element("O"));
            productList.Add(new IndexInReaction(4));
            productList.Add(new MathElement(MathSymbols.RightBracket));
            productList.Add(new IndexInReaction(3));
            productList.Add(new MathElement(MathSymbols.Add));
            productList.Add(new Element("K"));
            productList.Add(new MathElement(MathSymbols.LeftBracket));
            productList.Add(new Element("Cl"));
            productList.Add(new MathElement(MathSymbols.RightBracket));

            //Act
            ReactionResult result = reactionViewModel.CalculateReaction(substratsList, productList);

            //Assert
            expectedResult.SubstratsList.Add(new ChemicalFactor(2));
            expectedResult.SubstratsList.Add(new Element("Al"));
            expectedResult.SubstratsList.Add(new MathElement(MathSymbols.LeftBracket));
            expectedResult.SubstratsList.Add(new Element("Cl"));
            expectedResult.SubstratsList.Add(new IndexInReaction(3));
            expectedResult.SubstratsList.Add(new MathElement(MathSymbols.RightBracket));
            expectedResult.SubstratsList.Add(new MathElement(MathSymbols.Add));
            expectedResult.SubstratsList.Add(new ChemicalFactor(3));
            expectedResult.SubstratsList.Add(new Element("K"));
            expectedResult.SubstratsList.Add(new IndexInReaction(2));
            expectedResult.SubstratsList.Add(new MathElement(MathSymbols.LeftBracket));
            expectedResult.SubstratsList.Add(new Element("S"));
            expectedResult.SubstratsList.Add(new Element("O"));
            expectedResult.SubstratsList.Add(new IndexInReaction(4));
            expectedResult.SubstratsList.Add(new MathElement(MathSymbols.RightBracket));

            expectedResult.ProductsList.Add(new Element("Al"));
            expectedResult.ProductsList.Add(new IndexInReaction(2));
            expectedResult.ProductsList.Add(new MathElement(MathSymbols.LeftBracket));
            expectedResult.ProductsList.Add(new Element("S"));
            expectedResult.ProductsList.Add(new Element("O"));
            expectedResult.ProductsList.Add(new IndexInReaction(4));
            expectedResult.ProductsList.Add(new MathElement(MathSymbols.RightBracket));
            expectedResult.ProductsList.Add(new IndexInReaction(3));
            expectedResult.ProductsList.Add(new MathElement(MathSymbols.Add));
            expectedResult.ProductsList.Add(new ChemicalFactor(6));
            expectedResult.ProductsList.Add(new Element("K"));
            expectedResult.ProductsList.Add(new MathElement(MathSymbols.LeftBracket));
            expectedResult.ProductsList.Add(new Element("Cl"));
            expectedResult.ProductsList.Add(new MathElement(MathSymbols.RightBracket));

            result.Should().Be(expectedResult);
        }

        [Fact]
        public void CalculateReactionSecondTest()
        {
            //arrange
            List<IElement> substratsList = new List<IElement>();
            List<IElement> productList = new List<IElement>();

          
            ReactionResult expectedResult = new ReactionResult();
            ReactionViewModel reactionViewModel = new ReactionViewModel();

            //Na2(SO4) + K(OH) -> Na(OH) + K2(SO4) 
            //result should be
            //Na2(SO4) + 2K(OH) -> 2Na(OH) + K2(SO4) 


            substratsList.Add(new Element("Na"));
            substratsList.Add(new IndexInReaction(2));
            substratsList.Add(new MathElement(MathSymbols.LeftBracket));
            substratsList.Add(new Element("S"));
            substratsList.Add(new Element("O"));
            substratsList.Add(new IndexInReaction(4));
            substratsList.Add(new MathElement(MathSymbols.RightBracket));
            substratsList.Add(new MathElement(MathSymbols.Add));
            substratsList.Add(new Element("K"));
            substratsList.Add(new MathElement(MathSymbols.LeftBracket));
            substratsList.Add(new Element("O"));
            substratsList.Add(new Element("H"));
            substratsList.Add(new MathElement(MathSymbols.RightBracket));

            productList.Add(new Element("Na"));
            productList.Add(new MathElement(MathSymbols.LeftBracket));
            productList.Add(new Element("O"));
            productList.Add(new Element("H"));
            productList.Add(new MathElement(MathSymbols.RightBracket));
            productList.Add(new MathElement(MathSymbols.Add));
            productList.Add(new Element("K"));
            productList.Add(new IndexInReaction(2));
            productList.Add(new MathElement(MathSymbols.LeftBracket));
            productList.Add(new Element("S"));
            productList.Add(new Element("O"));
            productList.Add(new IndexInReaction(4));
            productList.Add(new MathElement(MathSymbols.RightBracket));

            //Act
            ReactionResult result = reactionViewModel.CalculateReaction(substratsList, productList);
            //Assert
            expectedResult.SubstratsList.Add(new Element("Na"));
            expectedResult.SubstratsList.Add(new IndexInReaction(2));
            expectedResult.SubstratsList.Add(new MathElement(MathSymbols.LeftBracket));
            expectedResult.SubstratsList.Add(new Element("S"));
            expectedResult.SubstratsList.Add(new Element("O"));
            expectedResult.SubstratsList.Add(new IndexInReaction(4));
            expectedResult.SubstratsList.Add(new MathElement(MathSymbols.RightBracket));
            expectedResult.SubstratsList.Add(new MathElement(MathSymbols.Add));
            expectedResult.SubstratsList.Add(new ChemicalFactor(2));
            expectedResult.SubstratsList.Add(new Element("K"));
            expectedResult.SubstratsList.Add(new MathElement(MathSymbols.LeftBracket));
            expectedResult.SubstratsList.Add(new Element("O"));
            expectedResult.SubstratsList.Add(new Element("H"));
            expectedResult.SubstratsList.Add(new MathElement(MathSymbols.RightBracket));


            expectedResult.ProductsList.Add(new ChemicalFactor(2));
            expectedResult.ProductsList.Add(new Element("Na"));
            expectedResult.ProductsList.Add(new MathElement(MathSymbols.LeftBracket));
            expectedResult.ProductsList.Add(new Element("O"));
            expectedResult.ProductsList.Add(new Element("H"));
            expectedResult.ProductsList.Add(new MathElement(MathSymbols.RightBracket));
            expectedResult.ProductsList.Add(new MathElement(MathSymbols.Add));
            expectedResult.ProductsList.Add(new Element("K"));
            expectedResult.ProductsList.Add(new IndexInReaction(2));
            expectedResult.ProductsList.Add(new MathElement(MathSymbols.LeftBracket));
            expectedResult.ProductsList.Add(new Element("S"));
            expectedResult.ProductsList.Add(new Element("O"));
            expectedResult.ProductsList.Add(new IndexInReaction(4));
            expectedResult.ProductsList.Add(new MathElement(MathSymbols.RightBracket));

            result.Should().Be(expectedResult);

        }
        [Fact]
        public void TestExample()
        {
            //Arrange
            int a = 1;
            int b = 2;
            int exceptedResult = 3;

            //Act
            int result = a + b;

            //Assert
            result.Should().Be(exceptedResult);

        }
    }


}