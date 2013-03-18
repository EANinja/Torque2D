#ifndef _PLUGIN_MANAGER_H_
#define _PLUGIN_MANAGER_H_

#pragma once

#include "platform/platformFileIO.h"
#include "platformWin32/platformWin32.h"
#include "collection/vector.h"
#include "console/console.h"
#include "console/consoleTypes.h"

#include "string/stringBuffer.h"

class IPlugin;

class PluginManager : public SimObject
{
	typedef SimObject Parent;
public:
    PluginManager();

	static PluginManager & GetInstance(void);

    void UnloadAll ( void );

    int GetNumPlugins ( void ) const;
    IPlugin * GetPlugin ( int nIndex );
    bool LoadPlugin ( const StringTableEntry & filename );

	/// Engine.
    static void             initPersistFields();

    /// Declare Console Object.
    DECLARE_CONOBJECT( PluginManager );
private:

    struct PluginInfo
    {
        IPlugin * pPlugin;
        HMODULE   hDll;
    };

    Vector<PluginInfo> m_plugins;

    static PluginManager * s_pInstance;
};

extern PluginManager PluginDatabase;

#endif // _PLUGIN_MANAGER_H_