namespace Kiosk.Modules

open Kiosk.Types
open Product

module Customer =
    let printGap () =
        printfn "\n\n\n\n"

    let printCustomer customer =
        match customer.Mood with
        | Good -> 
            printfn "<:)"
            printfn "/||\\"
            printfn " /\\"
        | Bad -> 
            printfn ">:("
            printfn "/||\\+--"
            printfn " /\\"

    let makeOrder customer line =
        printfn "%s" line

        let product, price = prepareOrder customer.Order

        let order = customer.Order

        if order.Name <> product.Name
        then
            printfn "It is not what I ordered!"
            printCustomer { customer with Mood = Bad }
            printGap ()
            0.0
        else
            if Set.ofList order.Additives <> Set.ofList product.Additives
            then
                printfn "It is not what I ordered!"
                printCustomer { customer with Mood = Bad }
                printGap ()
                0.0
            else
                printfn "Thank you for your order! \n"
                printCustomer { customer with Mood = Good }
                printGap ()
                price