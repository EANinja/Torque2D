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