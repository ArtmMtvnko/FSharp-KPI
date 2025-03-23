namespace CLI

open CoffeeMaker

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
        | _ -> 
            printfn "Invalid option"
            startMainMenu ()