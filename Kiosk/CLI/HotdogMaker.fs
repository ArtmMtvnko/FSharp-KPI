namespace CLI

open Kiosk.Types
open Kiosk.Modules.Additives
open Kiosk.Modules.Utils

module HotdogMaker =
    let basicHotdog =
        { Name = ProductName.Hotdog
          Ingredients = [HotdogIngredient Bread; HotdogIngredient Sausage]
          Additives = [] }

    let rec startHotdogMakerMenu (hotdog: Product) =
        let options =
            [ (1, "All at once")
              (2, "Add ketchup")
              (3, "Add mustard")
              (0, "Done") ]

        printfn "\nPick which addivite you are going to add:"

        for (key, value) in options do
            printfn "\t%d. %s" key value

        printfn "Added addivites:"
        printList hotdog.Additives

        printf "\nEnter the number: "
        let input = System.Console.ReadLine()
        let (valid, number) = System.Int32.TryParse input
        let option = if valid then number else -1
        
        match option with
        | 1 ->
            let omniHotdog: Product =
                { hotdog with Additives = [ HotdogAdditive Ketchup; HotdogAdditive Mustard ] }
            printfn "You added everything to the hotdog"
            startHotdogMakerMenu omniHotdog
        | 2 ->
            let hotdogWithKetchup = addAdditive (HotdogAdditive Ketchup) hotdog
            startHotdogMakerMenu hotdogWithKetchup
        | 3 ->
            let hotdogWithMustard = addAdditive (HotdogAdditive Mustard) hotdog
            startHotdogMakerMenu hotdogWithMustard
        | 0 -> 
            hotdog
        | _ -> 
            printfn "Invalid option"
            startHotdogMakerMenu hotdog
