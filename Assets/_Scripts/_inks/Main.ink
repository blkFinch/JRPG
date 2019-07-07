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
//
VAR item_to_add = 0
VAR scene_to_load = "null"
VAR sample_to_add = "null"


//CALLOUT METHODS
// first set the desired global var then call these 
//scenes to fire the corresponding method in InkManager.cs
=== pick_up_item
   #ADD_ITEM
-> DONE

=== load_scene
    #LOAD_SCENE
->END

=== add_sample
   #ADD_SAMPLE
->END


=== default
    !!! THIS IS DEFAULT SCENE PLEASE SET SCENE TO READ IN TalkingObject.cs !!!
-> END


//DEBUGGING & TESTING
=== item 
    #debug 
    Pick up an item?
    + [yeah!]
        ~ item_to_add = 1 
        -> pick_up_item -> END
    + [no!] -> END
->  END

=== sample
    #debug
    Add a sample?
    +[ yeah!]
    ~ sample_to_add = 1 
    -> add_sample -> END 
-> END

=== test_sample 
    {
        - is_instrument_playing("Bass"):
            Bass is playing 
        - else: 
            Bass is not playing
    }
-> END