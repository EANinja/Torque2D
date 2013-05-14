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
    %buttonWidth = InventoryCancelButton.Extent.x;
    %dialogWidth = InventoryDialog.Extent.x;
    %x = %dialogWidth - %buttonWidth - 10;
    InventoryCancelButton.setPosition(%x, 10);
    %this.initialized = true;
}

function InventoryDialog::buyItem(%this, %item)
{
}

function InventoryDialog::sellItem(%this, %item)
{
}

