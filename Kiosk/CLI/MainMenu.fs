namespace CLI

open CoffeeMaker
open HotdogMaker
open BurgerMaker
open Kiosk.Types

module MainMenu =
    let rec makeProduct (product: Product) =
        let options =
            [ (1, "Coffee")
              (2, "Hotdog")
              (3, "Burger")
              (0, "Done") ]
        
        printfn "\nPick which one you are going to make:"

        for (key, value) in options do
            printfn "\t%d. %s" key value

        let input = System.Console.ReadLine()
        let (valid, number) = System.Int32.TryParse input
        let option = if valid then number else -1

        match option with
        | 1 ->
            printfn "You picked Coffee"
            let coffee = startCoffeeMakerMenu CoffeeMaker.basicCoffee
            makeProduct coffee
        | 2 ->
            printfn "You picked Coffee"
            let hotdog = startHotdogMakerMenu HotdogMaker.basicHotdog
            makeProduct hotdog
        | 3 ->
            printfn "You picked Burger"
            let burger = startBurgerMakerMenu BurgerMaker.basicBurger
            makeProduct burger
        | 0 ->
            product
        | _ ->
            printfn "Invalid option"
            makeProduct product
