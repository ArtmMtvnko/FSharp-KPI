module FsTests

open FsUnit
open NUnit.Framework
open Kiosk.Types
open Kiosk.Modules.Kiosk
open Kiosk.Modules.Additives
open Kiosk.Modules.Accounting 

// 1  
[<TestCase(15.0)>]
[<TestCase(-20.0)>]
[<TestCase(0.0)>]
let ``Is rent included properly?`` price =  
   let expected = price * (1.0 + 0.15)  
   includeRent price |> should equal expected  

[<Test>]  
let ``Does includeRent throws exception`` () =  
   (fun () -> includeRent -1.0 |> ignore) |> should throw typeof<System.ArgumentException>  

// 2  
[<TestCase(15.0)>]  
[<TestCase(-20.0)>]  
[<TestCase(0.0)>]  
let ``Is tax included properly?`` price =  
   let expected = price * (1.0 + 0.2)  
   includeTax price |> should equal expected  

[<Test>]  
let ``Does includeTax throws exception`` () =  
   (fun () -> includeTax -1.0 |> ignore) |> should throw typeof<System.ArgumentException>  

// 3  
[<TestCase(15.0)>]  
[<TestCase(-20.0)>]  
[<TestCase(0.0)>]  
let ``Are tips included properly?`` price =  
   let expected = price * (1.0 + 0.1)  
   includeTips price |> should equal expected  

[<Test>]  
let ``Does includeTips throws exception`` () =  
   (fun () -> includeTips -1.0 |> ignore) |> should throw typeof<System.ArgumentException>  

// 4  
[<TestCaseSource("priceParams")>]  
let ``Calculate price for a product with ingredients and additives`` (product: Product) (expected: float) =  
   calculatePrice product |> should equal expected  

let priceParams () = seq {  
   yield [|  
       box {  
           Name = Coffee  
           Ingredients = [  
               CoffeeIngredient CoffeeBeans  
               CoffeeIngredient Water  
           ]  
           Additives = [  
               CoffeeAdditive Milk  
           ]  
       };  
       box ([0.5; 0.1; 0.2]  
       |> List.sum  
       |> includeRent  
       |> includeTax  
       |> includeTips)  
   |]  

   yield [|   
       box {  
           Name = Burger  
           Ingredients = [  
               BurgerIngredient Buns  
               BurgerIngredient Patty  
           ]  
           Additives = [  
               BurgerAdditive Cheese  
               BurgerAdditive Lettuce  
           ]  
       };  
       box ([0.4; 1.3; 0.3; 0.1]  
       |> List.sum  
       |> includeRent  
       |> includeTax  
       |> includeTips)  
   |]  
}  

// 5  
[<Test>]  
let ``Is additive added to product?`` () =  
   let product = {  
       Name = Coffee  
       Ingredients = [CoffeeIngredient CoffeeBeans; CoffeeIngredient Water]  
       Additives = [CoffeeAdditive Milk]  
   }  

   let additive = CoffeeAdditive Sugar  
   let updatedProduct = product |> addAdditive additive  

   updatedProduct.Additives |> should contain additive  

[<Test>]  
let ``What if I added additive multiple times?`` () =  
   let product = {  
       Name = Coffee  
       Ingredients = [CoffeeIngredient CoffeeBeans; CoffeeIngredient Water]  
       Additives = []  
   }  

   let updatedProduct =  
       product  
       |> addAdditive (CoffeeAdditive Sugar)  
       |> addAdditive (CoffeeAdditive Sugar)  

   updatedProduct.Additives.Length |> should equal 1  

// 6  
[<Test>]  
let ``Can I find an additive in product?`` () =  
   let product = {  
       Name = Coffee  
       Ingredients = [CoffeeIngredient CoffeeBeans; CoffeeIngredient Water]  
       Additives = [CoffeeAdditive Milk; CoffeeAdditive Sugar]  
   }  

   let additive = CoffeeAdditive Sugar  
   tryFindAdditive additive product |> should equal (Some additive)  

[<Test>]  
let ``Can I find an unexisted additive in product?`` () =  
   let product = {  
       Name = Coffee  
       Ingredients = [CoffeeIngredient CoffeeBeans; CoffeeIngredient Water]  
       Additives = [CoffeeAdditive Milk]  
   }  

   let additive = CoffeeAdditive Sugar  
   tryFindAdditive additive product |> should equal None  

// 7  
[<Test>]  
let ``Was additive added?`` () =  
   let product = {  
       Name = Coffee  
       Ingredients = [CoffeeIngredient CoffeeBeans; CoffeeIngredient Water]  
       Additives = [CoffeeAdditive Milk]  
   }  
   let additive = CoffeeAdditive Milk  
   isAdditiveAdded additive product |> should be True  

[<Test>]  
let ``Was not additive added?`` () =  
   let product = {  
       Name = Coffee  
       Ingredients = [CoffeeIngredient CoffeeBeans; CoffeeIngredient Water]  
       Additives = []  
   }  
   let additive = CoffeeAdditive Milk  
   isAdditiveAdded additive product |> should be False  

// 8  
[<Test>]  
let ``Adding transaction to the account`` () =  
   let account = Account([10.0; 20.0; 30.0]) :> IAccountable  
   account.AddTransaction 40.0  
   account.Transactions |> should contain 40.0  

[<Test>]  
let ``Adding multiple transactions to the account`` () =  
   let account = Account([10.0; 20.0; 30.0]) :> IAccountable  
   account.AddTransaction 40.0  
   account.AddTransaction 40.0  
   account.AddTransaction 40.0  
   account.AddTransaction 40.0  
   account.Transactions.Length |> should equal 7  

// 9  
[<Test>]  
let ``Does pure profit calculation work?`` () =  
   let account = Account([10.0; 20.0; 30.0])  
   let actualProfit = (account :> IAccountable).GetPureProfit()  
   let expectedProfit = ((account :> IAccountable).Transactions |> List.sum) - account.Expenses  
   actualProfit |> should equal (expectedProfit * 0.8)  

[<Test>]  
let ``Is pure profit calculation idempotent?`` () =  
   let account = Account([10.0; 20.0; 30.0])  
   let list = System.Collections.Generic.List<float * float>()  

   for _ = 1 to 100 do  
       let actualProfit = (account :> IAccountable).GetPureProfit()  
       let expectedProfit = (((account :> IAccountable).Transactions |> List.sum) - account.Expenses) * 0.8  
       list.Add((expectedProfit, actualProfit))  

   let differ = list.Exists(fun (expected, actual) -> expected <> actual)  

   differ |> should be False  

// 10  
[<Test>]  
let ``Gathering account statistics. Does total count is the same?`` () =  
   let account = Account([10.0; 20.0; 30.0]) :> IAccountable  
   let (total, _, _, _, _) = extractStatistics account  
   total |> should equal 3  

[<Test>]  
let ``Gathering account statistics. Does profit calculation work?`` () =  
   let account = Account([100.0; 20.0; 30.0]) :> IAccountable  
   let (_, _, _, min, max) = extractStatistics account  

   max |> should equal 100.0  
   min |> should equal 20.0
