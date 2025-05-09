namespace Kiosk.Modules  

module Accounting =  
    open System

    type IAccountable =  
        abstract member Transactions: float list  
        abstract member AddTransaction: float -> unit  
        abstract member GetPureProfit: unit -> float  

    type KioskAccount =  
        { mutable Transactions: float list
          Expenses: float }
        interface IAccountable with  
            member this.Transactions = this.Transactions  
            member this.AddTransaction amount =  
                this.Transactions <- amount :: this.Transactions  
            member this.GetPureProfit() =
                let profit = List.sum this.Transactions - this.Expenses
                let taxes = 0.2
                profit - (profit * taxes)

    let extractStatistics (account: IAccountable) =
        let numberOfTransactions = List.length account.Transactions
        let profit = account.GetPureProfit ()
        let average = List.average account.Transactions
        let min = List.min account.Transactions
        let max = List.max account.Transactions
       
        numberOfTransactions, profit, average, min, max

    type Account(initial: float list) =
        let mutable txns = initial
        let mutable expenses = 0.0
        let randomFactor = Random().NextDouble() * (0.7 - 0.2) + 0.2

        do initial |> List.iter (fun txn -> expenses <- expenses + txn * randomFactor)

        new () = Account([])

        new (initial: float seq) = Account(Seq.toList initial)

        member this.ProfitGross with get () = List.sum txns - expenses
        member this.Expenses with get () = expenses

        interface IAccountable with
            member this.Transactions = txns
            member this.AddTransaction amount =
                txns <- amount :: txns
                expenses <- expenses + (amount * randomFactor)
                ()
            member this.GetPureProfit() =
                let profit = List.sum txns - expenses
                let taxes = 0.2
                profit * (1.0 - taxes)
