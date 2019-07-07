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

//OVERIDE
VAR item_to_add = 1 
VAR scene_to_load = "null"

=== default
    !!! THIS IS DEFAULT SCENE PLEASE SET SCENE TO READ IN TalkingObject.cs !!!
-> END

=== pick_up_item
   #ADD_ITEM
-> DONE

=== load_scene
    #LOAD_SCENE
->END


