# Space-Rocks

### Shoot the space rocks in this 2D shooter

## Game Design Document

### Game Overview	
  
Space Rocks is a two-dimensional game with a player's spaceship and a lot of rocks.  The ship can accelerate, decelerate, rotate, or fire a missile.  The rocks just bounce around.  When a missile strikes a rock, the rock fragments into smaller pieces.  When a missile strikes a small-enough piece, it vaporizes and is no longer a threat.  Until then, a rock or fragment that hits the ship kills it.  Rocks, missiles, and the ship bounce off the sides of the gamespace.  The ship can be killed by its own missiles.  The player gets three lives per play.  Points are scored for shooting rocks and for clearing the play field, which results in a new set of rocks to destroy.
  
###  Difference from other similar games	

Unlike asteroids, the game entities bounce off the sides instead of wrapping to the other side in torroidal fashion.
  

### Feature Set	

+ 2D physics

+ ship accelerates by control, but there is drag.  Rotation happens when the rotate key is held down

+ firing is single shot, one missile per keypress.  Firing may be rate-limited.  Missiles travel a fixed distance before evaporating.

+ Rocks, when hit by a missile, split into smaller rocks which inherit the energy from the parent rock.

+ Score display and high score saving

+ music and sound effects

+ various visual effects

+ Options menu to adjust volume for music and sound effects
  
### Camera	
2D orthogonal

### Game Engine	
Unity3d

