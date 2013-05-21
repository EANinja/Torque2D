
function InventoryFrameControl::onMouseDown(%this, %modifier, %mousePoint, %mouseClickCount)
{
    if (%mouseClickCount == 2)
        AnimationBuilder.appendFrame(%this.getParent().Frame);
    else
    {
        %position = %mousePoint;
        %halfParentWidth = %this.sprite.getExtent().x / 2;
        %halfParentHeight = %this.sprite.getExtent().y / 2;
        %position.x -= %halfParentWidth;
        %position.y -= %halfParentHeight;
        Inventory.createDraggingControl(%this.sprite, %position, %mousePoint, %this.sprite.Extent);
    }
}

function InventoryFrameControl::onTouchDragged(%this, %modifier, %mousePoint, %mouseClickCount)
{
    if (!%this.getParent().isActive())
        return;

    %position = %mousePoint;
    %halfParentWidth = %this.sprite.getExtent().x / 2;
    %halfParentHeight = %this.sprite.getExtent().y / 2;
    %position.x -= %halfParentWidth;
    %position.y -= %halfParentHeight;
    Inventory.createDraggingControl(%this.getParent(), %position, %mousePoint, %this.Extent);
}