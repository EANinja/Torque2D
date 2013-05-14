function Inventory::create( %this )
{    
    // Load the preferences.
    %this.loadPreferences();
    
    // Load Inventory scripts.
    exec( "./scripts/Inventory.cs" );
    exec( "./scripts/InventoryGui.cs" );
    exec( "./scripts/verticalScrollContainer.cs" );
        
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
