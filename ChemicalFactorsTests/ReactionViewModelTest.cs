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