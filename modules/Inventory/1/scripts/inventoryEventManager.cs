//-----------------------------------------------------------------------------
// 3 Step Studio
// Copyright GarageGames, LLC 2012
//-----------------------------------------------------------------------------

// Builds the event manager and script listener that will be responsible for
// handling important system events, such as a module group finishing a load,
// a patch being available, a download completing, etc
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
