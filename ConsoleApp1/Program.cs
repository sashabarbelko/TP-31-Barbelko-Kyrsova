using System.Diagnostics;
using System.Linq;
using System.Xml.Schema;
namespace ConsoleApp1;

public class Program
{
    public static void Main()
    {
        DBContext dbContext = new DBContext();
        IUserRepository userRepo = new UserRepository(dbContext);
        IProductRepository productRepo = new ProductRepository(dbContext);
        IOrderRepository orderRepo = new OrderRepository(dbContext);

        User currentUser = null;

        while (true) { 
            Console.WriteLine("╔═══════════════════════════════╗");
            Console.WriteLine("║          MAIN MENU            ║");
            Console.WriteLine("╠═══════════════════════════════╣");
            Console.WriteLine("║ 1 - Register User             ║");
            Console.WriteLine("║ 2 - Login                     ║");
            Console.WriteLine("║ 3 - View Products             ║");
            Console.WriteLine("║ 4 - Add Balance               ║");
            Console.WriteLine("║ 5 - Buy Product               ║");
            Console.WriteLine("║ 6 - View Purchase History     ║");
            Console.WriteLine("║ 7 - Show all clients          ║");
            Console.WriteLine("║ 8 - Exit                      ║");
            Console.WriteLine("╚═══════════════════════════════╝");
            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Try again.");
                continue;
            }

            if (choice == 8) break;

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter Username");
                    string Username = Console.ReadLine();
                    Console.WriteLine("Enter the password");
                    string Password = Console.ReadLine();

                    User newUser = new User { Username = Username, Password = Password };
                    userRepo.CreateUser(newUser);
                    Console.WriteLine($"User {Username} registred successfully.");
                    break;

                case 2:
                    Console.WriteLine("Enter Username");
                    Username = Console.ReadLine();
                    Console.WriteLine("Enter Password");
                    Password = Console.ReadLine();

                    currentUser = userRepo.GetUserByUsername(Username);
                    if (currentUser != null && currentUser.Password == Password)
                    {
                        Console.WriteLine($"Welcome {currentUser.Username}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid username or password.");
                    }
                    break;

                case 3:
                    Console.WriteLine("Available Products:");
                    foreach (var product in productRepo.GetAllProducts())
                    {
                        Console.WriteLine($"ID: {product.ProductID}, Name: {product.ProductName}, Price: {product.Price}, Stock: {product.Stock}");
                    }
                    break;

                case 4:
                    if (currentUser != null)
                    {
                        Console.WriteLine("Enter amount to add to your balance: ");
                        decimal amount = decimal.Parse(Console.ReadLine());
                        currentUser.Balance += amount;
                        dbContext.SaveChanges();
                        Console.WriteLine($"Your new balance is {currentUser.Balance}");
                    }
                    else
                    {
                        Console.WriteLine("Please log in first.");
                    }
                    break;

                case 5:
                    if (currentUser != null)
                    {
                        Console.WriteLine("Enter Product ID to buy:");
                        int productId = int.Parse(Console.ReadLine());
                        var product = productRepo.GetProductById(productId);

                        if (product != null && currentUser.Balance >= product.Price && product.Stock > 0)
                        {
                            currentUser.Balance -= product.Price;
                            product.Stock--;
                            Order order = new Order
                            {
                                Buyer = currentUser,
                                Products = new List<Product> { product },
                                Price = product.Price
                            };
                            orderRepo.CreateOrder(order);
                            Console.WriteLine($"You have successfully purchased {product.ProductName}. Your new balance is {currentUser.Balance}");
                            dbContext.SaveChanges();
                        }
                        else
                        {
                            Console.WriteLine("Insufficient balance or product not available.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please log in first.");
                    }
                    break;

                case 6:
                    if (currentUser != null)
                    {
                        Console.WriteLine("Your Purchase History:");
                        foreach (var order in orderRepo.GetAllOrders())
                        {
                            Console.WriteLine($"Order ID: {order.OrderID}, Date: {order.Date}, Total: {order.Price}");
                            foreach (var product in order.Products)
                            {
                                Console.WriteLine($"  Product: {product.ProductName}, Price: {product.Price}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please log in first.");
                    }
                    break;

                case 7:
                    foreach (var user in userRepo.GetAllUsers())
                    {
                        Console.WriteLine($"User ID: {user.UserId}, Balance: {user.Balance}, Name: {user.Username}");
                    }
                    break;

                case 111:
                    if (currentUser != null && currentUser.UserId == 1)
                    {
                        Console.WriteLine("╔═══════════════════════════════╗");
                        Console.WriteLine("║          ADMIN MENU           ║");
                        Console.WriteLine("╠═══════════════════════════════╣");
                        Console.WriteLine("║ 1 - Change Product            ║");
                        Console.WriteLine("║ 2 - Delete Product            ║");
                        Console.WriteLine("╚═══════════════════════════════╝");
                        Console.Write("Choose an option: ");

                        if (!int.TryParse(Console.ReadLine(), out int adminChoice))
                        {
                            Console.WriteLine("Invalid input. Try again.");
                            break;
                        }

                        switch (adminChoice)
                        {
                            case 1: 
                                Console.WriteLine("Enter Product ID to modify:");
                                int productId = int.Parse(Console.ReadLine());
                                var product = productRepo.GetProductById(productId);

                                if (product != null)
                                {
                                    Console.WriteLine("Enter new name for the product:");
                                    product.ProductName = Console.ReadLine();
                                    Console.WriteLine("Enter new price for the product:");
                                    product.Price = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Enter new stock quantity:");
                                    product.Stock = int.Parse(Console.ReadLine());

                                    productRepo.UpdateProduct(product);
                                    Console.WriteLine("Product updated successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Product not found.");
                                }
                                break;

                            case 2: 
                                Console.WriteLine("Enter Product ID to delete:");
                                productId = int.Parse(Console.ReadLine());
                                product = productRepo.GetProductById(productId);

                                if (product != null)
                                {
                                    productRepo.DeleteProduct(productId);
                                    Console.WriteLine("Product deleted successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Product not found.");
                                }
                                break;

                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("You are not an admin.");
                    }
                    break;


                default:
                    Console.WriteLine("Invalid choice.");
                    continue;

            }
        }
    }
}
