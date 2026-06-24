open System

let rec readChar prompt =
    printf "%s" prompt
    let input = Console.ReadLine()
    
    match input.Length with
    | 0 ->
        printfn "Ошибка: введена пустая строка! Введите один символ."
        readChar prompt
    | 1 ->
        input.[0]
    | _ ->
        printfn "Ошибка: введите ровно один символ!"
        readChar prompt

let rec readPositiveInt prompt =
    printf "%s" prompt
    match Int32.TryParse(Console.ReadLine()) with
    | true, value when value > 0 -> value
    | true, _ ->
        printfn "Ошибка! Введите положительное число."
        readPositiveInt prompt
    | false, _ ->
        printfn "Ошибка! Введите целое число."
        readPositiveInt prompt

let buildStringFromSeq (chars: seq<char>) =
    chars
    |> Seq.fold (fun acc ch -> acc + string ch) ""

// Функция для создания отложенной последовательности символов
let lazyCharSequence count =
    let lazyValues = 
        [| for i in 0..count-1 -> 
            lazy (readChar (sprintf "Введите символ %d: " (i + 1))) |]
    
    // Функция для получения значения по индексу с вычислением только при необходимости
    let getValue index = lazyValues.[index].Value
    
    Seq.init count getValue

[<EntryPoint>]
let main _ =
    let count = readPositiveInt "Введите количество символов: "
    
    // Создание последовательности с отложенными вычислениями
    let charsSeq = lazyCharSequence count
    
    let resultString = buildStringFromSeq charsSeq
    
    // Вывод результатов
    printfn "\nИсходная последовательность символов:"
    charsSeq |> Seq.iter (printf "%c ")
    printfn "\n"
    
    printfn "Полученная строка: \"%s\"" resultString
    printfn "Длина строки: %d" resultString.Length
    
    0
