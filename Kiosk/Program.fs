open Kiosk.Product
open Kiosk

[<EntryPoint>]
let main args =
    //let a: int list = [ 1; 2; 3 ]
    //let b: int array = [| 1; 2; 3 |]

    let order =
        [ { Name = Coffee
            Additives = [ CoffeeAdditive Milk; CoffeeAdditive Sugar ] }
          { Name = Hotdog
            Additives = [ HotdogAdditive Ketchup; HotdogAdditive Mustard ] } ]

    makeOrder order

    0
