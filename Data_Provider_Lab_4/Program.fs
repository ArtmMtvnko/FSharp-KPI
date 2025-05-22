module Program

open DataLoader
open DataEditor
open Types

System.Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

[<EntryPoint>]
let main args =
    let json = pickJsonFile "Data"

    match json with
    | Error msg ->
        printfn "%s" msg
        1
    | Ok filePath ->
        let asyncGetDataset = async {
            let! wholeDataset = DataScienceSalary.AsyncLoad(filePath)
            return wholeDataset |> selectWithStep 1000
        }

        let dataset = asyncGetDataset |> Async.RunSynchronously

        let dataProcessFunctions = [
            getSeleryProcessedData
            getExpLevelCount
        ]

        let avgSalary = getAvgJobSalaryPerYear [ "Software Engineer"; "Data Scientist"; "Data Analyst"; "Machine Learning Engineer" ]

        let results = 
            dataProcessFunctions
            |> List.map (fun fn -> async { return fn dataset })
            |> Async.Parallel
            |> Async.RunSynchronously

        let simpleBarCharts = 
            results
            |> Array.map (getBarChart "Simple Bar Chart")
            |> Array.toList

        let barCharts =
            (dataset |> avgSalary |> getGroupedBarChart) :: simpleBarCharts

        barCharts |> drawAllBarCharts

        0

