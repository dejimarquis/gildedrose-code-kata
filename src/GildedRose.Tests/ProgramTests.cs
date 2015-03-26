using System.Collections.Generic;
using System.Linq;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void Run_ExistingItems_QualityIsNeverNegative()
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
        public void Run_ExistingItems_QualityIsNeverMoreThanFifty()
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
        public void Run_ExistingItems_QualityIsUpdated(string name, int expectedQuality)
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
        public void Run_ExistingItems_SellInIsDecreaseddBy1(string name, int expectedSellIn)
        {
            // Arrange

            // Act
            Program.Run();

            // Assert
            var item = Program.UpdatedItems.First(i => i.Name == name);
            Assert.That(item.SellIn, Is.EqualTo(expectedSellIn), "SellIn for '{0}' is incorrect", name);
        }

        [Test]
        [TestCase(1,5,4)]
        [TestCase(0,5,3)]
        [TestCase(-1,5,3)]
        [TestCase(-2,5,3)]
        public void Run_SellDateHasPassed_QualityDegradesTwiceAsFast(int sellIn, int quality, int expectedQuality)
        {
            // Arrange
            var item = new Item {SellIn = sellIn, Quality = quality};

            // Act
            Program.Run(new List<Item>{item});

            // Assert
            Assert.That(item.Quality,Is.EqualTo(expectedQuality),"Quality does not match");
        }

        [Test]
        [TestCase(1,0)]
        [TestCase(0,0)]
        [TestCase(-1,0)]
        public void Run_Quality_NeverLessThanZero(int quality, int expectedQuality)
        {
            // Arrange
            var item = new Item { Quality = quality };

            // Act
            Program.Run(new List<Item> { item });

            // Assert
            Assert.That(item.Quality, Is.EqualTo(expectedQuality),"Quality should never be less than 0");
        }

        [Test]
        [TestCase("Aged Brie",51, 50)]
        [TestCase("Aged Brie",80, 50)]
        [TestCase("Something Else",80, 50)]
        public void Run_Quality_NeverMoreThan50(string name, int quality, int expectedQuality)
        {
            // Arrange
            var item = new Item {SellIn = 10, Quality = quality, Name = name};

            // Act
            Program.Run(new List<Item> { item });

            // Assert
            Assert.That(item.Quality, Is.EqualTo(expectedQuality),"Quality does not match for '{0}'",name);
        }

        [Test]
        [TestCase(0,1,3,"Old Brie")]
        [TestCase(1,1,2,"One day out")]
        [TestCase(2,1,2, "Two days out")]
        public void Run_AgedBrie_QualityIncreases_TheOlderItGets(int sellIn, int quality, int expectedQuality, string scenarioName)
        {
            // Arrange
            var item = new Item { SellIn = sellIn, Quality = quality, Name = "Aged Brie"};

            // Act
            Program.Run(new List<Item> { item });

            // Assert
            Assert.That(item.Quality, Is.EqualTo(expectedQuality),"Quality does not match for scenario '{0}'",scenarioName);
        }

        [Test]
        [TestCase(13, 13)]
        [TestCase(1, 1)]
        [TestCase(0, 0)]
        public void Run_Sulfuras_SellIn_NeverChanges(int sellIn, int expectedSellIn)
        {
            // Arrange
            var item = new Item { SellIn = sellIn, Quality = 1, Name = "Sulfuras, Hand of Ragnaros" };

            // Act
            Program.Run(new List<Item> { item });

            // Assert
            Assert.That(item.SellIn, Is.EqualTo(expectedSellIn),"SellIn does not match");
        }

        [Test]
        [TestCase(13, 13)]
        [TestCase(1, 1)]
        [TestCase(0, 0)]
        public void Run_Sulfuras_Quality_NeverChanges(int quality, int expectedQuality)
        {
            // Arrange
            var item = new Item { SellIn = 1, Quality = quality, Name = "Sulfuras, Hand of Ragnaros" };

            // Act
            Program.Run(new List<Item> { item });

            // Assert
            Assert.That(item.Quality, Is.EqualTo(expectedQuality),"Quality does not match");
        }

        [Test]
        [TestCase(11, 13, 14, "11 days away from the concert (Over 10 days away, increase by 1)")]
        [TestCase(10, 13, 15, "10 days away from the concert (10 or less days away, increase by 2")]
        [TestCase(9, 13, 15, "10 days away from the concert (10 or less days away, increase by 2")]
        [TestCase(5, 13, 16, "5 days away from the concert (5 or less days away, increase by 3")]
        [TestCase(4, 13, 16, "4 days away from the concert (5 or less days away, increase by 3")]
        [TestCase(1, 13, 16, "4 days away from the concert (5 or less days away, increase by 3")]
        [TestCase(0, 13, 0, "After the concert (Value is zero)")]
        public void Run_BackstagePasses_Quality_IncreasesWithTime(int sellIn, int quality, int expectedQuality, string scenarioName)
        {
            // Arrange
            var item = new Item { SellIn = sellIn, Quality = quality, Name = "Backstage passes to a TAFKAL80ETC concert" };

            // Act
            Program.Run(new List<Item> { item });

            // Assert
            Assert.That(item.Quality, Is.EqualTo(expectedQuality), "Quality does not match for scenario '{0}' with SellIn {1}",scenarioName, sellIn);
        }
    }
}