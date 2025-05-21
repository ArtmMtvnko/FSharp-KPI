module DataLoader

open System.IO
open FSharp.Data
open System

type JsonData = JsonProvider<"""{ "exemple": "value" }""">.Root

let writeJSON (data: JsonData array) outpath =
    let jsonOutput = data |> Array.map (fun item -> item.JsonValue.ToString()) |> String.concat ",\n"  
    let jsonFormatted = "[" + jsonOutput + "]"  

    let outputPath = Path.Combine(__SOURCE_DIRECTORY__, outpath)  
    File.WriteAllText(outputPath, jsonFormatted) 

    printfn "Data is written to %s" outputPath


let rec displayMenu (files: string[]) =
    printfn "Select a JSON file from the list below:"

    files |> Array.iteri (fun i file -> printfn "%d. %s" (i + 1) file)

    printf "Enter the number of the file you want to select: "

    match Console.ReadLine() |> Int32.TryParse with
    | (true, choice) when choice > 0 && choice <= files.Length -> files.[choice - 1]
    | _ ->
        printfn "Invalid selection. Please try again."
        displayMenu files


let pickJsonFile directoryPath =  
   let jsonFiles = Directory.GetFiles(directoryPath, "*.json")

   if jsonFiles.Length = 0 then  
       Error (sprintf "No JSON files found in the directory: %s" directoryPath)  
   else  
       let selectedFile = displayMenu jsonFiles  
       printfn "You selected: %s" selectedFile  
       Ok <| Path.Combine(System.Environment.CurrentDirectory, selectedFile)

