using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Stationery
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ApplicationContext db = new())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                Type WritingInstruments = new Type { Name = "Writing Instruments" };
                Type paperProducts = new Type { Name = "Paper Products" };
                Type officeTools = new Type { Name = "Office Tools" };
                db.Types.AddRange(WritingInstruments, paperProducts, officeTools);

                Item pen = new Item { Name = "Pen", Type = WritingInstruments, amount = 500, selfprice = 130 };
                Item notebook = new Item { Name = "Notebook", Type = paperProducts, amount = 600, selfprice = 200 };
                Item stapler = new Item { Name = "Stapler", Type = officeTools, amount = 700, selfprice = 100 };
                Item ruler = new Item { Name = "Ruler", Type = officeTools, amount = 700, selfprice = 100 };
                db.Items.AddRange(pen, notebook, stapler, ruler);

                Manager John = new Manager { Name = "John", Surname = "Smith" };
                Manager Brad = new Manager { Name = "Brad", Surname = "Pitt" };
                Manager Nick = new Manager { Name = "Nick", Surname = "Cobain" };
                db.Managers.AddRange(John, Brad, Nick);

                Buyer Kurt = new Buyer { Name = "Kurt" };
                Buyer Jason = new Buyer { Name = "Jason" };
                Buyer Alex = new Buyer { Name = "Alex" };
                db.Buyers.AddRange(Kurt, Jason, Alex);

                Selling s1 = new Selling { price = 200, amount = 30, date = new DateOnly(2025, 1, 13), Item = pen, Manager = John, Buyer = Kurt };
                Selling s2 = new Selling { price = 300, amount = 23, date = new DateOnly(2025, 1, 20), Item = notebook, Manager = Brad, Buyer = Jason };
                Selling s3 = new Selling { price = 150, amount = 56, date = new DateOnly(2025, 2, 2), Item = stapler, Manager = Nick, Buyer = Alex };
                db.Sellings.AddRange(s1, s2, s3);
                db.SaveChanges();

                int user_choice;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to the stationery!\n" +
                            "1.Enter 1 to show all info;\n" +
                            "2.Enter 2 to show all types;\n" +
                            "3.Enter 3 to show all managers;\n" +
                            "4.Enter 4 to show max amount of items;\n" +
                            "5.Enter 5 to show min amount of items;\n" +
                            "6.Enter 6 to show min selfcost;\n" +
                            "7.Enter 7 to show max selftcost;\n" +
                            "8.Enter 8 to show item by type;\n" +
                            "9.Enter 9 to show sold items by manager;\n" +
                            "10.Enter 10 to show items bought by buyer;\n" +
                            "11.Enter 11 to show the newest sell;\n" +
                            "12.Enter 12 to show average amount of items;\n"+
                            "0 - to exit");
                    int.TryParse(Console.ReadLine(), out user_choice);
                    switch (user_choice)
                    {
                        case 1:
                            showInfo();
                            break;
                        case 2:
                            showTypes();
                            break;
                        case 3:
                            showMangers();
                            break;
                        case 4:
                            showMaxAmount();
                            break;
                        case 5:
                            showMinAmount();
                            break;
                        case 6:
                            showMinSelfPrice();
                            break;
                        case 7:
                            showMaxSelfPrice();
                            break;
                        case 8:
                            showItemBycategory();
                            break;
                        case 9:
                            showItemByManager();
                            break;
                        case 10:
                            showItemByBuyer();
                            break;
                        case 11:
                            showNewestSell();
                            break;
                        case 12:
                            averageAmount();
                            break;
                        case 0:
                            Console.WriteLine("Good bye!");
                            break;
                        default:
                            Console.WriteLine("Wrong input!");
                            break;
                    }
                    if (user_choice != 0)
                    {
                        Console.WriteLine("\nPress any key to return to the menu...");
                        Console.ReadKey();
                    }
                } while (user_choice != 0);
                //showInfo();
                //showTypes();
                //showMangers();
                //showMinAmount();
                //showItemBycategory();
                //showItemByManager();
                //showItemByBuyer();
                //showNewestSell();
                //averageAmount();
            }
        }

        public static void showInfo()
        {
            using (ApplicationContext db = new())
            {
                var items = db.Items.ToList();
                Console.WriteLine("All items:");
                foreach (Item i in items)
                {
                    Console.WriteLine('\t' + i.Name);
                }
            }
        }
        public static void showTypes()
        {
            using (ApplicationContext db = new())
            {
                var types = db.Types.ToList();
                Console.WriteLine("All types:");
                foreach (Type t in types)
                {
                    Console.WriteLine('\t' + t.Name);
                }
            }
        }

        public static void showMangers()
        {
            using (ApplicationContext db = new())
            {
                var managers = db.Managers.ToList();
                Console.WriteLine("All Managers:");
                foreach (Manager m in managers)
                {
                    Console.WriteLine('\t' + m.Name + ' ' + m.Surname);
                }
            }
        }
        public static void showMaxAmount()
        {
            using (ApplicationContext db = new())
            {
                int? maxAmount = db.Items.Max(s => (int?)s.amount);

                var item = db.Items.FirstOrDefault(s => s.amount == maxAmount);
                Console.WriteLine($"Minimum amouts has {item.Name} item");
            }
        }

        public static void showMinAmount()
        {
            using (ApplicationContext db = new())
            {
                int? minAmount = db.Items.Min(s => (int?)s.amount);

                var item = db.Items.FirstOrDefault(s => s.amount == minAmount);
                Console.WriteLine($"Maximum amouts has {item.Name} item");
            }
        }
        public static void showMinSelfPrice()
        {
            using (ApplicationContext db = new())
            {
                int? minSelfPrice = db.Items.Min(s => (int?)s.selfprice);

                var item = db.Items.FirstOrDefault(s => s.selfprice == minSelfPrice);
                Console.WriteLine($"Item with minimum selfprice: ");
            }
        }
        public static void showMaxSelfPrice()
        {
            using (ApplicationContext db = new())
            {
                int? maxSelfPrice = db.Items.Max(s => (int?)s.selfprice);

                var item = db.Items.FirstOrDefault(s => s.selfprice == maxSelfPrice);
                Console.WriteLine($"Item with minimum selfprice: ");
            }
        }
        public static void showItemBycategory()
        {
            using (ApplicationContext db = new())
            {
                string userInput;
                Console.WriteLine("Write a category: ");
                userInput = Console.ReadLine();

                var items = db.Items.Where(i => i.Type.Name == userInput).ToList();
                foreach (var item in items)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }
        public static void showItemByManager()
        {
            using (ApplicationContext db = new())
            {
                string userInput;
                Console.WriteLine("Input manager's name: ");
                userInput = Console.ReadLine();

                var items = db.Sellings.Include(s => s.Item).Include(s => s.Manager).Where(s => s.Manager.Name == userInput).ToList();
                Console.WriteLine($"Manager {userInput} sold: ");
                foreach (var item in items)
                {
                    Console.WriteLine('\t'+item.Item.Name);
                }
            }
        }

        public static void showItemByBuyer()
        {
            using (ApplicationContext db = new())
            {
                string userInput;
                Console.WriteLine("Input buyer's name: ");
                userInput = Console.ReadLine();

                var items = db.Sellings.Include(s => s.Item).Include(s => s.Buyer).Where(s => s.Buyer.Name == userInput).ToList();
                Console.WriteLine($"Buyer {userInput} bought: ");
                foreach (var item in items)
                {
                    Console.WriteLine('\t' + item.Item.Name);
                }
            }
        }
        public static void showNewestSell()
        {
            using (ApplicationContext db = new())
            {
                DateOnly? maxDate = db.Sellings.Max(s => (DateOnly?)s.date);
                var item = db.Sellings.Include(s => s.Item).FirstOrDefault(s => s.date == maxDate);

                Console.WriteLine(item.Item.Name);
            }
        }

        public static void averageAmount()
        {
            using (ApplicationContext db = new())
            {
                var averages = db.Items.Include(s => s.Type)
                    .GroupBy(i => i.Type.Name)
                    .Select(g => new { TypeName = g.Key, averageAmount = g.Average(i => i.amount) })
                    .ToList();
                foreach (var item in averages)
                {
                    Console.WriteLine(item.TypeName + " - " + item.averageAmount);
                }
            }
        }

    }
}
