namespace CLI

open Kiosk.Types

module CoffeeMaker =
    let basicCoffee: Product =
        { Name = ProductName.Coffee
          Ingredients = []
          Additives = [] }

    let isAdditiveAdded additive (coffee: Product) =
        List.exists (fun a -> a = additive) coffee.Additives


    let addSugar product: Product =
        { product with Additives = CoffeeAdditive Sugar :: product.Additives }

    let addMilk product: Product =
        { product with Additives = CoffeeAdditive Milk :: product.Additives }

    let addSyrup product: Product =
        { product with Additives = CoffeeAdditive Syrup :: product.Additives }

    let rec startCoffeeMakerMenu coffee =
        let options =
            [ (1, "All at once")
              (2, "Add sugar")
              (3, "Add milk")
              (4, "Add syrup")
              (0, "Back") ]

        for (key, value) in options do
            printfn "\t%d. %s" key value

        printfn "Current coffee: %A" coffee

        let input = System.Console.ReadLine()
        let option = if input = "" then -1 else input |> int
        
        match option with
        | 1 ->
            let omniCoffee = coffee |> addSugar |> addMilk |> addSyrup
            printfn "You added everything in a cup of coffee"
            startCoffeeMakerMenu omniCoffee
        | 2 ->
            let coffeeWithSugar =
                if isAdditiveAdded (CoffeeAdditive Sugar) coffee
                then
                    printfn "You added sugar to the coffee"
                    coffee
                else addSugar coffee

            startCoffeeMakerMenu coffeeWithSugar
        | 3 ->
            let coffeeWithMilk = addMilk coffee
            printfn "You added milk to the coffee"
            startCoffeeMakerMenu coffeeWithMilk
        | 4 ->
            let coffeeWithSyrup = addSyrup coffee
            printfn "You added syrup to the coffee"
            startCoffeeMakerMenu coffeeWithSyrup
        | 0 -> 
            coffee
        | _ -> 
            printfn "Invalid option"
            startCoffeeMakerMenu coffee
