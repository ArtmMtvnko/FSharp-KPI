module Program

open Kiosk.Modules
open Kiosk.Modules.Accounting
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

    let income1 = performCustomer1Replica "Hi! Can I get a burger? Also could you put some cheese and mushrooms?"
    let income2 = performCustomer2Replica "Gimme that coffee man! And I wanna see some milk and sugar there"

    // Classic type declaration
    let kioskAccount =
        { Transactions = [income1; income2]
          Expenses = 100.0 }

    let (total, profit, avg, min, max) = extractStatistics kioskAccount

    // Object expression
    let mutable txns = [income1; income2]
    let (total2, profit2, avg2, min2, max2) = 
        extractStatistics
            { new IAccountable with
                member this.Transactions = txns
                member this.AddTransaction amount =
                    txns <- amount :: txns
                member this.GetPureProfit () =
                    let profit = List.sum txns - 30.0
                    profit - (profit * 0.1)}

    // Class declaration
    let account = Account ([income1; income2])
    let (total3, profit3, avg3, min3, max3) = extractStatistics account

    let message: Printf.TextWriterFormat<_> = 
            "Total transactions: %i \n
             Profit: %f$ \n
             Average transaction amount: %f$ \n
             Mininum transaction amount: %f$ \n
             Maximum transaction amount: %f$ \n"

    printfn message total profit avg min max
    printfn message total2 profit2 avg2 min2 max2
    printfn message total3 profit3 avg3 min3 max3
    printfn "Profit Gross: %f \n Total Expenses: %f \n" account.ProfitGross account.Expenses

    0
