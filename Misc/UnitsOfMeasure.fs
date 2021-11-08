module ``Units of measure``
// Units of measure are checked and removed at compile time
// such that they do not incur a runtime penalty

[<Measure>] type cm

[<Measure>] type m

type cm with
    static member asMeter = 0.01<m/cm>

[<Measure>] type l

[<Measure>] type CHF

let convertToLitres (m3 : float<m^3>) = m3 * 1000.0<l/m^3>

let width = 12.0<m>
let thickness = 30.0<cm> * cm.asMeter
let height = 2.0<m>

let pricePerLitre = 42.00<CHF/l>

let budget : float<CHF> = (width * height * thickness |> convertToLitres) * pricePerLitre
