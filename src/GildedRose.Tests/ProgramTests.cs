using System.Linq;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void Main_QualityIsNeverNegative()
        {
            // Arrange

            // Act
            Program.Run();

            // Assert
            var items = Program.UpdatedItems;

            foreach (var item in items)
            {
                Assert.That(item.Quality,Is.GreaterThanOrEqualTo(0),"Quality for '{0}' is negative",item.Name);
            }
        }

        [Test]
        public void Main_QualityIsNeverMoreThanFifty()
        {
            // Arrange

            // Act
            Program.Run();

            // Assert
            var items = Program.UpdatedItems;

            foreach (var item in items)
            {
                Assert.That(item.Quality, Is.LessThanOrEqualTo(50), "Quality for '{0}' is greater than 50", item.Name);
            }
        }

        [Test]
        [TestCase("Aged Brie",1)]
        [TestCase("Sulfuras, Hand of Ragnaros", 50)]
        [TestCase("+5 Dexterity Vest", 19)]
        [TestCase("Elixir of the Mongoose", 6)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 21)]
        [TestCase("Conjured Mana Cake", 5)]
        public void Main_Item_QualityIsUpdated(string name, int expectedQuality)
        {
            // Arrange

            // Act
            Program.Run();

            // Assert
            var item = Program.UpdatedItems.First(i => i.Name == name);
            Assert.That(item.Quality,Is.EqualTo(expectedQuality),"Quality for '{0}' is incorrect",name);
        }

        [Test]
        [TestCase("Aged Brie", 1)]
        [TestCase("Sulfuras, Hand of Ragnaros", 0)]
        [TestCase("+5 Dexterity Vest", 9)]
        [TestCase("Elixir of the Mongoose", 4)]
        [TestCase("Backstage passes to a TAFKAL80ETC concert", 14)]
        [TestCase("Conjured Mana Cake", 2)]
        public void Main_Item_SellInIsDecrementedBy1(string name, int expectedSellIn)
        {
            // Arrange

            // Act
            Program.Run();

            // Assert
            var item = Program.UpdatedItems.First(i => i.Name == name);
            Assert.That(item.SellIn, Is.EqualTo(expectedSellIn), "SellIn for '{0}' is incorrect", name);
        }
    }
}