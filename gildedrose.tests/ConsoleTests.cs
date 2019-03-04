using gildedrose.console;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace gildedrose.tests
{
    public class ConsoleTests
    {
        [Theory]
        [InlineData(10,20,22)]
        [InlineData(5,20,23)]
        [InlineData(5,50,50)]
        [InlineData(0,20,0)]
        public void BackstagePassesQualityTest(int sellin, int quality, int expectedQuality)
        {
            //Arrange
            var app = new Program();
            var Items = new List<Item>
                                          {
                                              new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellin, Quality = quality},
                                          };

            Program.Items = Items;


            //Act
            app.UpdateQuality();

            //Assert
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(10,20,21)]
        [InlineData(5, 50, 50)]
        [InlineData(0,20,22)]
        public void AgieBrieQualityTest(int sellin, int quality, int expectedQuality)
        {
            //Arrange
            var app = new Program();
            var Items = new List<Item>
                                          {
                                              new Item {Name = "Aged Brie", SellIn = sellin, Quality = quality},
                                          };

            Program.Items = Items;


            //Act
            app.UpdateQuality();

            //Assert
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(10, 20, 20)]
        [InlineData(0, 20, 20)]
        public void SulfuraQualityTest(int sellin, int quality, int expectedQuality)
        {
            //Arrange
            var app = new Program();
            var Items = new List<Item>
                                          {
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = sellin, Quality = quality},
                                          };

            Program.Items = Items;


            //Act
            app.UpdateQuality();

            //Assert
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(10,20,19)]
        [InlineData(0,20,18)]
        [InlineData(0,0, 0)]
        public void OtherItemQualityTest(int sellin, int quality, int expectedQuality)
        {
            //Arrange
            var app = new Program();
            var Items = new List<Item>
                                          {
                                              new Item {Name = "Random", SellIn = sellin, Quality = quality},
                                          };

            Program.Items = Items;


            //Act
            app.UpdateQuality();

            //Assert
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(10, 20, 18)]
        [InlineData(0, 20, 16)]
        [InlineData(0, 0, 0)]
        public void ConjuredItemQualityTest(int sellin, int quality, int expectedQuality)
        {
            //Arrange
            var app = new Program();
            var Items = new List<Item>
                                          {
                                              new Item {Name = "Conjured", SellIn = sellin, Quality = quality},
                                          };

            Program.Items = Items;


            //Act
            app.UpdateQuality();

            //Assert
            Assert.Equal(expectedQuality, Items[0].Quality);
        }
    }
}
