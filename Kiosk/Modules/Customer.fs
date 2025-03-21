namespace Kiosk.Modules

open Kiosk.Types
open Product

module Customer =
    let coffeeWithAdditives =
        { Name = Coffee
          Additives = [ CoffeeAdditive Milk; CoffeeAdditive Sugar ] }

    let hotdogWithAdditives =
        { Name = Hotdog
          Additives = [ HotdogAdditive Ketchup; HotdogAdditive Mustard ] }

    let makeOrder customer line =
        match customer.Mood with
        | Good -> 
            printfn "<:)"
            printfn "/||\\"
            printfn " /\\"
        | Bad -> 
            printfn ">:("
            printfn "/||\\+--"
            printfn " /\\"

        printfn line
        prepareOrder customer.Orders