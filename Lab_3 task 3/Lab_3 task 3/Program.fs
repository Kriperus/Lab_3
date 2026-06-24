open System
open System.IO

let rec getDir prompt =
    printf "%s" prompt
    let p = Console.ReadLine().Trim()
    if Directory.Exists(p) then p
    else printfn "Ошибка: каталог не существует!"; getDir prompt

[<EntryPoint>]
let main _ =
    let dir = getDir "Введите путь к каталогу: "
    
    let filesArray = 
        try
            Directory.EnumerateFiles(dir) |> Seq.toArray
        with
        | :? UnauthorizedAccessException ->
            printfn "Ошибка: нет прав доступа к каталогу!"
            Array.empty
        | ex ->
            printfn "Ошибка при получении списка файлов: %s" ex.Message
            Array.empty
    
    // Создаем ленивую последовательность на основе массива
    let lazyFilesArray = 
        filesArray |> Array.map (fun f -> lazy f)
    
    // Функция для создания отложенной последовательности
    let files = 
        Seq.init lazyFilesArray.Length (fun i -> lazyFilesArray.[i].Value)
    
    let hasFiles = filesArray.Length > 0
    
    printfn "\nКаталог: %s" dir
    printfn "Файлы есть: %b" hasFiles
    
    if hasFiles then
        printfn "Количество: %d" filesArray.Length
        printfn "\nФайлы:"
        files |> Seq.iter (printfn "  %s")
    
    0
