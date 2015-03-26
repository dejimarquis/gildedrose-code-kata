using System.Runtime.CompilerServices;

namespace GildedRose.Console
{
    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

    public static class ItemExtensions
    {
        public static bool IsSellByDatePassed(this Item item)
        {
            return item.SellIn <= 0;
        }


        public static bool IsAgedBrie(this Item item)
        {
            return (item.Name ?? "").ToLower().Contains("aged brie");
        }

        public static bool IsSulfuras(this Item item)
        {
            return (item.Name ?? "").ToLower().Contains("sulfuras");
        }

        public static bool IsBackstagePass(this Item item)
        {
            return (item.Name ?? "").ToLower().Contains("backstage pass");
        }

        public static bool IsConjured(this Item item)
        {
            return (item.Name ?? "").Trim().ToLower().StartsWith("conjured");
        }
    }

    public static class IntegerExtensions
    {
        public static bool IsBetween(this int value, int larger, int smaller)
        {
            return value <= larger && value >= smaller;
        }
    }
}