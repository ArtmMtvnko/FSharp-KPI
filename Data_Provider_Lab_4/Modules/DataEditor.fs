module DataEditor

open XPlot.Plotly
open Types

let selectWithStep step (array: 'a array) =
    [| for i in 0 .. step .. array.Length - 1 -> array[i] |]


let getBarChart title (data: ('a * 'b) array) =
    let layout = Layout(title = title)

    data
    |> Chart.Bar
    |> Chart.WithLayout layout


let getGroupedBarChart (data: (string * (int * int) array) array) =
    data
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
    |> Chart.Plot
    |> Chart.WithLayout (Layout(barmode = "group"))


let drawAllBarCharts charts =
    charts |> Chart.ShowAll


let getSeleryProcessedData (dataset: DataScienceSalary.Root array) =
    dataset
    |> Array.map (fun x -> (x.ExperienceLevel, x.SalaryInUsd))
    |> Array.groupBy (fun (lvl, _) -> lvl)
    |> Array.map (fun (_, tupleArr) -> tupleArr)
    |> Array.map (fun x ->
        x |> Array.fold (fun (lvl, acc) (_, salary) ->
            (lvl, acc + salary / x.Length)
        ) (fst x[0], 0)
    )


let getExpLevelCount (dataset: DataScienceSalary.Root array) =
    dataset
    |> Array.map (fun x -> x.ExperienceLevel)
    |> Array.groupBy (fun lvl -> lvl)
    |> Array.map (fun (lvl, array) -> (lvl, array.Length))


let getAvgJobSalaryPerYear desiredJobs (dataset: DataScienceSalary.Root array) =
    let jobsToCheck = Set.ofSeq desiredJobs

    dataset
    |> Array.map (fun x -> (x.JobTitle, x.WorkYear, x.SalaryInUsd))
    |> Array.groupBy (fun (job, _, _) -> job)
    |> Array.filter (fun (job, _) -> Set.contains job jobsToCheck)
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
