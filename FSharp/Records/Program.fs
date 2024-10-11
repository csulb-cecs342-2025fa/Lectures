open System

// Declaring new types in F#, starting simple.

// The "type" keyword declares a new type. The simplest new type
// is just an alias for another type.

// We can declare new types for with Records. A record is just like a struct, where each 
// field is labeled with a name.
type GeoCoord = {lat: float; long: float}

// We don't need type annotations with records.
let csulb = {lat = 33.783072; long = -118.110376}

// Like the way tuples can be deconstructed, so too can records.
let { lat = vecLat; long = vecLong} = csulb
// We can now use vecLat and vecLong individually.

// Instead of deconstructing, we can also use "dot" notation
let vecLat2 = csulb.lat


// We can write functions on records, naturally.
let hemisphereNS geo =
    if geo.lat > 0.0 then
        "Northern"
    elif geo.lat < 0.0 then
        "Southern"
    else
        "Equatorial"


// There's a ton more that we could do here, but this suffices for now.
// Next: unions!