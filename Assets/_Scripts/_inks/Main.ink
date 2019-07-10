//MAIN.INK
//
// This is where to store global VARs Methods and Scenes
// Include this in all inks to be safe

//EXTERNAL FUNCTIONS

//checks if player has item of id i in inventory
//-- check ItemDatabase for item id no.s
EXTERNAL check_item(i) 
EXTERNAL hero_name()
EXTERNAL is_instrument_playing(s)

//GLOBAL VARs
VAR item_to_add = 0
VAR scene_to_load = "null"
VAR sample_to_add = 0


//CALLOUT METHODS
// 
=== add_item(item_id)
    ~ item_to_add = item_id
    -> _pick_up_item

=== _pick_up_item
   #ADD_ITEM
-> DONE

=== load_scene(scene_name)
    ~ scene_to_load = scene_name
    -> _load_scene -> END 

=== _load_scene
    #LOAD_SCENE
-> DONE

=== add_sample(sample_id)
    ~ sample_to_add = sample_id
    -> _add_sample  

=== _add_sample
   #ADD_SAMPLE
-> DONE


=== default
    !!! THIS IS DEFAULT SCENE PLEASE SET SCENE TO READ IN TalkingObject.cs !!!
-> END


//DEBUGGING & TESTING
=== item 
    #debug 
    Pick up an item?
    + [yeah!]
        -> add_item(1)
    + [no!] -> END
->  END

=== sample
    #debug
    Add a sample?
    +[ yeah!]
        -> add_sample(1)
-> END

=== sample_2
    #debug
    Add a sample?
    +[ yeah!]
        -> add_sample(2)
-> END

=== test_sample 
    {
        - is_instrument_playing("Bass"):
            Bass is playing 
        - else: 
            Bass is not playing
    }
-> END