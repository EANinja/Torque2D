ConsoleMethod(MySprite, setFlip, void, 4, 4,  "(bool flipX, bool flipY) Sets the sprite texture flipping for each axis.\n"
                                                "@param flipX Whether or not to flip the texture along the x (horizontal) axis.\n"
                                                "@param flipY Whether or not to flip the texture along the y (vertical) axis.\n"
                                                "@return No return value.")
{
    // Set Flip.
    object->setFlip( dAtob(argv[2]), dAtob(argv[3]) );
}

//-----------------------------------------------------------------------------

ConsoleMethod(MySprite, getFlip, const char*, 2, 2,   "() Gets the flip for each axis.\n"
                                                        "@return (bool flipX/bool flipY) Whether or not the texture is flipped along the x and y axis.")
{
    // Create Returnable Buffer.
    char* pBuffer = Con::getReturnBuffer(32);

    // Format Buffer.
    dSprintf(pBuffer, 32, "%d %d", object->getFlipX(), object->getFlipY());

    // Return Buffer.
    return pBuffer;
}

//-----------------------------------------------------------------------------

ConsoleMethod(MySprite, setFlipX, void, 3, 3,     "(bool flipX) Sets whether or not the texture is flipped horizontally.\n"
                                                    "@param flipX Whether or not to flip the texture along the x (horizontal) axis."
                                                    "@return No return value.")
{
    // Set Flip.
    object->setFlipX( dAtob(argv[2]) );
}

//-----------------------------------------------------------------------------

ConsoleMethod(MySprite, setFlipY, void, 3, 3,     "(bool flipY) Sets whether or not the texture is flipped vertically.\n"
                                                    "@param flipY Whether or not to flip the texture along the y (vertical) axis."
                                                    "@return No return value.")
{
    // Set Flip.
    object->setFlipY( dAtob(argv[2]) );
}

//-----------------------------------------------------------------------------

ConsoleMethod(MySprite, getFlipX, bool, 2, 2,     "() Gets whether or not the texture is flipped horizontally.\n"
                                                    "@return (bool flipX) Whether or not the texture is flipped along the x axis.")
{
   return object->getFlipX();
}

//-----------------------------------------------------------------------------

ConsoleMethod(MySprite, getFlipY, bool, 2, 2,     "() Gets whether or not the texture is flipped vertically."
                                                    "@return (bool flipY) Whether or not the texture is flipped along the y axis.")
{
   return object->getFlipY();
}