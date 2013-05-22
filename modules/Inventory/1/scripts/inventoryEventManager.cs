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

function initializeInventoryEventManager()
{
    if (!isObject(InventoryEventManager))
    {
        $InventoryEventManager = new EventManager(InventoryEventManager)
        { 
            queue = "InventoryEventManager"; 
        };
        
        // Module related signals
        InventoryEventManager.registerEvent("_ItemDeleteRequest");
    }
    
    if (!isObject(InventoryListener))
    {
        $InventoryListener = new ScriptMsgListener(InventoryListener) 
        { 
            class = "InventoryEventListener"; 
        };
        
        // Module related subscriptions
        InventoryEventManager.subscribe(InventoryListener, "_ItemDeleteRequest", "onItemDeleteRequest");
    }
}

// Cleanup the InventoryEventManager
function destroyInventoryEventManager()
{
    if (isObject(InventoryEventManager) && isObject(InventoryListener))
    {
        // Remove all the subscriptions5        InventoryEventManager.remove(InventoryListener, "_AnimUpdateRequest");
        InventoryEventManager.remove(InventoryListener, "_ItemDeleteRequest");

        // Delete the actual objects
        InventoryEventManager.delete();
        InventoryListener.delete();
        
        // Clear the global variables, just in case
        $InventoryEventManager = "";
        $InventoryListener = "";
    }
}

function InventoryEventListener::onItemDeleteRequest(%this, %msgData)
{
    InventoryDialog.schedule(64, deleteObject, %msgData);
}
