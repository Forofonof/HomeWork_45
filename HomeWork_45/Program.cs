using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        const string CommandCreateQueue = "1";
        const string CommandServeСustomers = "2";
        const string CommandExit = "3";

        bool isWork = true;

        Store store = new Store();

        while (isWork)
        {
            Console.WriteLine($"{CommandCreateQueue} - Создать очередь.\n{CommandServeСustomers} - Обслужить клиентов.\n{CommandExit} - Выход.");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case CommandCreateQueue:
                    store.CreateCustomerQueue();
                    break;

                case CommandServeСustomers:
                    store.ServeCustomerQueue();
                    break;

                case CommandExit:
                    isWork = false;
                    break;

                default:
                    Console.WriteLine("Ошибка! Неверная команда.");
                    break;
            }
        }
    }
}

class Store
{
    private Queue<Сustomer> _customer = new Queue<Сustomer>();
    private List<Product> _products = new List<Product>();
    private Random _random = new Random(); 

    public Store()
    {
        _products.Add(new Product("Хлеб", GetPriceProduct()));
        _products.Add(new Product("Сыр", GetPriceProduct()));
        _products.Add(new Product("Колбаса", GetPriceProduct()));
        _products.Add(new Product("Бекон", GetPriceProduct()));
        _products.Add(new Product("Молоко", GetPriceProduct()));
        _products.Add(new Product("Вода", GetPriceProduct()));
        _products.Add(new Product("Мороженое", GetPriceProduct()));
    }

    public void CreateCustomerQueue()
    {
        int minimumNumber = 3;
        int maximumNumber = 6;

        int countBuyer = _random.Next(minimumNumber, maximumNumber);

        for (int i = 0; i < countBuyer; i++)
        {
            _customer.Enqueue(CreateBuyer());
        }

        Console.Clear();
        Console.WriteLine($"Очередь создана на {countBuyer} человек(a)");
    }

    public void ServeCustomerQueue()
    {
        if (_customer.Count == 0)
        {
            Console.WriteLine("Покупателей на кассе нет, создайте очередь.");
            return;
        }

        while (_customer.Count > 0)
        {
            _customer.Dequeue().BuyProducts();
        }
    }

    private int GetPriceProduct()
    {
        int minimumNumber = 2;
        int maximumNumber = 50;

        int priceProduct = _random.Next(minimumNumber, maximumNumber);
        return priceProduct;
    }

    private Сustomer CreateBuyer()
    {
        List<Product> products = new List<Product>();

        int minimumCountProduct = 3;
        int maximumCountProduct = 10;
        int minimumCountMoney = 50;
        int maximumCountMoney = 350;

        int countProduct = _random.Next(minimumCountProduct, maximumCountProduct);
        int countMoney = _random.Next(minimumCountMoney, maximumCountMoney);

        for (int i = 0; i < countProduct; i++)
        {
            products.Add(_products[_random.Next(_products.Count - 1)]);
        }
        return new Сustomer(countMoney, products);
    }
}

class Сustomer
{
    private int _money;
    private List<Product> _products;

    public Сustomer(int money, List<Product> products)
    {
        _money = money;
        _products = products;
    }

    public void BuyProducts()
    {
        ShowProductCart();

        Console.WriteLine($"Покупатель набрал товаров на сумму: {GetPurchaseAmount()}$. У него в кармане: {_money}$");

        if (_money >= GetPurchaseAmount())
        {
            Console.WriteLine($"Покупатель оплатил товары на сумму: {GetPurchaseAmount()}$");
        }
        else
        {
            RemoveUnaffordableProducts();
        }
    }

    private void ShowProductCart()
    {
        Console.WriteLine($"Корзина покупателя:");

        foreach (var product in _products)
        {
            Console.WriteLine($"{product.Title} - {product.Price}$ ");
        }
    }

    private int GetPurchaseAmount()
    {
        int purchaseAmount = 0;

        foreach (var product in _products)
        {
            purchaseAmount += product.Price;
        }

        return purchaseAmount;
    }

    private void RemoveUnaffordableProducts()
    {
        while (GetPurchaseAmount() >= _money)
        {
            RemoveProduct();
        }
    }

    private void RemoveProduct()
    {
        Random random = new Random();

        int index = random.Next(0, _products.Count);
        Product product = _products[index];
        _products.Remove(product);

        Console.WriteLine($"Покупатель выложил товар: {product.Title}. Ценой {product.Price}$");
    }
}

class Product
{
    public Product(string title, int price)
    {
        Title = title;
        Price = price;
    }

    public string Title { get; private set; }

    public int Price { get; private set; }
}