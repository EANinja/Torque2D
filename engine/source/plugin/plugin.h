
#ifndef IPLUGIN_H_
#define IPLUGIN_H_

#include "platform/platformFileIO.h"
#include "platformWin32/platformWin32.h"
#include "collection/vector.h"
#include "console/console.h"

#ifdef PLUGIN_EXPORTS
#define PLUGINDECL __declspec(dllexport)
#else
#define PLUGINDECL __declspec(dllimport)
#endif


class PluginManager;


class IPlugin
{
  public:
    virtual ~IPlugin() {};

    virtual bool Initialize() = 0;
    virtual bool Shutdown() = 0;
    virtual void About(HWND hParent) = 0;

	virtual const StringTableEntry & GetName() = 0;
	virtual const StringTableEntry & GetExportName() = 0;

    virtual bool Export( const char * pData ) = 0;
};


typedef IPlugin * (* CREATEPLUGIN)(PluginManager & mgr);

extern "C" PLUGINDECL IPlugin * CreatePlugin(PluginManager & mgr);


#endif  // IPLUGIN_H_
