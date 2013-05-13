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

function Inventory::create( %this )
{    
    // Load the preferences.
    %this.loadPreferences();
    
    // Load Inventory scripts.
    exec( "./scripts/Inventory.cs" );
        
    // Load GUI profiles.
    exec("./gui/guiProfiles.cs");

    // Load and configure the inventory dialog.
    Inventory.add( TamlRead("./gui/Inventory.gui.taml") );
    GlobalActionMap.bind( keyboard, "i", ToggleInventory );
}

//-----------------------------------------------------------------------------

function Inventory::destroy( %this )
{
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
