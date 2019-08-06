//DEBUGGING & TESTING
=== item 
    #debug 
    Pick up an item?
    + [yeah!]
        -> add_item(1)
->  END

=== test_tunnel
~ item_to_add = 1
        this is a tunnel <>
        -> sub_tunnel 
= sub_tunnel
#ADD_ITEM
        this is sub tunnel <>
->->

=== sample
    #debug
    Add a sample?
    +[ yeah!]
        -> add_sample(1) -> another

= another
    add another?
    + [ yes!]
        -> add_sample(2) -> END
-> END

=== test_sample 
    {
        - is_instrument_playing("Bass"):
            Bass is playing 
        - else: 
            Bass is not playing
    }
-> END

== test_pause 
        This sentence should be displayed first
        + [$pause] 
                then this text
->END 
