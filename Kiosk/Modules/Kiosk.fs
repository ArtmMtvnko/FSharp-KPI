namespace Kiosk.Modules

open Kiosk.Types

module Kiosk =
    let includeRent price =
        price + price * 0.15

    let includeTax price =
        price + price * 0.2

    let includeTips price =
        price + price * 0.1

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

        for additive in product.Additives do
            sum <- sum +
            match additive with
                | CoffeeAdditive coffeeAdditive ->
                    match coffeeAdditive with
                    | Milk -> 0.2
                    | Sugar -> 0.1
                    | Syrup -> 0.3
                | HotdogAdditive hotdogAdditive ->
                    match hotdogAdditive with
                    | Ketchup -> 0.1
                    | Mustard -> 0.1
                    | Mayonnaise -> 0.2
                | BurgerAdditive burgerAdditive ->
                    match burgerAdditive with
                    | Cheese -> 0.3
                    | Lettuce -> 0.1
                    | Mushrooms -> 0.2
                    | Onions -> 0.1

        sum |> includeRent |> includeTax |> includeTips
