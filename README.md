Unity JRPG
===
This is my attempt at recreating a Final Fantasy 7 like JRPG in unity.
Some additional improvements I'd like to add:
- branching dialogue paths using Ink
- light rythm based combat mechanics

SET UP AND BEST PRACTICES
=
some notes about setting up and using the game engine

Ink
-
- Supress all dialogue choices
- Sustain all dialogue choices(unless you want to chance the user never seeing that choice 
 again after an accidental x out)

 so most choices should look like this:

 ```
 dialogue here. 
    + [the plus sign sustains the choice option]
    + [the brackets surpress the output]
```

Items
-
First create a scriptable object of the desired type 
then add scriptable object of effect 
and finally add the object to the item database