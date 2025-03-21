namespace Kiosk.Modules

open Kiosk.Types
open Kiosk

module Product =
    let createCoffee additives =
        { Name = Coffee
          Ingredients = [ CoffeeIngredient Water; CoffeeIngredient CoffeeBeans ]
          Additives = additives }

    let createHotdog additives =
        { Name = Hotdog
          Ingredients = [ HotdogIngredient Bread; HotdogIngredient Sausage ]
          Additives = additives }

    let createBurger additives =
        { Name = Burger
          Ingredients = [ BurgerIngredient Buns; BurgerIngredient Patty ]
          Additives = additives }

    let prepareOrder (orders: Order list) = 
        for order in orders do
            match order.Name with
            | Coffee -> 
                let coffee = createCoffee order.Additives
                let price = calculatePrice coffee
                printfn "Here is your coffee: \n%A \nIt's %A dollars \n" coffee price
            | Hotdog -> 
                let hotdog = createHotdog order.Additives
                let price = calculatePrice hotdog
                printfn "Here is your hotdog: \n%A \nIt's %A dollars \n" hotdog price
            | Burger -> 
                let burger = createBurger order.Additives
                let price = calculatePrice burger
                printfn "Here is your burger: \n%A \nIt's %A dollars \n" burger price