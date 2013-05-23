//-----------------------------------------------------------------------------
// Copyright (c) 2013 Roostertail Games
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

$PlayerMoney = $pref::Inventory::default::playerMoney;

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
    // this is here to support stackable items, ignore for now
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
    // this is here to support stackable items, ignore for now
    if ( %count $= "" )
        %count = 1;
    // Do we have this item?  This is where we have to handle stackable vs.
    // non-stackable items.
    %index = 0;
    %invSlot = %this.contents[%index];
    while(%invSlot !$= "")
    {
        %index++;
        %invSlot = %this.contents[%index];
    }
    %this.contents[%index] = %item;
}

/// <summary>
/// Finds an item in the inventory.
/// </summary>
/// <param name="item">The item to find.  This uses the "game item ID" to search.</param>
/// <return>Returns the slot if found, or -1 if not.
function InventoryObject::findItem(%this, %item)
{
    %index = 0;
    %found = false;
    %invSlot = %this.contents[%index];
    while(%invSlot !$= "")
    {
        %current = getWord(%invSlot, 4);
        if ( %current %= %item )
        {
            %found = true;
            break;
        }
        %index++;
        %invSlot = %this.contents[%index];
    }
    if (!%found)
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
    if (!%this.getParent().isActive())
        return;

    %position = %mousePoint;
    %halfParentWidth = %this.sprite.Extent.x / 2;
    %halfParentHeight = %this.sprite.Extent.y / 2;
    %position.x -= %halfParentWidth;
    %position.y -= %halfParentHeight;
    Inventory.createDraggingControl(%this.sprite, %position, %mousePoint, %this.sprite.Extent);
}

/// <return>True if purchase was successful</return>
function InventoryObject::buyItem(%this, %itemData)
{
    // for my basic store item I'm using a format as follows:
    // asset label cost stock objectTag
    // asset: the asset of the image to display
    // label: the name of the object to display
    // cost: the item cost
    // stock: the number of the item available in the store, -1 is infinite
    // objectTag: a tag that can be used to link our store object with a game object.
    %price = getWord(%itemData, 2);
    if ($PlayerMoney < %price)
    {
        // make a pop-up and tell the loser to get more money!
        // in the mean time, just echo and return
        InventoryDialog.noCashCtrl.text = "Not enough money to buy " @ getWord(%itemData, 1);
        return false;
    }
    $PlayerMoney -= %price;
    InventoryDialog.currentCashCtrl.text = $PlayerMoney;
    return true;
}

function InventoryObject::sellItem(%this, %itemData)
{
    %price = getWord(%itemData, 2);
    $PlayerMoney += %price;
    %this.removeItem(%itemData);
    InventoryDialog.noCashCtrl.text = "";
    InventoryDialog.currentCashCtrl.text = $PlayerMoney;
}
