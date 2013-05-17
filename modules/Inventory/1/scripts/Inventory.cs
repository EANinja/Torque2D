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

function InventoryObject::getContents()
{
    
}

function InventoryObject::removeItem(%this, %item, %count)
{
    if ( %count $= "" )
        %count = 1;
}

function InventoryObject::addItem(%this, %item, %count)
{
    if ( %count $= "" )
        %count = 1;
}

$Inventory::StoreContents[0] = "ToyAssets:Planetoid Rocks 10 5";
$Inventory::StoreContents[1] = "ToyAssets:TD_Bones_01Sprite Sticks 8 5";
$Inventory::StoreContents[2] = "ToyAssets:brick_01 Food 25 10";
$Inventory::StoreContents[3] = "ToyAssets:TD_Crystal_blueSprite Water 5 -1";
$Inventory::StoreContents[4] = "ToyAssets:TD_Crystal_redSprite Knife 50 5";
