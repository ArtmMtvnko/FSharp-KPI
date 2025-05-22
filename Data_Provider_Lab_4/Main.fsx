#r "nuget: FSharp.Data"
#r "nuget: XPlot.Plotly"

open FSharp.Data  

printfn "Hello from F#"
printfn "Source directory: %A" __SOURCE_DIRECTORY__  
System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__  

type DataScienceSalary = JsonProvider<"Data\output.json">  
let wholeData = DataScienceSalary.Load("Data\DataScience_salaries_2025.json")
printfn "Length: %A" wholeData.Length

let selectWithStep step (array: 'T array) =
    [| for i in 0 .. step .. array.Length - 1 -> array[i] |]

let dataSet = wholeData |> selectWithStep 1000
//printfn "%A" dataSet

let salaryData = 
    dataSet
    |> Array.map (fun x -> (x.ExperienceLevel, x.SalaryInUsd))
    |> Array.groupBy (fun (lvl, _) -> lvl)
    |> Array.map (fun (_, tupleArr) -> tupleArr)
    |> Array.map (fun x ->
        x |> Array.fold (fun (lvl, acc) (_, salary) ->
            (lvl, acc + salary / x.Length)
        ) (fst x[0], 0)
    )

printfn "Salary Data: %A" salaryData

let expLevelCount =
    dataSet
    |> Array.map (fun x -> x.ExperienceLevel)
    |> Array.groupBy (fun lvl -> lvl)
    |> Array.map (fun (lvl, array) -> (lvl, array.Length))

printfn "Experience Level Data: %A" expLevelCount

let jobsToSee =
    Set.ofList [
        "Software Engineer"; "Data Scientist"; "Data Analyst"; "Machine Learning Engineer"
    ]

let avgSalaryPerYear =
    dataSet
    |> Array.map (fun x -> (x.JobTitle, x.WorkYear, x.SalaryInUsd))
    |> Array.groupBy (fun (job, _, _) -> job)
    |> Array.filter (fun (job, _) -> Set.contains job jobsToSee)
    |> Array.map (fun (job, array) ->
        let res =
            array
            |> Array.groupBy (fun (_, year, _) -> year)
            |> Array.map (fun (year, arrayTuple) ->
                let avgSalary =
                    arrayTuple
                    |> Array.map (fun (_, _, salary) -> float salary)
                    |> Array.average
                (year, int avgSalary)
            )
        (job, res)
    )

printfn "Avg salary: %A" avgSalaryPerYear


open XPlot.Plotly

let salaryBarChart =  
   salaryData  
   |> Chart.Bar  
   |> Chart.WithLayout (Layout(title = "Salary by Experience Level"))

let expLevelBarChart =
    expLevelCount
    |> Chart.Bar
    |> Chart.WithLayout (Layout(title = "Engineer count by Experience Level"))


let bars =
    avgSalaryPerYear
    |> Array.map (fun (job, array) ->
        let res = 
            array
            |> Array.fold (fun (years, salaries) (year, salary) ->
                (year :: years, salary :: salaries)
            ) ([], [])

        (job, fst res, snd res)
    )
    |> Array.map (fun (job, years, salaries) -> Bar(
        x = years,
        y = salaries,
        name = job
    ))

let groupedLayout = Layout(barmode = "group")

let avgSalaryPerYearBarChart =
    bars
    |> Chart.Plot
    |> Chart.WithLayout groupedLayout

[ salaryBarChart
  expLevelBarChart
  avgSalaryPerYearBarChart ]
|> Chart.ShowAll
