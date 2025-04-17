namespace Kiosk.Modules

open Kiosk.Types

module Additives =
    let isAdditiveAdded additive (product: Product) =
        List.exists (fun a -> a = additive) product.Additives

    let tryFindAdditive additive (product: Product) : Additives option =
        List.tryFind (fun a -> a = additive) product.Additives

    let addAdditive additive (product: Product) =
        match tryFindAdditive additive product with
        | Some _ ->
            printfn "You already added the additive"
            product
        | None ->
            printfn "You added %A" additive
            { product with Additives = additive :: product.Additives }

