open System
open System.IO

let rec getDir prompt =
    printf "%s" prompt
    let p = Console.ReadLine().Trim()
    if Directory.Exists(p) then p
    else printfn "Ошибка!"; getDir prompt

[<EntryPoint>]
let main _ =
    let dir = getDir "Введите путь к каталогу: "
    
    let files = 
        Directory.EnumerateFiles(dir)
        |> Seq.cache
    
    let hasFiles = files |> Seq.length > 0
    
    printfn "\nКаталог: %s" dir
    printfn "Файлы есть: %b" hasFiles
    
    if hasFiles then
        printfn "Количество: %d" (files |> Seq.length)
        printfn "\nФайлы:"
        files |> Seq.iter (printfn "  %s")
    
    0