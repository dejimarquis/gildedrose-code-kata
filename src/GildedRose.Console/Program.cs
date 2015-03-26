using System;
using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program
    {
        IList<Item> Items;

        public static IList<Item> UpdatedItems { get; private set; } 

        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            Run();

            System.Console.ReadKey();

        }

        public static void Run()
        {
            var items = GetDefaultItems();
            Run(items);
        }

        private static List<Item> GetDefaultItems()
        {
            var items = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15,Quality = 20},
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };

            return items;
        }

        public static void Run(IList<Item> items)
        {
            var app = new Program()
            {
                Items = items
                
            };

            app.UpdateQuality();
            UpdatedItems = app.Items;
        }

        private void UpdateQuality()
        {
            foreach (var item in Items)
            {
                UpdateItem(item);
            }
        }

        private void UpdateItem(Item item)
        {
            UpdateItemQuality(item);
            UpdateItemSellIn(item);
        }

        private void UpdateItemQuality(Item item)
        {
            var qualityModifier = GetQualityModifier(item);
            item.Quality += qualityModifier;

            if (item.IsBackstagePass() && item.IsSellByDatePassed())
                item.Quality = 0;

            QualityCanNotBeMoreThan50(item);
            QualityCanNotBeNegative(item);
        }

        private void UpdateItemSellIn(Item item)
        {
            var sellInModifier = GetSellInModifier(item);
            item.SellIn += sellInModifier;
        }

        private int GetQualityModifier(Item item)
        {
            if (item.IsAgedBrie())
            {
                if (item.IsSellByDatePassed())
                    return 2;

                return 1;
            }

            if (item.IsSulfuras())
                return 0;

            if (item.IsBackstagePass())
            {
                if(item.SellIn.IsBetween(10,6))
                    return 2;

                if (item.SellIn.IsBetween(5, 1))
                    return 3;

                return 1;
            }

            var isConjuredItem = item.IsConjured();

            if (item.IsSellByDatePassed())
                return isConjuredItem ? -4:-2;

            return isConjuredItem ? -2:-1;
        }

        private int GetSellInModifier(Item item)
        {
            if (item.IsSulfuras())
                return 0;

            return -1;
        }

        private void QualityCanNotBeNegative(Item item)
        {
            item.Quality = Math.Max(item.Quality, 0);
        }

        private void QualityCanNotBeMoreThan50(Item item)
        {
            item.Quality = Math.Min(item.Quality, 50);
        }
    }
}
