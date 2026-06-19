open System

// Проверка ввода вещественного числа.
let rec readFloat prompt =
    printf "%s" prompt
    match Double.TryParse(Console.ReadLine()) with
    | true, value -> value
    | false, _ ->
        printfn "Ошибка! Введите корректное вещественное число."
        readFloat prompt

// Проверка ввода целого положительного числа.
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

// Получение последней цифры вещественного числа.
let lastDigit (x: float) =
    x.ToString()
    |> Seq.filter Char.IsDigit
    |> Seq.last
    |> string
    |> Int32.Parse

[<EntryPoint>]
let main _ =
    printfn "=== Получение последних цифр вещественных чисел ===\n"

    // Ввод количества чисел.
    let count = readPositiveInt "Введите количество чисел: "

    // Создание последовательности чисел с помощью Seq.init
    // и кеширование для предотвращения повторного ввода.
    let numbersSeq =
        Seq.init count (fun i -> readFloat (sprintf "Число %d: " (i + 1)))
        |> Seq.cache

    // Получение последовательности последних цифр с помощью Seq.map.
    let digitsSeq = numbersSeq |> Seq.map lastDigit

    // Вывод результатов.
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