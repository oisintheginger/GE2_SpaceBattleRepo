# GE2_SpaceBattleRepo
 Project Repo for Game Engines 2. Project goal is to recreate a scifi battle in unity using autonomous agents, pathfinding etc.

## Demo Video ##

[![](http://img.youtube.com/vi/zs_Amdz-2MU/0.jpg)](http://www.youtube.com/watch?v=zs_Amdz-2MU "")

## Implementation ##
I only managed to get two of the three scenes, which are the 'Alert' and the 'Engagement'. These are the first and last scenes of the initial storyboard. 

### Alert ###
Alert starts off with soldiers randomly wandering a scene. To wander, I implemented a grid system wich generates a grid of nodes, and depending on whether they are inside an 'obstacle object' it will mark them as traversable or non-traversable. I used a non-refactored version of the code we did in class for all the movement, and used a switch statement and timer to trigger different events. All camera changes are timed and or triggered by an event, i.e hitting a trigger to do the over wing shot. Animations were gathered from mixamo and none of the models were of my own design except the dalek turret, which I built using probuilder. This portion of the assignment was completed around February, before we covered the refactored steering behaviours in class.

### Engagement ###
I started this with scene around the 20th of April, gathering resources. I also looked on the class github for how to refactor the code for steering behaviours, and I pretty much implemented them as they were since they were all I needed for the assignment. I spent some time tinkering with the obstacle avoidance behaviour, as I could not get it to work as I wanted it to. In the end the bulk of my work for this section was just setting up the scene to flow in a certain way. I had cameras trigger through trigger colliders and timing. The Dalek ship turrets were also implemented just using a trigger sphere. When a fighter plane entered the sphere, the plane was added to a list of targets for the turret. The turret always targets the first index of that list. When a plane leaves the sphere, it is removed from the list. 

## What I am most proud of: ##
I'm proud that I managed to finish the assignment, after the big gap in work and the situation with the lockdown, I found it hard to find the will to finish this assignment. I had just finished another assignment which I got a great grade for, and starting back on this project with no momentum was pretty tough. Other than that, in terms of code, I'm proud that I now have a stronger sense of abstract classes and inheritance, as I was able to understand the code from the class repo. Also that I managed to get the grid system working for myself, even if it isn't the optimal solution to the problem.

## Instructions ## 
Just run from the Earth scene, or make a build, and it should run through by itself. The project is automated, so it requires no user input.

## Project Concept ##

I am going to recreate the space battle scene from the Doctor Who episodes 'Into the Storm' and 'Victory of the Daleks'. 
I am basing my project on this video clip from YouTube, and will be using it as a storyboard as it has interesting shots that seem to be achievable:

[![](http://img.youtube.com/vi/HirwnpeugNM/0.jpg)](http://www.youtube.com/watch?v=HirwnpeugNM "")

## Initial Breakdown/ Storyboard ##

In order to recreate this battle I have broken it up into three separate scenes; 'The Alert', 'Approach', and 'Engagement'.
'The Alert' has so far been completed with the scene progressing from stage to stage, utilising timers and events to activate cameras and camera movements.
It involves the pilots being alerted by the commander of the Dalek threat. The pilots then run to their planes and takeoff. Camera shots are activated by using timers
and by relying on AI reaching specific points at specific times.

'Approach' will be a simple scene starting off with the Doctor and the Daleks inside the ship moving around(Perhaps audio clips can be played).
Then I will switch to a camera shot of the space planes doing an offset pursuit around the globe earth. After demonstrating some cool shots, alternating from camera
to camera.

'Engagement' will be a very long scene, I hope to have a central Dalek ship in the middle of the scene and have many space planes battling around it, approaching 'targets'
to attack. I will switch from shot to shot, recreating some of the scenes from the video, i.e. the Dalek turret destroying a plane. The scene will end when the space
planes manage to damage the Dalek ship enough,
which will cause the main Dalek gun to explode, and the scene will end.

Sources: 
None of the models in the project are of my own creation, music is the Doctor Who Theme Music and SFX are from Freesound.org

Sources:
* Real star Skyox lite,Geoff Dallimore, Unity Asset Store
* Planet Earth Free, headwards, Unity Asset Store 
* P-51 Mustang, by PB-design, Free3D.com
* Dalek, SoMeme starwars, Sketchfab.com
* Dalek Ship, Megan Grant, Sketchfab.com
* Swat, Mixamo.com
* Alex, Mixamo.com
* willys jeep, Johannes Nienbar, Sketchfab
* Military Tent, Venikamen, sketachfab.com
* Wooden Crates, Animation Arts Creative GmBH, Unity Asset Store
* Fantasy Lamdscape, Pxltiger, Unity Asset Store
* Nature Ground Weed Texture, Free Stock Textures, freestocktexture.com
