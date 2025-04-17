namespace CLI

open Kiosk.Types
open Kiosk.Modules.Additives

module CoffeeMaker =
    let basicCoffee =
        { Name = ProductName.Coffee
          Ingredients = [CoffeeIngredient Water; CoffeeIngredient CoffeeBeans]
          Additives = [] }

    let rec startCoffeeMakerMenu coffee =
        let options =
            [ (1, "All at once")
              (2, "Add sugar")
              (3, "Add milk")
              (4, "Add syrup")
              (0, "Done") ]

        printfn "\nPick which addivite you are going to add:"

        for (key, value) in options do
            printfn "\t%d. %s" key value

        printfn "Current coffee:\n%A\n" coffee

        let input = System.Console.ReadLine()
        let (valid, number) = System.Int32.TryParse input
        let option = if valid then number else -1
        
        match option with
        | 1 ->
            let omniCoffee: Product =
                { coffee with Additives = [ CoffeeAdditive Sugar; CoffeeAdditive Milk; CoffeeAdditive Syrup ] }
            printfn "You added everything in a cup of coffee"
            startCoffeeMakerMenu omniCoffee
        | 2 ->
            let coffeeWithSugar = addAdditive (CoffeeAdditive Sugar) coffee
            startCoffeeMakerMenu coffeeWithSugar
        | 3 ->
            let coffeeWithMilk = addAdditive (CoffeeAdditive Milk) coffee
            startCoffeeMakerMenu coffeeWithMilk
        | 4 ->
            let coffeeWithSyrup = addAdditive (CoffeeAdditive Syrup) coffee
            startCoffeeMakerMenu coffeeWithSyrup
        | 0 -> 
            coffee
        | _ -> 
            printfn "Invalid option"
            startCoffeeMakerMenu coffee
