using ProductManager.Services;
using ProductManager.UIModels;
using ProductManager.CommonComponents;
using System;

namespace ProductManager.Console
{
    internal class ConsoleApp
    {
        enum AppState
        {
            Default = 0,
            WarehouseDetails = 1,
            ProductEdit = 2,
            ProductCreate = 3,
            End = 4,
            Exit = 100,
        }

        private static AppState _appState = AppState.Default;
        private static StorageService _storageService;
        private static List<WarehouseUIModel> _warehouses;
        private static WarehouseService _warehouseService;
        private static ProductService _productService;
        private static int _currentWarehouseId = 0;
        static void Main(string[] args)
        {
            _storageService = new StorageService();
            _warehouseService = new WarehouseService(_storageService);
            _productService = new ProductService(_storageService);
            string? command = null;
            while (_appState != AppState.Exit)
            {
                switch (_appState)
                {
                    case AppState.WarehouseDetails:
                        WarehouseDetailsState(command);
                        break;
                    case AppState.ProductEdit:
                        ProductEditState(command);
                        break;
                    case AppState.ProductCreate:
                        ProductCreateState();
                        break;
                    case AppState.Default:
                        DefaultState();
                        break;
                }
                System.Console.WriteLine("--Type Exit to close application.\n");
                command = System.Console.ReadLine();
                UpdateState(command);
            }
        }
        private static void UpdateState(string? command)
        {
            switch (command)
            {
                case "Back":
                    switch (_appState)
                    {
                        case AppState.ProductEdit:
                            _appState = AppState.WarehouseDetails;
                            break;
                        case AppState.ProductCreate:
                            _appState = AppState.WarehouseDetails;
                            break;
                        default:
                            _appState = AppState.Default;
                            break;
                    }
                    break;
                case "Exit":
                    _appState = AppState.Exit;

                    System.Console.Clear();
                    System.Console.WriteLine("\x1b[3J");
                    System.Console.WriteLine("Thank you and see you later!");
                    break;
                default:
                    switch (_appState)
                    {
                        case AppState.Default:
                            _appState = AppState.WarehouseDetails;
                            break;
                        case AppState.WarehouseDetails:
                            if (command == "Create") _appState = AppState.ProductCreate;
                            else _appState = AppState.ProductEdit;
                            break;
                        case AppState.ProductEdit:
                            if (command == "Create") _appState = AppState.ProductCreate;
                            else _appState = AppState.ProductEdit;
                            break;
                        case AppState.End:
                            System.Console.WriteLine("Unknown command. Please try again.");
                            break;
                    }
                    break;
            }
        }
        private static Category GetCategory(string? category)
        {
            switch (category)
            {
                case "ElectricGuitars":
                    return Category.ElectricGuitars;
                case "GuitarPedals":
                    return Category.GuitarPedals;
                case "Amplifiers":
                    return Category.Amplifiers;
                case "Synthesizer":
                    return Category.Synthesizer;
                default:
                    return Category.ElectricGuitars;
            }
        }
        private static void LoadWarehouses()
        {
            if (_warehouses != null)
                return;
            _warehouses = new List<WarehouseUIModel>();
            foreach (var warehouse in _storageService.GetWarehouses())
            {
                var warehouseUIModel = _warehouseService.GetWarehouseUI(warehouse.Id);
                _warehouses.Add(warehouseUIModel);
            }
        }
        private static void EditUI(ref ProductUIModel product)
        {
            System.Console.WriteLine("--Enter new Name or press Enter to skip");
            string? input = System.Console.ReadLine();
            if (input != "") product.Name = input;

            System.Console.WriteLine("--Enter new Quantity or press Enter to skip");
            input = System.Console.ReadLine();
            if (input != "") product.Quantity = int.Parse(input);

            System.Console.WriteLine("--Enter new Price or press Enter to skip");
            input = System.Console.ReadLine();
            if (input != "") product.Price = double.Parse(input);

            System.Console.WriteLine("--Enter new Category or press Enter to skip");
            input = System.Console.ReadLine();
            if (input != "") product.Category = GetCategory(input);

            System.Console.WriteLine("--Enter new Description or press Enter to skip");
            input = System.Console.ReadLine();
            if (input != "") product.Description = input;

            System.Console.WriteLine("--Preview of the Product:");
            System.Console.WriteLine(product);
        }
        private static void DefaultState()
        {
            System.Console.Clear();
            System.Console.WriteLine("\x1b[3J");
            System.Console.WriteLine("Hello and welcome to the Product Manager Console App!");
            System.Console.WriteLine("--Here is the list of all Warehouses: ");

            LoadWarehouses();
            foreach (var warehouse in _warehouses)
            {
                System.Console.WriteLine(warehouse);
            }

            System.Console.WriteLine();
            System.Console.WriteLine("--Type the name of the Warehouse to see it's Products.");
        }
        private static void WarehouseDetailsState(string? warehouseName)
        {
            System.Console.Clear();
            System.Console.WriteLine("\x1b[3J");
            if (warehouseName == "Back") warehouseName = _warehouses[_currentWarehouseId].Name;
            bool warehouseExists = false;
            for (int i = 0; i < _warehouses.Count; i++)
            {
                if (_warehouses[i].Name == warehouseName)
                {
                    warehouseExists = true;
                    _currentWarehouseId = i;
                    _warehouses[i].Products = _productService.GetProductsUI(_warehouses[i].Id).ToList();
                    _warehouses[i].CalculateTotalCost();
                    System.Console.WriteLine($"--Products in {_warehouses[i].Name}:");
                    for (int j = 0; j < _warehouses[i].Products.Count; j++)
                    {
                        System.Console.WriteLine($"--{j+1}. {_warehouses[i].Products[j]}");
                    }
                }
            }
            if (!warehouseExists)
            {
                System.Console.WriteLine("--Warehouse not found! Please try again.\n");
                System.Console.WriteLine("--Type Back to get list of all Warehouses.");
            }
            else
            {
                System.Console.WriteLine("\n--Type a number of the Product to edit.");
                System.Console.WriteLine("--Type Create to create a Product for current Warehouse.");
                System.Console.WriteLine("--Type Back to get list of all Warehouses.");
            }
        }
        private static void ProductEditState(string? productNum)
        {
            System.Console.Clear();
            System.Console.WriteLine("\x1b[3J");

            int productNumConverted = int.Parse(productNum) - 1;
            if (productNumConverted >= 0 && productNumConverted < _warehouses[_currentWarehouseId].Products.Count)
            {
                var newProduct = _warehouses[_currentWarehouseId].Products[productNumConverted];

                System.Console.WriteLine(newProduct);
                System.Console.WriteLine();

                EditUI(ref newProduct);
                
                System.Console.WriteLine("\n--Type Save to save it in the Warehouse");
                string? input = System.Console.ReadLine();
                if (input == "Save")
                {
                    _productService.SaveProduct(newProduct);
                    _warehouses[_currentWarehouseId].Products[productNumConverted] = newProduct;
                    System.Console.WriteLine("--Product has been saved successfully!");
                }
                else System.Console.WriteLine("--Product wasn't saved!");
            }
            else
            {
                System.Console.WriteLine("--Out of range! To create a Product type Create");
            }
            System.Console.WriteLine("\n--Type Back to get list of all Products in this Warehouse.");
        }
        private static void ProductCreateState()
        {
            System.Console.Clear();
            System.Console.WriteLine("\x1b[3J");

            var newProduct = new ProductUIModel(_warehouses[_currentWarehouseId].Id);

            EditUI(ref newProduct);

            System.Console.WriteLine("\n--Type Save to save it in the Warehouse");
            string? input = System.Console.ReadLine();
            if (input == "Save")
            {
                _productService.SaveProduct(newProduct);
                _warehouses[_currentWarehouseId].Products.Add(newProduct);
                System.Console.WriteLine("Product has been saved successfully!");
            }
            else System.Console.WriteLine("Product wasn't saved");

            System.Console.WriteLine("\n--Type Back to get list of all Products in this Warehouse.");
        }

    }
}
