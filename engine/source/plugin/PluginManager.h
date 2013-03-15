#ifndef _PLUGIN_MANAGER_H_
#define _PLUGIN_MANAGER_H_

#include "platform/platformFileIO.h"
#include "platformWin32/platformWin32.h"
#include "collection/vector.h"
#include "console/console.h"

#include "string/stringBuffer.h"

#pragma once

class IPlugin;

class PluginManager
{
public:
    static PluginManager & GetInstance(void);

    void LoadPlugins ( const StringTableEntry & strDir );
    void UnloadAll ( void );

    int GetNumPlugins ( void ) const;
    IPlugin * GetPlugin ( int nIndex );

private:
    PluginManager();
    void GetFilenames ( const StringTableEntry & dir, 
                        Vector<StringTableEntry> & filenames ) const;
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