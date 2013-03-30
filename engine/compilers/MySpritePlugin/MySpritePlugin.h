#ifndef _MYSPRITE_H_
#define _MYSPRITE_H_

#define PLUGIN_EXPORTS
#include "plugin.h"

#ifndef _SPRITE_BASE_H_
#include "../../source/2d/sceneobject/Sprite.h"
#endif

#ifndef _DGL_H_
#include "../../source/graphics/dgl.h"
#endif

#ifndef _STRINGBUFFER_H_
#include "../../source/string/stringBuffer.h"
#endif

#include "../../source/2d/scene/SceneRenderState.h"
#include "../../source/2d/scene/SceneRenderRequest.h"
#include "../../source/2d/core/BatchRender.h"

//------------------------------------------------------------------------------

extern "C" class PLUGINDECL MySprite : public Sprite
{
    typedef Sprite Parent;

private:
    /// Render flipping.
    bool mFlipX;
    bool mFlipY;

public:
    MySprite();
    virtual ~MySprite();

    static void initPersistFields();
    virtual void copyTo(SimObject* object);

    /// Render flipping.
    void setFlip( const bool flipX, const bool flipY );
    void setFlipX( const bool flipX );
    void setFlipY( const bool flipY );
    bool getFlipX( void ) const;
    bool getFlipY( void ) const;

    virtual void sceneRender( const SceneRenderState* pSceneRenderState, const SceneRenderRequest* pSceneRenderRequest, BatchRender* pBatchRenderer );

    /// Declare Console Object.
    DECLARE_CONOBJECT( MySprite );

protected:
    static bool writeFlipX( void* obj, StringTableEntry pFieldName );
    static bool writeFlipY( void* obj, StringTableEntry pFieldName );
};

#endif // _SPRITE_H_

