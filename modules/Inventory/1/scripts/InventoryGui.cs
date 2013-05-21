/// <summary>
/// Catch all in case there's something that catches drag and drops beneath us.
/// </summary>
/// <param name="this">The Inventory Gui</param>
/// <param name="control"> The control that was dropped.</param>
/// <param name="position"> The position the control was dropped at.</param>
function InventoryDialog::onControlDropped( %this, %control, %position )
{
    // check contained controls.  If any of them handle dropped controls then
    // let them do it.
    if ( ABStoryboardWindow.pointInControl( %position.x, %position.y ) )
        ABStoryboardWindow.onControlDropped( %control, %position );
    if ( %control.spriteClass $= "ABStoryboardPreviewSprite" )
        AnimationBuilder.removeFrame( %control.frameNumber );
}

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
    // set up store inventory
    %newInv = new ScriptObject()
    {
        class="InventoryObject";
    };
    Inventory.storeInventory = %newInv;
    Inventory.storeInventory.addInventoryItem("ToyAssets:Planetoid Rocks 10 5");
    Inventory.storeInventory.addInventoryItem("ToyAssets:TD_Bones_01Sprite Sticks 8 5");
    Inventory.storeInventory.addInventoryItem("ToyAssets:brick_01 Food 25 10");
    Inventory.storeInventory.addInventoryItem("ToyAssets:TD_Crystal_blueSprite Water 5 -1");
    Inventory.storeInventory.addInventoryItem("ToyAssets:TD_Crystal_redSprite Knife 50 5");
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

    // set up container inventory
    %newInv = new ScriptObject()
    {
        class="InventoryObject";
    };
    Inventory.containerInventory = %newInv;
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
    %this.inventoryPane.add(Inventory.inventoryContainer);
    Inventory.inventoryContainer.resizeContainer();
    Inventory.inventoryContainer.setCellBackground("Inventory:bagGrid");

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
    %contents = Inventory.storeInventory.getContents();
    %itemCount = getRecordCount(%contents);
    for (%i = 0; %i < %itemCount; %i++)
    {
        %itemRecord = getRecord(%contents, %i);
        %image = getWord(%itemRecord, 0);
        %name = getWord(%itemRecord, 1);
        %price = getWord(%itemRecord, 2);
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
