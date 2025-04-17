namespace Kiosk.Modules

module Utils =
    let printList (list: 'a list) =
        for item in list do
            printfn "- %A" item
