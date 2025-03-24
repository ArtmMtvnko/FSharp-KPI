namespace Kiosk.Modules

open Kiosk
open Kiosk.Types
open CLI
open CLI.MainMenu

module Product =
    let prepareOrder (order: Order) = 
        match order.Name with
        | Coffee -> 
            let coffee = makeProduct CoffeeMaker.basicCoffee
            let price = calculatePrice coffee
            printfn "Here is your order: \n%A \nIt's %A dollars \n" coffee price
            coffee, price
        | Hotdog -> 
            let hotdog = makeProduct HotdogMaker.basicHotdog
            let price = calculatePrice hotdog
            printfn "Here is your order: \n%A \nIt's %A dollars \n" hotdog price
            hotdog, price
        | Burger -> 
            let burger = makeProduct BurgerMaker.basicBurger
            let price = calculatePrice burger
            printfn "Here is your order: \n%A \nIt's %A dollars \n" burger price
            burger, price