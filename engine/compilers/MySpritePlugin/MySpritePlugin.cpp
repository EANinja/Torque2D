// MySpritePlugin.cpp : Defines the exported functions for the DLL application.
//
#include "../../source/platform/platformFileIO.h"
#include "../../source/platformWin32/platformWin32.h"
#include "../../source/collection/vector.h"
#include "../../source/console/console.h"
#include "../../source/console/consoleTypes.h"

#include "../../source/string/stringBuffer.h"

//#include "stdafx.h"


#ifndef _SPRITE_H_
#include "MySpritePlugin.h"
#endif

// Script bindings.
#include "MySprite_ScriptBinding.h"

//------------------------------------------------------------------------------

IMPLEMENT_CONOBJECT(MySprite);

//------------------------------------------------------------------------------

MySprite::MySprite() :
    mFlipX(false),
    mFlipY(false)
{
}

//------------------------------------------------------------------------------

MySprite::~MySprite()
{
}

//------------------------------------------------------------------------------

void MySprite::copyTo(SimObject* object)
{
    // Call to parent.
    Parent::copyTo(object);

    // Cast to sprite.
    MySprite* pSprite = static_cast<MySprite*>(object);

    // Sanity!
    AssertFatal(pSprite != NULL, "Sprite::copyTo() - Object is not the correct type.");

    /// Render flipping.
    pSprite->setFlip( getFlipX(), getFlipY() );
}

//------------------------------------------------------------------------------

void MySprite::initPersistFields()
{
    // Call parent.
    Parent::initPersistFields();

    /// Render flipping.
    addField("FlipX", TypeBool, Offset(mFlipX, MySprite), &writeFlipX, "");
    addField("FlipY", TypeBool, Offset(mFlipY, MySprite), &writeFlipY, "");
}

//------------------------------------------------------------------------------

void MySprite::sceneRender( const SceneRenderState* pSceneRenderState, const SceneRenderRequest* pSceneRenderRequest, BatchRender* pBatchRenderer )
{
    // Let the parent render.
    SpriteProxyBase::render(
        getFlipX(), getFlipY(),
        mRenderOOBB[0],
        mRenderOOBB[1],
        mRenderOOBB[2],
        mRenderOOBB[3],
        pBatchRenderer );
}

void MySprite::setFlip( const bool flipX, const bool flipY )  { mFlipX = flipX; mFlipY = flipY; }
void MySprite::setFlipX( const bool flipX )                   { setFlip( flipX, mFlipY ); }
void MySprite::setFlipY( const bool flipY )                   { setFlip( mFlipX, flipY ); }
bool MySprite::getFlipX( void ) const                  { return mFlipX; }
bool MySprite::getFlipY( void ) const                  { return mFlipY; }
bool MySprite::writeFlipX( void* obj, StringTableEntry pFieldName )        { return static_cast<MySprite*>(obj)->getFlipX() == true; }
bool MySprite::writeFlipY( void* obj, StringTableEntry pFieldName )        { return static_cast<MySprite*>(obj)->getFlipY() == true; }
