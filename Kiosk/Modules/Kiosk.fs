namespace Kiosk.Modules

open Kiosk.Types

module Kiosk =
    let calculatePrice product =
        let mutable sum = 0.0

        for ingredient in product.Ingredients do
            sum <- sum + 
            match ingredient with
                | CoffeeIngredient coffeeIngredient -> 
                    match coffeeIngredient with
                    | CoffeeBeans -> 0.5
                    | Water -> 0.1
                | HotdogIngredient hotdogIngredient ->
                    match hotdogIngredient with
                    | Bread -> 0.2
                    | Sausage -> 1.0
                | BurgerIngredient burgerIngredient ->
                    match burgerIngredient with
                    | Buns -> 0.4
                    | Patty -> 1.3

        sum
