function InventoryDialog::onDialogPush(%this)
{
}

function InventoryDialog::onDialogPop(%this)
{
}

function InventoryDialog::onWake(%this)
{
    if ( !%this.initialized )
        %this.initialize();
}

function InventoryDialog::onSleep(%this)
{
}

function InventoryDialog::initialize(%this)
{
    Inventory.storeContainer = createVerticalScrollContainer();
    %this.storePane = new GuiControl()
    {
        Name="InventoryStorePane";
        Profile="InventoryDefaultProfile";
        HorizSizing="relative";
        VertSizing="relative";
        Position="0 0";
        Extent="240 768";
        MinExtent="240 320";
        Visible="1";
    };
    %this.add(%this.storePane);
    %this.storePane.add(Inventory.storeContainer);
    Inventory.storeContainer.resizeContainer();
    %this.initialized = true;
}

function InventoryDialog::buyItem(%this, %item)
{
}

function InventoryDialog::sellItem(%this, %item)
{
}

