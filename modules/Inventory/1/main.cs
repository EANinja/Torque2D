function Inventory::create( %this )
{    
    // Load the preferences.
    %this.loadPreferences();
    
    // Load Inventory scripts.
    exec( "./scripts/assetGen.cs" );
    exec( "./scripts/Inventory.cs" );
    exec( "./scripts/InventoryGui.cs" );
    exec( "./scripts/verticalScrollContainer.cs" );
    exec( "./scripts/inventoryGridContainer.cs" );
        
    // Load GUI profiles.
    exec("./gui/guiProfiles.cs");

    // Load and configure the inventory dialog.
    Inventory.add( TamlRead("./gui/Inventory.gui.taml") );
    GlobalActionMap.bind( keyboard, "i", ToggleInventory );
}

//-----------------------------------------------------------------------------

function Inventory::destroy( %this )
{
    echo ( " @@@ Inventory::destroy()");
    %this.savePreferences();
}

//-----------------------------------------------------------------------------

function Inventory::loadPreferences( %this )
{
    // Load the default preferences.
    exec( "./scripts/InventoryPreferences.cs" );
    
    // Load the last session preferences if available.
    if ( isFile("preferences.cs") )
        exec( "preferences.cs" );   
}

//-----------------------------------------------------------------------------

function Inventory::savePreferences( %this )
{
    // Export only the Inventory preferences.
    export("$pref::Inventory::*", "preferences.cs", false );
}

function Inventory::createDraggingControl(%this, %sprite, %spritePosition, %mousePosition, %size)
{
    echo(" @@@ Inventory::createDraggingControl(" @ %this @ ", " @ %sprite @ ", " @ %spritePosition @ ", " @ %mousePosition @ ", " @ %size @ ")");
    // Create the drag and drop control.
    %dragControl = new GuiDragAndDropControl()
    {
        Profile = "GuiDragAndDropProfile";
        Position = %spritePosition;
        Extent = %size;
        deleteOnMouseUp = true;
    };

    // And the sprite to display.
    %spritePane = new GuiSpriteCtrl()
    {
        scene = %this.draggingScene;
        Extent = %size;
        Image = %sprite.Image;
    };
    //%spritePane.Frame = %sprite.Frame;
    %spritePane.frameNumber = %sprite.frameNumber;
    %spritePane.spriteClass = %sprite.class;

    // Place the guis.
    InventoryDialog.add(%dragControl);
    %dragControl.add(%spritePane);

    // Figure the position to place the control relative to the mouse.
    %xOffset = getWord(%mousePosition, 0) - getWord(%spritePosition, 0);
    %yOffset = getWord(%mousePosition, 1) - getWord(%spritePosition, 1);

    %dragControl.startDragging(%xOffset, %yOffset);
}

