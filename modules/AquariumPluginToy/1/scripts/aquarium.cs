//-----------------------------------------------------------------------------
// Copyright (c) 2013 GarageGames, LLC
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------

function getFishAnimationList()
{
   %list = "AquariumPluginToy:angelfish1Anim" @ "," @ "AquariumPluginToy:angelfish2Anim" @ "," @ "AquariumPluginToy:butterflyfishAnim";
   %list = %list @ "," @ "AquariumPluginToy:pufferfishAnim" @ "," @ "AquariumPluginToy:rockfishAnim" @ "," @ "AquariumPluginToy:seahorseAnim";
   %list = %list @ "," @ "AquariumPluginToy:triggerfish1Anim" @ "," @ "AquariumPluginToy:eelAnim";
}

//-----------------------------------------------------------------------------

function getFishSize(%anim)
{
    switch$(%anim)
    {
        case "AquariumPluginToy:angelfish1Anim":
        %fishInfo = "15 15";

        case "AquariumPluginToy:angelfish2Anim":
        %fishInfo = "15 15";
        
        case "AquariumPluginToy:butterflyfishAnim":
        %fishInfo = "15 15";
        
        case "AquariumPluginToy:pufferfishAnim":
        %fishInfo = "15 15";
        
        case "AquariumPluginToy:rockfishAnim":
        %fishInfo = "15 7.5";
        
        case "AquariumPluginToy:seahorseAnim":
        %fishInfo = "7.5 15";
        
        case "AquariumPluginToy:triggerfish1Anim":
        %fishInfo = "15 15";

        case "AquariumPluginToy:eelAnim":
        %fishInfo = "7.5 3.75";
    }

    return %fishInfo;
}

//-----------------------------------------------------------------------------

function buildAquarium()
{
    // Background
    %background = new Sprite();
    %background.setBodyType( "static" );
    %background.setImage( "AquariumPluginToy:background" );
    %background.setSize( 100, 75 );
    %background.setCollisionSuppress();
    %background.setAwake( false );
    %background.setActive( false );
    %background.setSceneLayer(5);
    SandboxScene.add( %background );
    
    // Far rocks
    %farRocks = new Sprite();
    %farRocks.setBodyType( "static" );
    %farRocks.setPosition( 0, -7.5 );
    %farRocks.setImage( "AquariumPluginToy:rocksfar" );
    %farRocks.setSize( 100, 75 );
    %farRocks.setCollisionSuppress();
    %farRocks.setAwake( false );
    %farRocks.setActive( false );
    %farRocks.setSceneLayer(4);
    SandboxScene.add( %farRocks );
    
    // Near rocks
    %nearRocks = new Sprite();
    %nearRocks.setBodyType( "static" );
    %nearRocks.setPosition( 0, -8.5 );
    %nearRocks.setImage( "AquariumPluginToy:rocksnear" );
    %nearRocks.setSize( 100, 75 );
    %nearRocks.setCollisionSuppress();
    %nearRocks.setAwake( false );
    %nearRocks.setActive( false );
    %nearRocks.setSceneLayer(3);
    SandboxScene.add( %nearRocks );
    
    // Left trigger
    %leftTrigger = new SceneObject() { class = "AquariumBoundary"; };
    
    %leftTrigger.side = "left";
    %leftTrigger.setSize( 5, 400 );
    %leftTrigger.setPosition( -85, 0);
    %leftTrigger.setSceneLayer( 1 );
    %leftTrigger.setSceneGroup( 15 );
    %leftTrigger.setCollisionGroups( 14 );
    %leftTrigger.createPolygonBoxCollisionShape( 5, 400);
    %leftTrigger.setDefaultDensity( 1 );
    %leftTrigger.setDefaultFriction( 1.0 );        
    %leftTrigger.setAwake( true );
    %leftTrigger.setActive( true );
    %leftTrigger.setCollisionCallback(true);
    %leftTrigger.setBodyType( "static" );
    %leftTrigger.setCollisionShapeIsSensor(0, true);
    SandboxScene.add( %leftTrigger );
    
    // Right trigger
    %rightTrigger = new SceneObject() { class = "AquariumBoundary"; };
    
    %rightTrigger.setSize( 5, 400 );
    %rightTrigger.side = "right";
    %rightTrigger.setPosition( 85, 0);
    %rightTrigger.setSceneLayer( 1 );
    %rightTrigger.setSceneGroup( 15 );
    %rightTrigger.setCollisionGroups( 14 );
    %rightTrigger.createPolygonBoxCollisionShape( 5, 400);
    %rightTrigger.setDefaultDensity( 1 );
    %rightTrigger.setDefaultFriction( 1.0 );    
    %rightTrigger.setAwake( true );
    %rightTrigger.setActive( true );
    %rightTrigger.setCollisionCallback(true);
    %rightTrigger.setBodyType( "static" );
    %rightTrigger.setCollisionShapeIsSensor(0, true);
    SandboxScene.add( %rightTrigger );    
}

//-----------------------------------------------------------------------------

function AquariumBoundary::onCollision(%this, %object, %collisionDetails)
{
    if (%object.class $= "FishClass")
        %object.recycle(%this.side);
}

//-----------------------------------------------------------------------------

function createAquariumEffects()
{
    %obj = new Scroller();
    %obj.setBodyType( "static" );
    %obj.setImage( "AquariumPluginToy:wave" );
    %obj.setPosition( 0, 0 );
    %obj.setScrollX(2);
    %obj.setSize( 100, 75 );
    %obj.setRepeatX( 0.2 );   
    %obj.setSceneLayer( 0 );
    %obj.setSceneGroup( 0 );
    %obj.setCollisionSuppress();
    %obj.setAwake( false );
    %obj.setActive( false );
    SandboxScene.add( %obj );

    // Add the bubbles particle.
    %bubbles= new ParticlePlayer();
    %bubbles.Particle = "AquariumPluginToy:Bubbles";
    %bubbles.setPosition( 0, -38 );
    %bubbles.setSceneLayer(4);
    SandboxScene.add( %bubbles );   
    
    // Add the caustics particle.
    %caustics = new ParticlePlayer();
    %caustics.Particle = "AquariumPluginToy:Caustics";
    SandboxScene.add( %caustics ); 
}

//-----------------------------------------------------------------------------

function FishClass::onAdd(%this)
{
    // Set a random speed for the fish
    %this.setSpeed();
   
    if (getRandom(0, 10) > 5)
    {
        %this.setLinearVelocityX(%this.speed);
        %this.setFlipX(false);
    }
    else
    {
        %this.setLinearVelocityX(-%this.speed);
        %this.setFlipX(true);
    }
}

//-----------------------------------------------------------------------------

function FishClass::recycle(%this, %side)
{
    // Fish has turned around, so set a new random speed
    %this.setSpeed();
    %layer = getRandom(0, 5);
    %this.setLinearVelocityY(getRandom(-3, 3));
    %this.setPositionY(getRandom(-15, 15));
    %this.setSceneLayer(%layer);

    if (%side $= "left")
    {
        %this.setLinearVelocityX(%this.speed);
        %this.setFlipX(false);
    }
    else if (%side $= "right")
    {
        %this.setLinearVelocityX(-%this.speed);
        %this.setFlipX(true);
    }
}

//-----------------------------------------------------------------------------

function FishClass::setSpeed(%this)
{
   // Speed is a dynamic variable created when this function is first called
   // Every other time after the first call will simply modify the variable
   // .minSpeed and .maxSpeed are declared in the Dynamic Fields rollout of the editor
   %this.speed = getRandom(%this.minSpeed, %this.maxSpeed);
}