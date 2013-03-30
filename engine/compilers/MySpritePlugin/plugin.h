#ifndef IPLUGIN_H_
#define IPLUGIN_H_

#include "../../source/platform/platformFileIO.h"
#include "../../source/platformWin32/platformWin32.h"
#include "../../source/collection/vector.h"
#include "../../source/console/console.h"

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
};


typedef IPlugin * (* CREATEPLUGIN)(PluginManager & mgr);

extern "C" PLUGINDECL IPlugin * CreatePlugin(PluginManager & mgr);


#endif  // IPLUGIN_H_
