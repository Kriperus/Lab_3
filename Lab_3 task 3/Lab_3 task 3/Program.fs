open System
open System.IO

let rec getDir prompt =
    printf "%s" prompt
    let p = Console.ReadLine().Trim()
    if Directory.Exists(p) then p
    else 
        printfn "Ошибка: каталог не существует!"
        getDir prompt

[<EntryPoint>]
let main _ =
    let dir = getDir "Введите путь к каталогу: "
    
    try
        let files = Directory.EnumerateFiles(dir)
        
        let hasFiles = 
            files 
            |> Seq.length > 0
        
        printfn "\nКаталог: %s" dir
        printfn "Файлы есть: %b" hasFiles
        
        if hasFiles then
            printfn "Количество: %d" (files |> Seq.length)
            printfn "\nФайлы:"
            files |> Seq.iter (printfn "  %s")
    with
    | :? UnauthorizedAccessException ->
        printfn "Ошибка: нет прав доступа к каталогу!"
    | :? DirectoryNotFoundException ->
        printfn "Ошибка: каталог не найден!"
    | :? PathTooLongException ->
        printfn "Ошибка: путь слишком длинный!"
    | ex ->
        printfn "Ошибка при получении списка файлов: %s" ex.Message
    
    0
