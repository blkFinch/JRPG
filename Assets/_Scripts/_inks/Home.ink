INCLUDE Main.ink

//PLAYER BEDROOM
//
=== bed
    #internal
    {&I'm not ready for bed... Maybe I should check my email...| Not yet.. | I'm not tired.}
-> END

== add_new_sample 
    Add another sample?
    +[yup..]
        -> add_sample(1)
->END


=== door
    {hero_name()}, this is the door...
    + {check_item(2)} [Should we go?] 
        -> load_scene("Hallway")
    + [Let's hang out here] ->END
->END

=== sp303
    #internal 
    Hey- it's my trusty 303. I should take it along just in case. Gotta have my sampler!
    +  {not check_item(2)} [Pick it Up?] 
           -> add_item(2)
    +   [Leave it here for now] ->END
-> END
