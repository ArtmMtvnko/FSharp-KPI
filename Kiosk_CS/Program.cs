using Microsoft.FSharp.Collections;
using Kiosk.Types;
using Kiosk.Modules;
using static Kiosk.Modules.Accounting;
using Additives = Kiosk.Types.Additives;
using static System.Math;

Console.WriteLine("Hello, World!");

var customer1 = new Customer<int>(
    id: 123321,
    name: "John Doe",
    mood: Mood.Good,
    order: new Order(
        name: ProductName.Burger,
        additives: [
            Additives.NewBurgerAdditive(BurgerAdditive.Cheese),
            Additives.NewBurgerAdditive(BurgerAdditive.Mushrooms),
        ]
        // [] instead of ListModules.OfSeq(new[] { Additives..., Additives... })
    )
);

var customer2 = new Customer<string>(
    id: "asdf123",
    name: "Boiler Smith",
    mood: Mood.Bad,
    order: new Order(
        name: ProductName.Coffee,
        additives: [
            Additives.NewCoffeeAdditive(CoffeeAdditive.Milk),
            Additives.NewCoffeeAdditive(CoffeeAdditive.Sugar),
        ]
    )
);

var income1 = Customer.makeOrder(customer1, "Hi! Can I get a burger? Also could you put some cheese and mushrooms?");
var income2 = Customer.makeOrder(customer2, "Gimme that coffee man! And I wanna see some milk and sugar there");

var account = new Account([income1]);
(account as IAccountable).AddTransaction(income2);

var (total, profit, avg, min, max) = extractStatistics(account);

Console.WriteLine($"Total transactions count: {total} \n" +
                  $"Profit: {Round(profit, 2)}$ \n" +
                  $"Average transaction amount: {Round(avg, 2)}$ \n" +
                  $"Mininum transaction amount: {Round(min, 2)}$ \n" +
                  $"Maximum transaction amount: {Round(max, 2)}$ \n" +
                  $"Profit (Gross): {Round(account.ProfitGross, 2)}$ \n" +
                  $"Total Expenses: {Round(account.Expenses, 2)}$ \n");
