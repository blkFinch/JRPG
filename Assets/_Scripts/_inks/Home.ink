INCLUDE Main.ink

//PLAYER BEDROOM
//
=== bed
    #internal
    {&I'm not ready for bed... Maybe I should check my email...| Not yet.. | I'm not tired.}
-> END

=== lamp 
    #debug 
    Pick up an item?
    + [yeah!]
        ~ item_to_add = 1 
        -> pick_up_item -> END
    + [no!] -> END
->  END

=== door
    {hero_name()}, this is the door...
    + {check_item(2)} [Should we go?] 
        ~ scene_to_load = "Hallway"
        -> load_scene -> END
    + [Let's hang out here] ->END
->END

=== sp303
    #internal 
    Hey- it's my trusty 303. I should take it along just in case. Gotta have my sampler!
    +  {not check_item(2)} [Pick it Up?] 
            ~ item_to_add = 2
            -> pick_up_item -> END
    +   [Leave it here for now] ->END
-> END
