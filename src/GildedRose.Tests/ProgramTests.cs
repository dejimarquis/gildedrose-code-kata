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

        
    }
}