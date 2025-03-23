open Kiosk.Modules
open Kiosk.Types
open Customer
open CLI.MainMenu

[<EntryPoint>]
let main args =
    //let a: int list = [ 1; 2; 3 ]
    //let b: int array = [| 1; 2; 3 |]

    startMainMenu () |> ignore

    let customer1 =
        { Name = "John"
          Mood = Good
          Orders =
            [ { Name = Burger
                Additives = [ BurgerAdditive Cheese ; BurgerAdditive Mushrooms ] }
              { Name = Hotdog
                Additives = [ HotdogAdditive Ketchup; HotdogAdditive Mustard ] } ] }

    let customer2 =
        { Name = "Smith"
          Mood = Bad
          Orders = 
            [ { Name = Coffee
                Additives = [ CoffeeAdditive Milk; CoffeeAdditive Sugar ] } ] }

    makeOrder customer1 "Hi! Can I get a burger and a hotdog? \n"
    makeOrder customer2 "Gimme that goddamn coffee! And make it as fast as that dude sonic \n"

    0
