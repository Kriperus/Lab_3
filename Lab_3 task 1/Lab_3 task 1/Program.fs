open System

let rec readFloat prompt =
    printf "%s" prompt
    match Double.TryParse(Console.ReadLine()) with
    | true, value -> value
    | false, _ ->
        printfn "Ошибка! Введите корректное вещественное число."
        readFloat prompt

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

let lastDigit (x: float) =
    x.ToString()
    |> Seq.filter Char.IsDigit
    |> Seq.last
    |> string
    |> Int32.Parse

// Функция для создания отложенной последовательности
let lazySequence count =
    let lazyValues = 
        [| for i in 0..count-1 -> 
            lazy (readFloat (sprintf "Число %d: " (i + 1))) |]
    
    // Функция для получения значения по индексу с вычислением только при необходимости
    let getValue index = lazyValues.[index].Value
    
    Seq.init count getValue

[<EntryPoint>]
let main _ =
    let count = readPositiveInt "Введите количество чисел: "

    // Создание последовательности с отложенными вычислениями
    let numbersSeq = lazySequence count

    // Получение последовательности последних цифр с помощью Seq.map.
    let digitsSeq = numbersSeq |> Seq.map lastDigit

    printfn "\nРезультат:"
    Seq.zip numbersSeq digitsSeq
    |> Seq.iter (fun (num, digit) -> printfn "%g -> %d" num digit)

    printfn "\nИсходные числа:"
    numbersSeq |> Seq.iter (printf "%g ")
    printfn "\n"

    printfn "Последние цифры:"
    digitsSeq |> Seq.iter (printf "%d ")
    printfn "\n"

    0
