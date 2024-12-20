# VFX Workshop w/ Franz!

### Project

Start by opening `Assets/WorkshopScene`

**Controls**
1. LMB to shoot towards the mouse
2. WASD to move

Hitting a target dummy makes it disappear.

### Workshop Activity

Under `Assets/Prefabs[WORKSHOP]` are two prefabs, one for the projectile and one for the player.

`GameObjects` with the `[MODIFY ME]` postfix are particle gameobjects!

**These are:**

Protaganist (Player)
1. *Run particles* (plays when the player starts running, stops when they stop)
2. *Jump particles* (plays when the player jumps)

Projectile
1. *Live particles* (plays when the particle spawns, stops on collision)
2. *Collision particles* (plays on collision)
3. *Spawn particles* (plays on spawn)

Aim Cursor
1. *Cursor Particles* (plays at the start)

There are particle systems named `Example` under each one. Set the gameobject to active to see them!

To add new particle systems, either:
1. **Right Click > Duplicate** an `Example` particle system and modify it
2. Start from scratch by creating an empty gameobject under a particle gameobject and adding a `ParticleSystem` component through the inspector. Make sure to disable `Play On Awake`

### Design new VFX and see how the game's feel changes
### Share videos/screenshots of your VFX in the workshop discord channel


#### For show-offs:

The project uses the Universal Render Pipeline.
Try combining Shader Graph materials with particles to get even cooler effects

### Used Assets
https://opengameart.org/content/colored-summoning-circles