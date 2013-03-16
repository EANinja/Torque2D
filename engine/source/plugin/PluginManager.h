#ifndef _PLUGIN_MANAGER_H_
#define _PLUGIN_MANAGER_H_

#pragma once

#include "platform/platformFileIO.h"
#include "platformWin32/platformWin32.h"
#include "collection/vector.h"
#include "console/console.h"

#include "string/stringBuffer.h"

class IPlugin;

class PluginManager
{
public:
    static PluginManager & GetInstance(void);

    void UnloadAll ( void );

    int GetNumPlugins ( void ) const;
    IPlugin * GetPlugin ( int nIndex );

private:
    PluginManager();
    bool LoadPlugin ( const StringTableEntry & filename );


    struct PluginInfo
    {
        IPlugin * pPlugin;
        HMODULE   hDll;
    };

    Vector<PluginInfo> m_plugins;

    static PluginManager * s_pInstance;
};
#endif // _PLUGIN_MANAGER_H_