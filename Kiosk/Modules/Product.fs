namespace Kiosk

type Additives =
    | CoffeeAdditive of CoffeeAdditive
    | HotdogAdditive of HotdogAdditive
    | BurgerAdditive of BurgerAdditive
    | None

and CoffeeAdditive =
    | Milk
    | Sugar
    | Syrup

and HotdogAdditive =
    | Ketchup
    | Mustard
    | Mayonnaise

and BurgerAdditive =
    | Cheese
    | Lettuce
    | Mushrooms
    | Onions

type ProductName =
    | Coffee
    | Hotdog
    | Burger

type Product =
    { Name: ProductName
      Ingredients: string list
      Additives: Additives list }

type Order =
    { Name: ProductName
      Additives: Additives list }

module Product =
    let createCoffee additives =
        { Name = Coffee
          Ingredients = [ "Water"; "Coffee" ]
          Additives = additives }

    let createHotdog additives =
        { Name = Hotdog
          Ingredients = [ "Bread"; "Sausage" ]
          Additives = additives }

    let createBurger additives =
        { Name = Burger
          Ingredients = [ "Bread"; "Patty" ]
          Additives = additives }

    let makeOrder (orders: Order list) = 
        for order in orders do
            match order.Name with
            | Coffee -> printfn "Here is your coffee: \n %A" <| createCoffee order.Additives
            | Hotdog -> printfn "Here is your hotdog: \n %A" <| createHotdog order.Additives
            | Burger -> printfn "Here is your burger: \n %A" <| createBurger order.Additives