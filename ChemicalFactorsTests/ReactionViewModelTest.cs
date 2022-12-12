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
        public void CalculateReaction()
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
            expectedResult.SubstratsList.Add(new IndexInReaction(2));
            expectedResult.SubstratsList.Add(new Element("K"));
            expectedResult.SubstratsList.Add(new MathElement(MathSymbols.LeftBracket));
            expectedResult.SubstratsList.Add(new Element("O"));
            expectedResult.SubstratsList.Add(new Element("H"));
            expectedResult.SubstratsList.Add(new MathElement(MathSymbols.RightBracket));


            expectedResult.ProductsList.Add(new IndexInReaction(2));
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