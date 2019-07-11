
//Apartment Hallway
//
=== apt_door
    It's my apartment...
    + [Go Home...?] 
        -> load_scene("Home")
    + [Not Yet...] ->END
->END

=== strange_man
    {
        - is_instrument_playing("Bass"):
            Fuck yeah bro that's wicked boss AND hella dope!
        - else: 
            Hey man you got a good bassline for me today?
    }
->END