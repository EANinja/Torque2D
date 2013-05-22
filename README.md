Torque2D
========

MIT Licensed Open Source version of Torque 2D from GarageGames

This is a stripped set of modules just to isolate an inventory module and get it working in T2D.

When the "game" starts, hit 'i' to open the "store" for use.

At the moment you can drag stuff from the container on the left to the container on the right,
relocate items in the right container, set the right container to a few different fill options, 
and the dragged objects retain the inventory information from the original set.

I'm going to get the actual inventory data transferring from the "store" inventory to the "player"
inventory shortly, and also implement an example method of handling money.

Because there are many ways of tracking object attributes I'm not going to go down that path.  I've 
shown an example that uses a tag or ID to track what a particular object represents and leave it as 
an exercise for the reader to handle translating this between the inventory and actual game entities.
