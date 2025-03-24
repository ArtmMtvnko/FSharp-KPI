namespace CLI

open Kiosk.Types

module BurgerMaker =
    let basicBurger =
        { Name = ProductName.Burger
          Ingredients = [BurgerIngredient Buns; BurgerIngredient Patty]
          Additives = [] }

    let isAdditiveAdded additive (burger: Product) =
        List.exists (fun a -> a = additive) burger.Additives

    let addAdditive additive (burger: Product) =
        if isAdditiveAdded additive burger
        then 
            printfn "You already added the additive"
            burger
        else
            printfn "You added %A" additive
            { burger with Additives = additive :: burger.Additives }

    let rec startBurgerMakerMenu burger =
        let options =
            [ (1, "All at once")
              (2, "Add cheese")
              (3, "Add lettuce")
              (4, "Add mushrooms")
              (5, "Add onions")
              (0, "Done") ]

        printfn "\nPick which addivite you are going to add:"

        for (key, value) in options do
            printfn "\t%d. %s" key value

        printfn "Current burger:\n%A\n" burger

        let input = System.Console.ReadLine()
        let (valid, number) = System.Int32.TryParse input
        let option = if valid then number else -1
        
        match option with
        | 1 ->
            let omniBurger: Product =
                { burger with Additives = [ BurgerAdditive Cheese; BurgerAdditive Lettuce; BurgerAdditive Mushrooms; BurgerAdditive Onions ] }
            printfn "You added everything to the burger"
            startBurgerMakerMenu omniBurger
        | 2 ->
            let burgerWithCheese = addAdditive (BurgerAdditive Cheese) burger
            startBurgerMakerMenu burgerWithCheese
        | 3 ->
            let burgerWithLettuce = addAdditive (BurgerAdditive Lettuce) burger
            startBurgerMakerMenu burgerWithLettuce
        | 4 ->
            let burgerWithMushrooms = addAdditive (BurgerAdditive Mushrooms) burger
            startBurgerMakerMenu burgerWithMushrooms
        | 5 ->
            let burgerWithOnions = addAdditive (BurgerAdditive Onions) burger
            startBurgerMakerMenu burgerWithOnions
        | 0 ->
            burger
        | _ -> 
            printfn "Invalid option"
            startBurgerMakerMenu burger
