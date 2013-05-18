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
    %this.populateStorePane();
}

function InventoryDialog::onSleep(%this)
{
}

function InventoryDialog::initialize(%this)
{
    // set up store container
    Inventory.storeContainer = createVerticalScrollContainer();
    %this.storePane = new GuiControl()
    {
        Name="InventoryStorePane";
        Profile="InventoryDefaultProfile";
        HorizSizing="right";
        VertSizing="bottom";
        Position="0 0";
        Extent="240 768";
        MinExtent="240 320";
        Visible="1";
    };
    %this.addGuiControl(%this.storePane);
    %this.storePane.add(Inventory.storeContainer);
    Inventory.storeContainer.resizeContainer();

    // set up inventory container
    %this.inventoryPane = new GuiControl()
    {
        Name="InventoryInventoryPane";
        Profile="InventoryDefaultProfile";
        HorizSizing="relative";
        VertSizing="relative";
        Position="250 0";
        Extent="774 768";
        MinExtent="240 320";
        Visible="1";
    };
    %this.addGuiControl(%this.inventoryPane);
    Inventory.inventoryContainer = createInventoryGridContainer(%this.inventoryPane);
    Inventory.inventoryContainer.setCellBackground("Inventory:bagGrid");
    %this.inventoryPane.add(Inventory.inventoryContainer);
    Inventory.inventoryContainer.resizeContainer();

    %this.initialized = true;
}

function InventoryDialog::buyItem(%this, %item)
{
}

function InventoryDialog::sellItem(%this, %item)
{
}

function InventoryDialog::populateStorePane(%this)
{
    Inventory.storeContainer.clear();
    for (%i = 0; %i < 5; %i++)
    {
        %image = getWord($Inventory::StoreContents[%i], 0);
        %name = getWord($Inventory::StoreContents[%i], 1);
        %price = getWord($Inventory::StoreContents[%i], 2);
        Inventory.storeContainer.addButton(%this.createItemButton(%image, %name, %price), "", "", "");
    }
    Inventory.storeContainer.resizeContainer();
}

function InventoryDialog::createItemButton(%this, %itemImage, %itemName, %itemPrice)
{
    %button = new GuiControl()
    {
        Profile="InventoryDefaultProfile";
        HorizSizing="right";
        VertSizing="bottom";
        Position="0 0";
        Extent="190 60";
        MinExtent="100 60";
        Visible="1";
    };
    
    %buttonImage = new GuiButtonCtrl()
    {
        Profile="InventoryButtonProfile";
        HorizSizing="right";
        VertSizing="bottom";
        Position = "0 0";
        Extent="190 60";
        MinExtent="100 60";
        Visible="1";
    };
    %button.addGuiControl(%buttonImage);
    
    %itemSprite = new GuiSpriteCtrl()
    {
        Profile="InventoryDefaultProfile";
        HorizSizing="right";
        VertSizing="bottom";
        Position = "5 5";
        Extent="50 50";
        MinExtent="50 50";
        Visible="1";
        Image = %itemImage;
    };
    %buttonImage.addGuiControl(%itemSprite);

    %labelPos = (%itemSprite.Position.x + %itemSprite.Extent.x + 5) SPC "18";
    %itemLabel = new GuiTextCtrl()
    {
        canSaveDynamicFields="0";
        isContainer="0";
        Profile="InventoryDefaultProfile";
        HorizSizing="right";
        VertSizing="bottom";
        Position=%labelPos;
        Extent="84 23";
        MinExtent="8 2";
        canSave="1";
        Visible="1";
        Active="0";
        hovertime="1000";
        text=%itemName;
        maxLength="1024";
    };
    %buttonImage.addGuiControl(%itemLabel);

    %labelPos = (%itemLabel.Position.x + %itemLabel.Extent.x + 5) SPC "18";
    %itemPriceLabel = new GuiTextCtrl()
    {
        canSaveDynamicFields="0";
        isContainer="0";
        Profile="InventoryDefaultProfile";
        HorizSizing="right";
        VertSizing="bottom";
        Position=%labelPos;
        Extent="23 23";
        MinExtent="8 2";
        canSave="1";
        Visible="1";
        Active="0";
        hovertime="1000";
        text=%itemPrice;
        maxLength="1024";
    };
    %buttonImage.addGuiControl(%itemPriceLabel);
    
    return %button;
}
