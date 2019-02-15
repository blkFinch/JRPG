
=== default
    = greeting
        Hello! this is initial text! Several choice should be expected!
        +   [Hey!] -> bored
        +   [option 2!] -> bored
        +   [who are you?] -> get_quest
    ->DONE

    = bored 
        thats all you have to say?
    ->DONE

    = get_quest
        I am a quest giving NPC!
        +   [Oh thats cool]
            It is very cool!
    ->DONE
->END

=== no_choice 
    Hello no choices are expected here! I hope you're having a great day!
->END
    