using EMarket.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace EMarket.Data
{
    public class DBObjects
    {
        public static void Initial(AppDbContext content)
        {
            if (!content.Category.Any())
                content.Category.AddRange(Categories.Select(c => c.Value));
            if (!content.Filters.Any())
                content.Filters.AddRange(filtersList);            
            if (!content.Monitor.Any())
            {
                content.Monitor.AddRange(
                    new Monitor()
                    {
                        Name = "PROLITE TF1015MC-B2",
                        ShortDesc = "Open Frame PCAP 10 point touch screen equipped with a foam seal finish for seamless integration",
                        LongDesc = "The ProLite TF1015MC (10.1 inch) uses PCAP touch technology and is built into an eye catching bezel " +
                        "with edge-to-edge glass. Thanks to a glass overlay covering the screen and a rugged bezel, it guarantees high " +
                        "durability and scratch-resistance, making it perfect for kiosk and high use public facing applications.",
                        Img = "/img/touchscreen/PROLITE TF1015MC-B2.png",
                        Price = 12000,
                        Diagonal = 10.1,
                        IsFavourite = true,
                        Avalible = true,
                        CategoryId = 1,
                        Color = "Black",
                        Category = Categories["Touchscreen"]
                    },
                    new Monitor()
                    {
                        Name = "PROLITE B1780SD-W1",
                        ShortDesc = "Perfect choice for office",
                        LongDesc = "The 17’’ ProLite B1780SD designed for business usage is a LED-backlit monitor with Height Adjustability " +
                        "and Screen Rotation allowing you to set the perfect position of the screen ensuring ergonomic posture and optimal " +
                        "viewing comfort.",
                        Img = "/img/desktop/PROLITE B1780SD-W1.png",
                        Price = 15000,
                        Diagonal = 17,
                        IsFavourite = false,
                        Avalible = true,
                        CategoryId = 2,
                        Color = "White",
                        Category = Categories["Desktop"]
                    }, 
                    new Monitor()
                    {
                        Name = "PROLITE E2083HSD-B1",
                        ShortDesc = "A reliable solution for both home and office use",
                        LongDesc = "The ProLite E2083HSD is a 19,5’’ LED-backlit monitor with 1600x900 resolution. It features 5msec response " +
                        "time and >5M: 1 Advanced Contrast Ratio assuring vibrant images and blur free Video.",
                        Img = "/img/desktop/PROLITE E2083HSD-B1.png",
                        Price = 17000,
                        Diagonal = 19.5,
                        IsFavourite = true,
                        Avalible = true,
                        CategoryId = 2,
                        Color = "Black",
                        Category = Categories["Desktop"]
                    },
                    new Monitor()
                    { 
                        Name = "PROLITE B1980D-B1",
                        ShortDesc = "The 19’’ Prolite B1980D designed for business, is an impressive LED-backlit monitor",
                        LongDesc = "Designed for business, this LED - backlit monitor with Height Adjustability and Screen Rotation allows you" +
                        " to set the perfect position of the screen ensuring ergonomic posture and optimal viewing comfort.The 5ms response time " +
                        "and high contrast make the B1980D ideal for a wide range of business applications.",
                        Img = "/img/desktop/PROLITE B1980D-B1.png",
                        Price = 18000,
                        Diagonal = 19,
                        IsFavourite = false,
                        Avalible = true,
                        CategoryId = 2,
                        Color = "Black",
                        Category = Categories["Desktop"]
                    },
                    new Monitor()
                    {
                        Name = "PROLITE XU2793HSU-B4",
                        ShortDesc = "27” IPS 3-side borderless monitor for multi-monitor set-ups",
                        LongDesc = "Stylish edge-to-edge design makes the ProLite XU2793HSU perfect for multi-monitor set-ups. The IPS panel " +
                        "technology offers accurate and consistent colour reproduction with wide viewing angles. High contrast and brightness " +
                        "values mean the monitor will provide excellent performance for photographic and web design.",
                        Img = "/img/desktop/PROLITE XU2793HSU-B4.png",
                        Price = 26000,
                        Diagonal = 27,
                        IsFavourite = true,
                        Avalible = true,
                        CategoryId = 2,
                        Color = "Black",
                        Category = Categories["Desktop"]
                    }, 
                    new Monitor() 
                    {
                        Name = "G-Master G2450HSU-B1",
                        ShortDesc = "Get ahead with the G2450HSU with VA Panel and 1ms MPRT",
                        LongDesc = "Featuring VA technology with 75Hz refresh rate and 1ms MPRT," +
                        " the 24’’ Full HD G2450HSU guarantees super image quality. The ability to " +
                        "customize the screen settings using the predefined and custom gaming modes " +
                        "along with the Black Tuner function gives you total control over the dark " +
                        "scenes and makes sure details are always clearly visible.",
                        Img = "/img/gaming/G-Master G2450HSU-B1.png",
                        Price = 30000,
                        Diagonal = 24,
                        IsFavourite = true,
                        Avalible = true,
                        CategoryId = 4,
                        Color = "Black",
                        Category = Categories["Gaming"]
                    },
                    new Monitor() 
                    {
                        Name = "G-Master G4380UHSU-B1",
                        ShortDesc = "43’’ 4K gaming monitor guaranteeing superb image quality",
                        LongDesc = "Packed into a superb VA matrix, the 8.3 million pixels (3840 x 2160)" +
                        " make the G-Master G4380UHSU an ideal screen for PC gamers looking to upgrade to 4K." +
                        " Featuring 0.4ms MPRT response time and supporting 144Hz over DisplayPort, the " +
                        "monitor allows you to make split second decisions and forget about ghosting effects" +
                        " or smearing issues.",
                        Img = "/img/gaming/G-Master G4380UHSU-B1.png",
                        Price = 49000,
                        Diagonal = 43,
                        IsFavourite = true,
                        Avalible = true,
                        CategoryId = 4,
                        Color = "Black",
                        Category = Categories["Gaming"]
                    }, 
                    new Monitor() 
                    {
                        Name = "PROLITE LE4340S-B3",
                        ShortDesc = "43” Full HD professional large format display with USB Media Playback",
                        LongDesc = "ProLite LE4340S is a 43'' Full HD, professional and fan-less LED-backlit " +
                        "display. Incorporating the latest Commercial Grade VA panel technology, the display " +
                        "guarantees great viewing from all angles, high brightness and exceptional colour " +
                        "clarity. A wide range of video and audio inputs ensures compatibility with multiple platforms.",
                        Img = "/img/lfd/PROLITE LE4340S-B3.png",
                        Price = 45000,
                        Diagonal = 43,
                        IsFavourite = true,
                        Avalible = true,
                        CategoryId = 3,
                        Color = "Black",
                        Category = Categories["LFD"]
                    }
                    );
            }
            content.SaveChanges();
        }

        private static Dictionary<string, Category> category;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (category == null)
                {
                    var list = new Category[] {
                        new Category() { CategoryName = "Touchscreen", Desc = "Touch screen interactive display" },
                        new Category() { CategoryName = "Desktop", Desc = "Monitors for both home and office use" },
                        new Category() { CategoryName = "LFD", Desc = "Monitors for digital advertising" },
                        new Category() { CategoryName = "Gaming", Desc = "Monitors for a comfortable gaming experience" }
                    };
                    category = new Dictionary<string, Category>();
                    foreach (var item in list)
                        category.Add(item.CategoryName, item);
                }
                return category;
            }
        }

        private static Filter[] filtersList = new Filter[]
        {
            new Filter() { NameFilter = "10-15", FilterDiagonal = "small" },
            new Filter() { NameFilter = "15,1-30", FilterDiagonal = "middle" },
            new Filter() { NameFilter = "30,1-50", FilterDiagonal = "large" },
            new Filter() { NameFilter = "50,1-120", FilterDiagonal = "huge" },
            new Filter() { NameFilter = "White", FilterColor ="White" },
            new Filter() { NameFilter = "Black", FilterColor = "Black" }
        };       
    }
}
