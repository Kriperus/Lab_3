open System

// Проверка ввода символа
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

// Проверка ввода целого положительного числа
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

// Формирование строки из последовательности символов с помощью Seq.fold
let buildStringFromSeq (chars: seq<char>) =
    chars
    |> Seq.fold (fun acc ch -> acc + string ch) ""

[<EntryPoint>]
let main _ =
    // Ввод количества символов
    let count = readPositiveInt "Введите количество символов: "
    
    // Создание последовательности символов с помощью Seq.init
    // и кеширование для предотвращения повторного ввода
    let charsSeq =
        Seq.init count (fun i -> readChar (sprintf "Введите символ %d: " (i + 1)))
        |> Seq.cache
    
    // Формирование строки из последовательности
    let resultString = buildStringFromSeq charsSeq
    
    // Вывод результатов
    printfn "\nИсходная последовательность символов:"
    charsSeq |> Seq.iter (printf "%c ")
    printfn "\n"
    
    printfn "Полученная строка: \"%s\"" resultString
    printfn "Длина строки: %d" resultString.Length
    
    0