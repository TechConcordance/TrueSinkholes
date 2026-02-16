# TrueSinkholes
This is a LabAPI plugin that:
1. Allows users to modify the range of sinkholes without affecting their size.
2. Adds a teleport area to sinkholes that causes players to get teleported to the Pocket Dimension if they're too close to the epicenter.
<br></br>
## Why did I make this?
Because I saw someone post a LabAPI plugin that is a port of a port of a plugin called 'BetterSinkholes'. Ridiculous.

Low-effort ports and AI slop piss me off, which are things I've consistently been vocal about.  
I figured if I talk the talk, I better walk the walk, so I put my money where my mouth is and wrote this thing.  
It has no extra dependencies, no redundancies, no unnecessary bullshit.
<br></br>
## Notes
I've included two configuration options. Both of them are float values.
| Option | Default Value | What it does |
|--------|---------------|--------------|
| `SlowDistanceMult` | `1f` | This multiplies the radius of sinkhole effect areas (`MaxDistance`) by however much you want. As stated before, this does not effect their physical size, only the radius in which the sinkhole effect is applied. |
| `TeleportDistanceMult` | `0.6f` | This determines the radius of the teleport area, relative to the maximum distance. This *is* effected by the `SlowDistanceMult`, as that changes the `MaxDistance` of the sinkhole. |

The original 'BetterSinkholes' plugins had an optional broadcast that could be sent to players if they are teleported to the pocket dimension.  
I removed that because this plugin is rather minimalistic and I didn't think anyone would really use the broadcast feature.  
If you want anything like that added, or anything additional, just let me know and I'll figure it out at some point.
