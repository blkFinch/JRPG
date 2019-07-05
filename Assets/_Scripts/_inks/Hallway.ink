INCLUDE Main.ink

//Apartment Hallway
//
=== apt_door
    It's my apartment...
    + [Go Home...?] 
        ~ scene_to_load = "Home"
        -> load_scene ->END
    + [Not Yet...] ->END
->END

