namespace CLI

open CoffeeMaker
open HotdogMaker
open BurgerMaker

module MainMenu =
    let rec startMainMenu () =
        let options =
            [ (1, "Coffee")
              (2, "Hotdog")
              (3, "Burger") ]
        
        printfn "Pick which one you are going to make:"

        for (key, value) in options do
            printfn "\t%d. %s" key value

        let input = System.Console.ReadLine() |> int

        match input with
        | 1 ->
            printfn "You picked Coffee"
            startCoffeeMakerMenu CoffeeMaker.basicCoffee
        | 2 ->
            printfn "You picked Coffee"
            startHotdogMakerMenu HotdogMaker.basicHotdog
        | 3 ->
            printfn "You picked Burger"
            startBurgerMakerMenu BurgerMaker.basicBurger
        | _ ->
            printfn "Invalid option"
            startMainMenu ()