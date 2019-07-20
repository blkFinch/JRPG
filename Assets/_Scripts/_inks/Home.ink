
//PLAYER BEDROOM
//
=== bed
    #internal
    {&I'm not ready for bed... Maybe I should check my email...| Not yet.. | I'm not tired.}
-> END

=== door
    {hero_name()}, this is the door...
    + {check_has_item(2)} [Should we go?] 
        -> load_set("Hallway")
    + [Let's hang out here] ->END
->END

=== sp303
    {
        -not check_has_item(2):
            -> no_303
        - else: 
            {&it's a desk...| Nothing to see here!}
            -> END
    }

= no_303
    #internal 
    Hey- it's my trusty 303. I should take it along just in case. Gotta have my sampler!
    +  {not check_has_item(2)} [Pick it Up?] 
           -> add_item(2) -> END
    +   [Leave it here for now] -> END
-> END
