//MAIN.INK
//
// This is where to store global VARs Methods and Scenes
// Include this in all inks to be safe

INCLUDE Debug.ink

INCLUDE Home.ink
INCLUDE Hallway.ink

//EXTERNAL FUNCTIONS

//checks if player has item of id i in inventory
//-- check ItemDatabase for item id no.s
EXTERNAL check_has_item(i) 
EXTERNAL check_has_sample(i) 
EXTERNAL hero_name()
EXTERNAL is_instrument_playing(s)

//GLOBAL VARs
VAR item_to_add = 0
VAR scene_to_load = "null"
VAR set_to_load = "null"
VAR sample_to_add = 0


//CALLOUT TUNNELS
// these are tunnel methods that call out to ink manager to perform action
//after the finish they will return to story flow

=== add_item(item_id)
    ~ item_to_add = item_id
    -> _pick_up_item

= _pick_up_item
   #ADD_ITEM
->->


=== add_sample(sample_id)
    ~ sample_to_add = sample_id
    -> _add_sample  

= _add_sample
   #ADD_SAMPLE
->->

=== load_scene(scene_name)
    ~ scene_to_load = scene_name
    -> _load_scene
= _load_scene
    #LOAD_SCENE
->END

=== load_set(set_name)
    ~ set_to_load = set_name
    -> _load_set
= _load_set
    #LOAD_SET
->END


=== default
    !!! THIS IS DEFAULT SCENE PLEASE SET SCENE TO READ IN TalkingObject.cs !!!
-> END
