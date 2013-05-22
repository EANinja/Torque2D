function ToggleInventory( %make )
{
    // Finish if being released.
    if ( !%make )
        return;
        
    // Is the console awake?
    if ( InventoryDialog.isAwake() )
    {
        Canvas.popDialog(InventoryDialog);    
        return;
    }
    
    Canvas.pushDialog(InventoryDialog);
}

/// <summary>
/// Returns total inventory content in the following format:
///
/// "ToyAssets:Planetoid Rocks 10 5" NL
/// "ToyAssets:TD_Bones_01Sprite Sticks 8 5"
///
/// where each record is space-delimited and contains each inventory slot's 
/// data.
/// If the item count is zero it isn't returned. (Remember -1 is infinite)
/// <summary>
/// <return>Returns a new-line delimited set of space delimited records containing item data.</return>
function InventoryObject::getContents(%this)
{
    %index = 0;
    %invSlot = %this.contents[%index];
    while(%invSlot !$= "")
    {
        %itemCount = getWord(%invSlot, 3);
        if (%itemCount !$= "0" )
        {
            if (%contents $= "")
                %contents = %invSlot;
            else
                %contents = %contents NL %invSlot;
        }
        %index++;
        %invSlot = %this.contents[%index];
    }
    return %contents;
}

/// <summary>
/// Removes %count of %item from the inventory.
/// </summary>
/// <param name="item">The item to remove.</param>
/// <param name="count">How many to remove.</param>
function InventoryObject::removeItem(%this, %item, %count)
{
    if ( %count $= "" )
        %count = 1;
}

/// <summary>
/// Adds %count of %item to the inventory.
/// </summary>
/// <param name="item">The item to add.</param>
/// <param name="count">How many to add.  Set to -1 to make this item unlimited</param>
function InventoryObject::addItem(%this, %item, %count)
{
    if ( %count $= "" )
        %count = 1;
    // Do we have this item?
}

/// <summary>
/// Finds an item in the inventory.
/// </summary>
/// <param name="item">The item to find.</param>
/// <return>Returns the slot if found, or -1 if not.
function InventoryObject::findItem(%this, %item)
{
    %index = 0;
    %found = false;
    %invSlot = %this.contents[%index++];
    while(%invSlot !$= "")
    {
        %current = getWord(%invSlot, 1);
        if ( %current %= %item )
        {
            %found = true;
            break;
        }
        %invSlot = %this.contents[%index++];
    }
    if (%found)
        %index -= 1;
    else
        %index = -1;
    return %index;
}

function InventoryObject::addInventoryItem(%this, %itemData)
{
    %index = 0;
    %invSlot = %this.contents[%index];
    while(%invSlot !$= "")
    {
        %index++;
        %invSlot = %this.contents[%index];
    }
    %this.contents[%index] = %itemData;
}

/// <summary>
/// Handles inventory button clicks.
/// This can be used to route clicks for individual inventory instances.
/// </summary>
/// <param name="data">The data attached to the clicked object.</param>
function InventoryObject::invButtonClick(%this, %data)
{
    // The first element of the data collection I pass is the ID of the 
    // inventory object that the data belongs to.  This is not the same as the
    // container gui - it refers to the script object where the data originates.
    echo(%data);
}

function InventoryObject::onMouseDragged(%this, %modifier, %mousePoint, %mouseClickCount)
{
    echo(" @@@ InventoryObject::onMouseDragged(" @ %this @ ", " @ %modifier @ ", " @ %mousePoint @ ", " @ %mouseClickCount @ ")");
    if (!%this.getParent().isActive())
        return;

    %position = %mousePoint;
    %halfParentWidth = %this.sprite.Extent.x / 2;
    %halfParentHeight = %this.sprite.Extent.y / 2;
    %position.x -= %halfParentWidth;
    %position.y -= %halfParentHeight;
    Inventory.createDraggingControl(%this.sprite, %position, %mousePoint, %this.sprite.Extent);
}

function GuiSpriteCtrl::onDelete(%this)
{
    echo(" @@@ " @ %this @ ":" @ %this.getClassName() @ " deleted");
}