GUI Elements Pack 1.0
Copyright 2012 David Durston
Contact: http://forum.unity3d.com/members/96047-JumpDog
________________________


Hi and thanks for purchasing this GUI Elements Pack! This pack was made primarily to be used with NGUI but since all the original sprites are included, should be able to be used with other GUI solutions as well. If you have NGUI, simply install the GUI_Elements_NGUI_Files.unitypackage to get the example scenes and atlases.
If you'd like to test it out, the Distribution (free trial) version of NGUI is available here: http://www.tasharen.com/get.php?file=NGUIDistro

Please feel free to contact me via Email or PM at the above Contact link if you need help or find something that needs fixing in a future update!


____________________________
UPDATING
____________________________

*If you modify or customize any of the sprites, be sure to prefix them with your name or something so you can find them and they don't get
overwritten on an update*

If an update for this is released including fixes or additional sprites, the original filenames will stay the same. If you have taken a .PNG
and edited it or are otherwise using your own versions of sprites with the same names, be sure to back them up!

____________________________
Hints using GUI Elements
____________________________

This pack was made with a very modular mindset to allow a non-artist to build graphically-customizable GUIs by assembling various pieces together.
The benefits of this include being able to recolor the white sprites to any color you want, and scaling them and customizing them with any fill or corner piece you want.
With all of the original .PNGs included, you can easily take them into Photoshop, GIMP, etc, and merge them together into a single sprite just as you want them to reduce the number of triangles and add further customization.

*IT IS ADVISED that you only use the included Atlas as a reference for where to slice objects and to use to prototype different possible combinations of elements. Ultimately, it would be best to either build your own atlas with the included PNGs, or copy the included atlas, deleting the sprites you will not use, as the atlas is 2048x2048 pixels and it is highly unlikely all of them will be used by any one person. Making your own smaller atlas with specific sprites will be better for performance.

Here are some helpful hints when building GUI panels and objects with NGUI:

- Use NGUI's search bar when you're looking through for a sprite. Just try "stroke" or "box" or "fill" or "gauge" rather than going through all of them!

- If you want a drop shadow for an element that doesn't have a "shadow" version, just use that same element, blacken it with a low opacity, and place it in the back!

- Adding a square tiled fill into a rounded box leads to the corners showing through from behind. Best way to correct this is the scale the tiled fill down so it fits inside the box, then use a "circle fill" sliced sprite to fill in the gaps, placing it behind both the box and tiled fill. See the "Panels" example scene for this in use.

- Experiment with slicing! You can change of the sprites around to get different effects sometimes, or using sprites in ways you might not think. In the "Panels" example is a case of using a "stroked 16-cut circle" as corners for a rounded box shape (lower left panels).

- Sometimes you might try to get some elements together whose sizes will not agree, especially when using sliced sprites. A workaround is to adjust the Panel object's scale as well, which will influence its children elements. This can help to make some elements fit together better.

- Care has been taken to keep sprite blank space low. When placing progress bars, sometimes the foreground and background sprites may need to be aligned together, or the background sprite may just need to be scaled (you may need to replace the background object with a sliced-sprite if it does not default to that).

____________________________
Version History
____________________________

Version 1.3
- GUI Elements has a total of 412 sprites
- Added 6 media controls in 2 versions, normal and cutout. These are found in Sprites > Icons > Media Controls.

New additions:
- play
- play_cutout
- stop
- stop_cutout
- fastForward
- fastForward_cutout
- toEnd
- toEnd_cutout
- record
- record_cutout
- pause
- pause_cutout

________________________________________________________
Version 1.2
- GUI Elements has a total of 400 sprites
- 112 new unique sprites, 133 total in patch
- First update, major additions include ammunition gauges, control objects, button components, and many icons
- Full list of new additions:

OBJECTS

> compass_needle
> compass_bg
> radarSquare74Deg
> radarCircle60Deg
> radarCircle90Deg


_________________________

GAUGES

> gauge_needle_flat
> gauge_needle_stroke
> gauge_needle_shadow
> gauge_needle_glow
> gauge1_flat
> gauge1_stroke
> gauge1_shadow
> gauge1_glow
> ammo_bullets15
> ammo_bullets30
> ammo_shells8
> ammo_shells8_long

_________________________

BUTTONS

> SQUARE

> > effect_square_glowOUT
> > effect_square_glowIN
> > effect_square_shadow
> > effect_square_stroke
> > effect_square_bevel
> > square_twoTone2
> > square_twoTone1
> > square_gradient3
> > square_gradient2
> > square_gradient1
> > square_flare
> > square_flat

> ROUND

> > effect_round_glowOUT
> > effect_round_glowIN
> > effect_round_shadow
> > effect_round_stroke
> > effect_round_bevel
> > round_twoTone2
> > round_twoTone1
> > round_gradient3
> > round_gradient2
> > round_gradient1
> > round_flare
> > round_flat


> BUTTONENDS

> > buttonEnd_round_1
> > buttonEnd_9
> > buttonEnd_8
> > buttonEnd_7
> > buttonEnd_6
> > buttonEnd_5
> > buttonEnd_4
> > buttonEnd_3
> > buttonEnd_2
> > buttonEnd_1

_________________________

CONTROLS

> cntrl_dpad_3
> cntrl_dpad_2
> cntrl_dpad_1
> cntrl_run
> cntrl_jump
> cntrl_pedalBrake
> cntrl_pedalGas
> cntrl_shoot
> cntrl_punch

_________________________

RETICLES

> Reticle11 (flat, stroked, shadow, glow)
> Reticle12 (flat, stroked, shadow, glow)
> Reticle13 (flat, stroked, shadow, glow)
> Reticle14 (flat, stroked, shadow, glow)
> Reticle15 (flat, stroked, shadow, glow)

_________________________

ICONS

> ICON

> > icon_hand
> > icon_lockOff2
> > icon_lockOff1
> > icon_lockOn
> > icon_zoomOUT
> > icon_zoomIN
> > icon_refresh
> > icon_home
> > icon_info
> > icon_alarmRing
> > icon_alarm
> > icon_reception2_on3
> > icon_reception2_on2
> > icon_reception2_on1
> > icon_reception2_off
> > icon_reception1
> > icon_battery
> > icon_flashlightOn2
> > icon_flashlightOn1
> > icon_flashlightOff
> > icon_cross
> > icon_speakerOn3
> > icon_speakerOn2
> > icon_speakerOn1
> > icon_speakerOff
> > icon_gear
> > icon_lightning
> > icon_star
> > icon_heart
> > icon_shield

> WEAPONS

> > ammo_rocket
> > ammo_arrow
> > ammo_bullet
> > ammo_shell
> > wpn_bow
> > wpn_spear
> > wpn_knife
> > wpn_sword
> > wpn_axe1
> > wpn_axe2
> > wpn_sniper
> > wpn_launcher
> > wpn_smg
> > wpn_rifle
> > wpn_pistol
> > wpn_grenade
> > wpn_shotgun

> VEHICLES

> > icon_jet
> > icon_plane
> > icon_car
> > icon_tank
> > icon_helicopter
> > icon_boat

________________________________________________________
Version 1.0
- Initial Release
- GUI Elements has a total of 267 sprites