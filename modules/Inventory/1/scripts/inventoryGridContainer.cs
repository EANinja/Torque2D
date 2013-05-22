//-----------------------------------------------------------------------------
// Utility methods for InventoryGridCtrl
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Utility methods for InventoryGridCtrl
//-----------------------------------------------------------------------------

/// <summary> These adjust the scroll indicator arrow position relative to the 
/// target control.
/// </summary>
$InventoryGridCtrl::ScrollIndicatorYOffset = 2;
$InventoryGridCtrl::ScrollIndicatorXOffset = -3;

/// <summary>
/// This clears the control's contents
/// </summary>
function InventoryGridCtrl::clear(%this)
{
    if (!isObject(%this.deletionSet))
        %this.deletionSet = new SimSet();

    for(%i = 0; %i < %this.deletionSet.getCount(); %i++)
    {
        %obj = %this.deletionSet.getObject(0);
        %obj.delete();
    }
    
    for(%i = 0; %i < %this.contentPane.getCount(); %i++)
    {
        %obj = %this.contentPane.getObject(%i);
        %this.deletionSet.add(%obj);        
    }
    %this.contentPane.clear();
    %this.buttonHeight = 0;
}

/// <summary>
/// This scrolls the control to the desired button
/// </summary>
/// <param name="index">The index of the desired button</param>
function InventoryGridCtrl::scrollToButton(%this, %index)
{
    if (%index < 0)
        %index = 0;

    if (%index > %this.contentPane.getCount())
        %index = %this.contentPane.getCount();
    %itemCount = %this.contentPane.getCount();
    %spacing = %this.contentPane.rowSpacing;
    %maxScroll = (%this.buttonHeight * %itemCount) + (%itemCount * %spacing) - %this.buttonHeight;

    %position = %index * (%spacing + %this.buttonHeight);
    if (%position > %maxScroll)
        %position = %maxScroll;
    %this.scrollCtrl.setScrollPosition(0, %position);
}

/// <summary>
/// This gets the vertical position of the desired button
/// </summary>
/// <param name="index">The index of the desired button</param>
/// <return>Returns the vertical position of the button</return>
function InventoryGridCtrl::getButtonPosition(%this, %index)
{
    if (%index < 0)
        %index = 0;

    if (%index > %this.contentPane.getCount())
        %index = %this.contentPane.getCount();
    %itemCount = %this.contentPane.getCount();
    %spacing = %this.contentPane.rowSpacing;
    %maxScroll = (%this.buttonHeight * %itemCount) + (%itemCount * %spacing) - %this.buttonHeight;

    %position = %index * (%spacing + %this.buttonHeight);
    if (%position > %maxScroll)
        %position = %maxScroll;
    return %position;
}

/// <summary>
/// This returns the button at the desired index.
/// </summary>
/// <param name="index">The index of the desired button</param>
/// <return>Returns the button at the specified index</return>
function InventoryGridCtrl::getButton(%this, %index)
{
    if ( %index < 0 )
        %index = 0;

    %count = %this.contentPane.getCount();

    if (%index > %count )
        %index = %count;

    return %this.contentPane.getObject(%index);
}

/// <summary>
/// This function adds a header to the container.  The header can be any collection of gui controls
/// contained within a parent control.
/// </summary>
/// <param name="guiControl">The GUI control to create the header from</param>
function InventoryGridCtrl::addHeader(%this, %guiControl)
{
    // take the provided gui control and place it on a 
    // IGCDynamicButton in IGCContentPane and assign it an index.
    if (%this.headerWidth $= "")
        %this.resizeCtrl = true;
    %this.headerCtrl = %guiControl;

    %this.headerWidth = getWord(%guiControl.extent, 0);
    %this.headerHeight = getWord(%guiControl.extent, 1);

    %this.addGuiControl(%guiControl);

    if (%this.resizeCtrl)
    {
        %this.resizeCtrl = false;
        %this.resizeContainer();
    }
}

/// <summary>
/// This function adds a footer to the container.  The footer can be any collection of gui controls
/// contained within a parent control.
/// </summary>
/// <param name="guiControl">The GUI control to create the footer from</param>
function InventoryGridCtrl::addFooter(%this, %guiControl)
{
    // take the provided gui control and place it on a 
    // IGCDynamicButton in IGCContentPane and assign it an index.
    if (%this.footerWidth $= "")
        %this.resizeCtrl = true;
    %this.footerCtrl = %guiControl;

    %this.footerWidth = getWord(%guiControl.extent, 0);
    %this.footerHeight = getWord(%guiControl.extent, 1);

    %this.addGuiControl(%guiControl);

    if (%this.resizeCtrl)
    {
        %this.resizeCtrl = false;
        %this.resizeContainer();
    }

    %parent = %this.getParent();
    %parentHeight = %parent.Extent.y - 13;
    %this.footerCtrl.setPosition(0, %parentHeight - %this.footerHeight - 18);
}

/// <summary>
/// This function adds a button to the container with a target object and method to 
/// call, along with other data potentially.  The container will be sized to the width 
/// of the assigned button's parent control unless the container has been assigned a
/// header.
/// </summary>
/// <param name="guiControl">The GUI control to create the button from</param>
/// <param name="object">The object that contains the method to be called</param>
/// <param name="handler">The method on %object to call to handle the button click</param>
/// <param name="data">Additional information that needs to be passed on to %handler</param>
function InventoryGridCtrl::addButton(%this, %guiControl, %object, %handler, %data)
{
    // take the provided gui control and place it on a 
    // IGCDynamicButton in IGCContentPane and assign it an index.
    if (%this.buttonWidth $= "")
        %this.resizeCtrl = true;

    %this.buttonWidth = %guiControl.Extent.x;
    %this.buttonHeight = %guiControl.Extent.y;

    if ( %this.buttonProfile $= "" && %guiControl.Profile !$= "GuiDefaultProfile" )
    {
        %this.buttonProfile = %guiControl.Profile;
        %this.useProfile = true;
    }
    else
        %this.useProfile = false;

    if ( %this.buttonProfile $= "" )
        %this.buttonProfile = "GuiTransparentProfile";

    %baseProfile = (%this.useProfile == true ? %this.buttonProfile : %guiControl.Profile);
    if ( %baseProfile $= "" )
        %baseProfile = "GuiTransparentProfile";

    %extent = %guiControl.Extent;
	%button = new GuiControl()
	{
		canSaveDynamicFields="0";
		isContainer="0";
		Profile=%baseProfile;
        HorizSizing="left";
        VertSizing="top";
		Position="0 0";
		Extent=%extent;
		MinExtent="8 2";
		canSave="1";
		Visible="1";
		hovertime="1000";
            index = %this.contentPane.getCount();
    };
    
    %toolTipProfile = (%guiControl.tooltipProfile !$= "" ? %guiControl.tooltipProfile : "GuiToolTipProfile");

	%clickEvent = new GuiMouseEventCtrl()
	{
	    class="IGCDynamicButton";
		canSaveDynamicFields="0";
		isContainer="0";
		Profile="GuiTransparentProfile";
        HorizSizing="left";
        VertSizing="top";
		Position="0 0";
		Extent=%extent;
		MinExtent="8 2";
		canSave="1";
		Visible="1";
		hovertime="1000";
        toolTipProfile=%toolTipProfile;
        toolTip=%guiControl.toolTip;
		groupNum="-1";
            index = %this.contentPane.getCount();
            object = %object;
            handler = %handler;
            data = %data;
            button = %button;
            container = %this;
	};

	// If the incoming gui control set has any buttons in it, assume that we want
	// to be able to click them.  Sort through the control set and hold buttons out
	// until the mouse event control has been added, then add the other child buttons.
	// Additionally, hold out pretty much any control that we might want to be able to
	// interact with, such as edit boxes or dropdown menus.
	%itemCount = %guiControl.getCount();
	%k = 0;
	while (%itemCount > 0)
	{
	    %temp = %guiControl.getObject(0);
	    if (isObject(%temp))
	    {
            %type = %temp.getClassName();
            if (%type $= "guiButtonBaseCtrl" || %type $= "guiBitmapButtonCtrl" || 
            %type $= "guiBitmapButtonTextCtrl" || %type $= "guiBorderButtonCtrl" ||
            %type $= "guiPopUpMenuCtrl" || %type $= "guiPopUpMenuCtrlEx" ||
            %type $= "guiTextEditCtrl" || %type $= "guiTextEditSliderCtrl" ||
            %type $= "guiSliderCtrl" || %type $= "guiRadioCtrl" || %type $= "guiCheckBoxCtrl" ||
            %type $= "guiIconButtonCtrl" || %type $= "guiImageButtonCtrl")
            {
                %buttons[%k] =  %temp;
                %guiControl.remove(%temp);
                %k++;
            }
            else
                %button.addGuiControl(%temp);
	    }
	    %itemCount=%guiControl.getCount();
	}
    %button.addGuiControl(%clickEvent);
	if (%k > 0)
	{
	    for (%i = 0; %i < %k; %i++)
	    {
	        %button.addGuiControl(%buttons[%i]);
	    }
	}

    %this.contentPane.add(%button);

    if (%this.batch)
        return;

    if (%this.resizeCtrl)
    {
        %this.resizeCtrl = false;
        %this.resizeContainer();
    }
}

function InventoryGridCtrl::onAdd(%this)
{
    if (isObject(%this.getParent))
    {
        %this.resizeContainer();
        %this.resizeContentPane();
        %this.resizeCtrl = false;
    }
}

function InventoryGridCtrl::onSleep(%this)
{
    if (isObject(%this.indicatorArrow))
    {
        %this.indicatorArrow.delete();
    }
}

function InventoryGridCtrl::onRemove(%this)
{
    if (isObject(%this.indicatorArrow))
    {
        %this.indicatorArrow.delete();
    }
}

/// <summary>
/// This function sets the container to add buttons without resizing the control.
/// </summary>
/// <param name="flag">The desired container batch add state</param>
function InventoryGridCtrl::toggleBatch(%this, %flag)
{
    %this.batch = %flag;
    if ( %this.scrollCallbacks && %flag )
        %this.scrollCtrl.setUseScrollEvents(false);
    if ( %this.scrollCallbacks && !%flag )
        %this.scrollCtrl.setUseScrollEvents(%this.scrollCallbacks);

    if (!%flag)
    {
        %this.resizeContentPane();

        if (%this.contentPane.Extent.y > %this.scrollCtrl.Extent.y)
        {
            %this.upButton.button.setActive(true);
            %this.downButton.button.setActive(true);
        }
        else
        {
            %this.upButton.button.setActive(false);
            %this.downButton.button.setActive(false);
        }
    }
}

function InventoryGridCtrl::resizeContainer(%this)
{
    %parent = %this.parentCtrl;
    %parentWidth = %parent.Extent.x - 5;
    %parentHeight = %parent.Extent.y - 14;
    %buttonWidth = %this.buttonWidth;
    %buttonHeight = %this.buttonHeight;

    %this.resize(0, 0, %parentWidth, %parentHeight);

    if (isObject(%this.headerCtrl))
        %this.headerCtrl.setPosition(0, 18);
    
    if (isObject(%this.footerCtrl))
        %this.footerCtrl.setPosition(0, %parentHeight - %this.footerHeight - 18);

    %contentContainerWidth = %this.Extent.x - 32;
    %contentContainerPosX = %this.Position.x + 18;
    %contentContainerHeight = %this.Extent.y - %this.headerHeight - %this.footerHeight - 32;
    %contentContainerPosY = %this.Position.y + 18 + %this.headerHeight;

    %this.contentContainer.resize(%contentContainerPosX, %contentContainerPosY, %contentContainerWidth, %contentContainerHeight);
}

function InventoryGridCtrl::resizeContentPane(%this)
{
    %count = %this.contentPane.getCount();
    %spacing = %this.contentPane.rowSpacing;
    %height = (%count * %this.buttonHeight) + (%count * %spacing);

    %position = %this.contentContainer.Position;
    %this.contentPane.resize(0, 0, %this.contentContainer.Extent.x, %height);
    %this.contentPane.rowSize = %this.buttonHeight;
    %this.contentPane.colSize = %this.contentContainer.Extent.x;

    %this.paneSize = getWord(%this.contentContainer.Extent, 1);
    %this.itemCount = %this.contentPane.getCount();    
    
    %this.resizeContainer();
}

/// <summary>
/// This sets the spacing between buttons in the container
/// </summary>
/// <param name="spacing">The desired space in pixels between buttons - "x y"</param>
function InventoryGridCtrl::setSpacing(%this, %spacing)
{
    %this.contentPane.rowSpacing = %spacing.x;
    %this.contentPane.colSpacing = %spacing.y;
}

/// <summary>
/// This sets the background image to use for the container "cells"
/// </summary>
/// <param name="image">The image asset to use for the background</param>
function InventoryGridCtrl::setCellBackground(%this, %image)
{
    %this.backgroundImage = %image;
    %asset = AssetDatabase.acquireAsset(%image);
    %size = %asset.getImageSize();
    AssetDatabase.releaseAsset(%asset);
    %this.contentPane.colSize = %size.x;
    %this.contentPane.rowSize = %size.y;
    %panelWidth = %this.Extent.x - 32;
    %panelHeight = %this.Extent.y - %this.headerHeight - %this.footerHeight - 32;
    %colCount = mFloor(%panelWidth / (%size.x + %this.contentPane.colSpacing));
    %rowCount = mFloor(%panelHeight/ (%size.y + %this.contentPane.rowSpacing));
    %contentWidth = %colCount * (%size.x + %this.contentPane.colSpacing);
    %contentHeight = %rowCount * (%size.y + %this.contentPane.rowSpacing);
    %position = ((%panelWidth - %contentWidth) / 2) SPC ((%panelHeight - %contentHeight) / 2);
    %position.x += 18;
    %position.y += 18;
    %this.contentPane.setPosition(%position.x, %position.y);
    %this.contentPane.Extent = %panelWidth SPC %panelHeight;
    %this.contentPane.colCount = %colCount;
    %this.contentPane.rowCount = %rowCount;
    %this.colCount = %colCount;
    %this.rowCount = %rowCount;
    for ( %i = 0; %i < %colCount; %i++)
    {
        for ( %j = 0; %j < %rowCount; %j++)
        {
            %x = (%i * %size.x) + (%i * %this.contentPane.colSpacing);
            %y = (%j * %size.y) + (%j * %this.contentPane.rowSpacing);
            %name="x"@%i@"y"@%j;
            %sprite = new GuiSpriteCtrl(%name)
            {
                Profile="InventoryDefaultProfile";
                class="inventoryCell";
                HorizSizing="right";
                VertSizing="bottom";
                Extent=%size;
                MinExtent=%size;
                Position=%x SPC %y;
                Visible="1";
                Image = %image;
            };
            %this.contentPane.add(%sprite);
        }
    }
    //%this.resizeContainer();
}

/// <summary>
/// This sets a profile to use for highlighting the currently selected button.
/// </summary>
/// <param name="profile">The profile to use for the selected button.</param>
function InventoryGridCtrl::setHighlightProfile(%this, %profile)
{
    %this.highlightProfile = %profile;
}

/// <summary>
/// This sets an asset ID for the selected item scroll position indicator.
/// </summary>
/// <param name="profile">The asset ID to use for the indicator arrow.</param>
function InventoryGridCtrl::setIndicatorImage(%this, %assetID)
{
    %this.indicatorImage = %assetID;
}

/// <summary>
/// This sets a base profile for all buttons.  If this is not set, the first button
/// added to the container sets the base button profile.
/// </summary>
/// <param name="profile">The profile to use for the selected button</param>
function InventoryGridCtrl::setNormalProfile(%this, %profile)
{
    %this.buttonProfile = %profile;
}

/// <summary>
/// This gets the current container button spacing
/// </summary>
/// <return>Returns the current button spacing</return>
function InventoryGridCtrl::getSpacing(%this)
{
    if (%this.contentPane.rowSpacing !$= "")
        return %this.contentPane.rowSpacing;
    else
        return 0;
}

/// <summary>
/// This "clicks" the desired contained button.
/// </summary>
/// <param name="index">The index of the desired button</param>
function InventoryGridCtrl::setSelected(%this, %index)
{
    if (%index < 0 || %index > %this.contentPane.getCount())
        return;

    %control = %this.contentPane.getObject(%index);
    if (isObject(%control))
    {
        for (%i = 0; %i < %control.getCount(); %i++)
        {
            %obj = %control.getObject(%i);
            %type = %obj.getClassName();
            if (%type $= "GuiMouseEventCtrl")
            {
                %obj.onMouseUp();
                break;
            }
        }
    }
}

/// <summary>
/// This gets the number of buttons in the container.
/// </summary>
/// <return>Returns the number of buttons in the content pane.</return>
function InventoryGridCtrl::getCount(%this)
{
    return %this.contentPane.getCount();
}

/// <summary>
/// This handles button clicks for contained buttons.  It checks to ensure that 
/// the object has the assigned method, then calls that method with the assigned
/// data.
/// </summary>
function IGCDynamicButton::onMouseUp(%this)
{
    if ( %this.container.selectedButton !$= "" && isObject(%this.container.selectedButton) )
        %this.container.selectedButton.setProfile(%this.container.buttonProfile);
    %this.container.selectedButton = %this.button;
    %this.button.setProfile(%this.container.highlightProfile);
    %this.container.updateIndicatorPosition();
    %object = %this.object;
    if (%object.isMethod(%this.handler))
        %object.call(%this.handler, %this.data);
}

function IGCDynamicButton::onMouseDown(%this, %id, %mousePoint)
{
    echo(" @@@ IGCDynamicButton::onMouseDown(" @ %this @ ", " @ %id @ ", " @ %mousePoint @ ")");
    %position = %mousePoint;
    %halfParentWidth = %this.sprite.Extent.x / 2;
    %halfParentHeight = %this.sprite.Extent.y / 2;
    %position.x -= %halfParentWidth;
    %position.y -= %halfParentHeight;
    Inventory.createDraggingControl(%this.sprite, %position, %mousePoint, %this.sprite.Extent);
}

function IGCDynamicButton::onMouseDragged(%this, %modifier, %mousePoint, %mouseClickCount)
{
    echo(" @@@ IGCDynamicButton::onMouseDragged(" @ %this @ ", " @ %modifier @ ", " @ %mousePoint @ ", " @ %mouseClickCount @ ")");
    //if (!%this.getParent().isActive())
        //return;

    %position = %mousePoint;
    %halfParentWidth = %this.sprite.Extent.x / 2;
    %halfParentHeight = %this.sprite.Extent.y / 2;
    %position.x -= %halfParentWidth;
    %position.y -= %halfParentHeight;

    Inventory.createDraggingControl(%this.sprite, %position, %mousePoint, %this.sprite.Extent);

    //%count = %this.parentCell.getCount();
    %oldObj = %this.parentCell.getObject(0);
    %this.parentCell.remove(%oldObj);
    InventoryEventManager.postEvent("_ItemDeleteRequest", %oldObj);
}

function inventoryCell::onControlDropped(%this, %control, %position)
{
    echo(" @@@ dropped in inventory at " @ %this.getName());
    %objCount = %this.getCount();
    if (%objCount > 0)
    {
        if (%control.parentCell !$= "")
        {
            echo(" @@@ Control:parentCell: " @ %control.parentCell);
            %control.parentCell.onControlDropped(%control, %position);
            return;
        }
        // find a nearby empty cell and drop there instead.
        %location = %this.getName();
        %position = strreplace(%location, "y", " ");
        %position = strreplace(%position, "x", "");
        %x = getWord(%position, 0);
        %y = getWord(%position, 1);
        if (Inventory.inventoryContainer.fillFromTopLeft)
        {
            for (%i = 0; %i < Inventory.inventoryContainer.rowCount; %i++)
            {
                for (%j = 0; %j < Inventory.inventoryContainer.colCount; %j++)
                {
                    %cell = "x" @ %j @ "y" @ %i;
                    if (!%cell.getCount())
                    {
                        %cell.onControlDropped(%control, %position);
                        return;
                    }
                }
            }
        }
        else
        {
            for (%i = %y; %i < Inventory.inventoryContainer.rowCount; %i++)
            {
                for (%j = %x; %j < Inventory.inventoryContainer.colCount; %j++)
                {
                    %cell = "x" @ %j @ "y" @ %i;
                    if (!%cell.getCount())
                    {
                        %cell.onControlDropped(%control, %position);
                        return;
                    }
                }
            }
        }
    }

    
    %posx = (%this.Extent.x - %control.Extent.x) / 2;
    %posy = (%this.Extent.y - %control.Extent.y) / 2;

	%clickEvent = new GuiMouseEventCtrl()
	{
	    class="IGCDynamicButton";
		canSaveDynamicFields="0";
		isContainer="0";
		Profile="GuiTransparentProfile";
        HorizSizing="left";
        VertSizing="top";
		Position="0 0";
		Extent=%extent;
		MinExtent="8 2";
		canSave="1";
		Visible="1";
		hovertime="1000";
        toolTipProfile=%toolTipProfile;
        toolTip=%guiControl.toolTip;
		groupNum="-1";
	};
    %clickEvent.parentCell = %this;
    %sprite = new GuiSpriteCtrl()
    {
        Extent = %control.Extent;
        Image = %control.Image;
    };
    %clickEvent.addGuiControl(%sprite);
    %sprite.Position = %posx SPC %posy;
    %this.addGuiControl(%clickEvent);
    %clickEvent.sprite = %sprite;
    %clickEvent.parentCell = %this;
    %sprite.parentCell = %this;
    echo(" @@@ Control: " @ %control @ " : class: " @ %control.getClassName());
}

/// <summary>
/// This "factory" function creates a vertical scroll container and returns a 
/// reference to it.  Use the returned reference to work with the container.
/// </summary>
/// <param name="profile">Profile for the scroll container.  Default is GuiSunkenContainerProfile</param>
/// <param name="page">Flag to set if the container can have multiple pages.</param>
/// <return>A new InventoryGridCtrl container object</return>
function createInventoryGridContainer(%requestor, %profile, %page)
{
    %container = new GuiControl()
    {
        class="InventoryGridCtrl";
        canSaveDynamicFields="0";
        isContainer="1";
        Profile="GuiTransparentProfile";
		HorizSizing="left";
		VertSizing="top";
        Position="0 0";
        Extent="160 558";
        MinExtent="8 2";
        canSave="1";
        Visible="1";
        hovertime="1000";
            parentCtrl=%requestor;
    };

    %containerProfile = (%profile !$= "" ? %profile : "GuiSunkenContainerProfile");
    if ( %containerProfile $= "" )
        %containerProfile = "GuiTransparentProfile";

    %pageContainer = new GuiControl()
    {
        canSaveDynamicFields="0";
        isContainer="1";
        Profile=%containerProfile;
		HorizSizing="left";
		VertSizing="top";
        Position="0 0";
        Extent="160 558";
        MinExtent="8 2";
        canSave="1";
        Visible="1";
        hovertime="1000";
    };
    %container.addGuiControl(%pageContainer);
    %container.contentContainer = %pageContainer;

    %contentPane = new GuiControl()
    {
        class="IGCContent";
        canSaveDynamicFields="0";
        isContainer="1";
        Profile="GuiTransparentProfile";
		HorizSizing="left";
		VertSizing="top";
        Position="1 1";
        Extent="140 442";
        MinExtent="8 2";
        canSave="1";
        Visible="1";
        hovertime="1000";
            colCount="1";
            colSize="1024";
            rowSize="106";
            rowSpacing="0";
            colSpacing="0";
    };
    %container.addGuiControl(%contentPane);
    %container.contentPane = %contentPane;

    %container.resizeContainer = true;

    return %container;
}
