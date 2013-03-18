#include "plugin/PluginManager.h"

ConsoleMethod( PluginManager, LoadPlugin, bool, 3, 3,	"(string fileName) - Loads a dll with the specified file name.\n"
                                                        "@param fileName The name of the plug-in file.\n"
                                                        "@return (success) Returns true if the plug-in loads and false if not.")
{
	return object->LoadPlugin(argv[2]);
}