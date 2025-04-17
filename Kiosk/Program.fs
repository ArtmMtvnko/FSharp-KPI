open Kiosk.Modules
open Kiosk.Types
open Customer

[<EntryPoint>]
let main args =
    let customer1 =
        { Id = 123321
          Name = "John"
          Mood = Good
          Order =
            { Name = Burger
              Additives = [ BurgerAdditive Cheese; BurgerAdditive Mushrooms ] } }

    let customer2 =
        { Id = "asdf123"
          Name = "Smith"
          Mood = Bad
          Order =
            { Name = Coffee
              Additives = [ CoffeeAdditive Milk; CoffeeAdditive Sugar ] } }

    let performCustomer1Replica = makeOrder customer1
    let performCustomer2Replica = makeOrder customer2

    performCustomer1Replica "Hi! Can I get a burger? Also could you put some cheese and mushrooms?" |> ignore
    performCustomer2Replica "Gimme that coffee man! And I wanna see some milk and sugar there" |> ignore

    0
