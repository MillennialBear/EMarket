//using EMarket.Data.Interfaces;
//using EMarket.Data.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;


//namespace EMarket.Data.mocks
//{
//    public class MockCategoty 
//    {
//        private readonly IMonitorsCategory _categoryMonitors = new MockCategory();
//        public IEnumerable<Monitor> Monitors {
//            get {
//                return new List<Monitor>() {
//                    new Monitor() { 
//                        Name = "PROLITE TF1015MC-B2",
//                        ShortDesc = "Open Frame PCAP 10 point touch screen equipped with a foam seal finish for seamless integration",
//                        LongDesc = "The ProLite TF1015MC (10.1 inch) uses PCAP touch technology and is built into an eye catching bezel " +
//                        "with edge-to-edge glass. Thanks to a glass overlay covering the screen and a rugged bezel, it guarantees high " +
//                        "durability and scratch-resistance, making it perfect for kiosk and high use public facing applications.",
//                        Img = "/img/toochscreen/PROLITE TF1015MC-B2.jpg",
//                        Price = 12000, 
//                        IsFavourite = true,
//                        Avalible = true,
//                        Category = _categoryMonitors.AllCategory.First()
//                    },
//                    new Monitor() { 
//                        Name = "PROLITE B1780SD-W1", 
//                        ShortDesc = "Perfect choice for office", 
//                        LongDesc = "The 17’’ ProLite B1780SD designed for business usage is a LED-backlit monitor with Height Adjustability" +
//                        " and Screen Rotation allowing you to set the perfect position of the screen ensuring ergonomic posture and optimal " +
//                        "viewing comfort.", 
//                        Img = "/img/desktop/PROLITE B1780SD-W1.jpg", 
//                        Price = 15000,
//                        IsFavourite = true,
//                        Avalible = true, 
//                        Category = _categoryMonitors.AllCategory.Last()
//                    }
//                };
//            }
//        }
//        public IEnumerable<Monitor> GetFavouriteMonitors { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

//        public Monitor GetObjectMonitor(int monitorId)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}
