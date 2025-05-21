module Program

open FSharp.Data
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
        let wholeDataset = DataScienceSalary.Load(filePath)
        let dataset = wholeDataset |> selectWithStep 1000

        let salaryProcessedData = getSeleryProcessedData dataset
        let expLevelCountData = getExpLevelCount dataset
        let avgJobSalaryPerYearData = getAvgJobSalaryPerYear dataset [
            "Software Engineer"; "Data Scientist";
            "Data Analyst"; "Machine Learning Engineer"
        ]

        let salaryBarChart = salaryProcessedData |> getBarChart "Salary Bar Chart"
        let expLevelBarChart = expLevelCountData |> getBarChart "Experiense Level Bar Chart"
        let avgJobSalaryBarChart = avgJobSalaryPerYearData |> getGroupedBarChart

        [ salaryBarChart
          expLevelBarChart
          avgJobSalaryBarChart ]
        |> drawAllBarCharts

        0

