namespace MetaheuristicsHW3
module TestFunction =
    let F1 (x : seq<double>) = Seq.sumBy (fun value -> value * value) x  

    let F2 (x : seq<double>) = 
        (Seq.sumBy (fun value -> abs(value) ) x) + 
        (Seq.fold (fun product value -> product * abs(value)) 1.0 x) 

    let F3 (x : seq<double>) =
        let xLength = Seq.length x 
        let mutable returnValue = 0.0
        for i in 0 .. xLength - 1 do
            let mutable tempValue = 0.0
            for j in 0 .. i do
                tempValue <- tempValue + (Seq.nth j x)
            returnValue <- returnValue + tempValue * tempValue
        returnValue

    let F4 (x: seq<double>) = abs( Seq.maxBy (fun value -> abs(value) ) x )

    let F5 (x: seq<double>) =
        let xLength = Seq.length x
        let mutable returnValue = 0.0
        for i in 0 .. xLength - 2 do
            let xi = (Seq.nth i x)
            let xi1 = (Seq.nth (i+1) x)
            let a = (xi1 - xi*xi)
            let b = (xi - 1.0)
            returnValue <- returnValue + 100.0*a*a + b*b
        returnValue 

    let F6 (x: seq<double>) = Seq.sumBy (fun value -> (floor(value + 0.5)) * (floor(value + 0.5))) x

    let F8 (x: seq<double>) = Seq.sumBy (fun value -> -value * sin(sqrt(abs(value)))) x

    let F9 (x: seq<double>) = Seq.sumBy (fun value -> value * value - 10.0*cos(2.0*acos(-1.0)*value) + 10.0) x

    let F10 (x: seq<double>) = 
        let xLength = Seq.length x
        -20.0 * exp(-0.2 * sqrt((F1 x) / (float)xLength)) -
            exp((Seq.sumBy ( fun value -> cos(2.0*acos(-1.0)*value) ) x) / (float)xLength) +
            20.0 + exp(1.0)

    let F11 (x: seq<double>) =
        let mutable tempValue = 1.0
        let xLength = Seq.length x
        
        for i in 0..xLength-1 do
            tempValue <- tempValue * cos((Seq.nth i x)/sqrt((float)(i+1)))

        (F1 x) / 4000.0 - tempValue + 1.0
