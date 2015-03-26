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
            var items = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };
            
            Run(items);
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
            UpdateQuality(item);
            UpdateSellIn(item);
        }

        private void UpdateQuality(Item item)
        {
            var qualityModifier = GetQualityModifier(item);
            item.Quality += qualityModifier;

            if (item.IsBackstagePass() && item.IsSellByDatePassed())
                item.Quality = 0;

            QualityCanNotBeMoreThan50(item);
        }

        private void UpdateSellIn(Item item)
        {
            var sellInModifier = GetSellInModifier(item);
            item.SellIn += sellInModifier;

            QualityCanNotBeNegative(item);
        }


        private int GetQualityModifier(Item item)
        {
            if (item.IsAgedBrie())
            {
                if (item.SellIn == 0)
                    return 2;

                return 1;
            }

            if (item.IsSulfuras())
                return 0;

            if (item.IsBackstagePass())
            {
                if(item.SellIn <= 10 && item.SellIn > 5)
                    return 2;

                if (item.SellIn <= 5 && item.SellIn > 0)
                    return 3;

                return 1;
            }

            if (item.IsSellByDatePassed())
                return -2;

            return -1;
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
            return item.Name == KnownItems.AgedBrie;
        }

        public static bool IsSulfuras(this Item item)
        {
            return item.Name == KnownItems.Sulfuras;
        }

        public static bool IsBackstagePass(this Item item)
        {
            return item.Name == KnownItems.BackstagePasses;
        }
    }

    public static class KnownItems
    {
        public const string AgedBrie = "Aged Brie";
        public const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        public const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
    }

}
