#include "PluginManager.h"
#include "Plugin.h"


PluginManager * PluginManager::s_pInstance = NULL;

PluginManager::PluginManager(){}

PluginManager & PluginManager::GetInstance()
{
    if ( s_pInstance == NULL )
        s_pInstance = new PluginManager();

    return *s_pInstance;
}

bool PluginManager::LoadPlugin ( const StringTableEntry & filename )
{
	char* fullPath;
	if (Con::expandPath(fullPath, sizeof(filename), filename))
	{
		HMODULE hDll = ::LoadLibrary ((LPTSTR)fullPath);
		if ( hDll == NULL )
		{
			LPVOID lpMsgBuf;
			::FormatMessage( 
				FORMAT_MESSAGE_ALLOCATE_BUFFER | 
				FORMAT_MESSAGE_FROM_SYSTEM | 
				FORMAT_MESSAGE_IGNORE_INSERTS,
				NULL,
				GetLastError(),
				MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), // Default language
				(LPTSTR) &lpMsgBuf,
				0,
				NULL 
			);
			StringTableEntry msg = static_cast<StringTableEntry>(lpMsgBuf);
		
			Con::errorf(msg);

			LocalFree( lpMsgBuf );
			return false;
		}


		CREATEPLUGIN pFunc = (CREATEPLUGIN)::GetProcAddress (hDll, "CreatePlugin");
		if ( pFunc == NULL )
			return false;


		IPlugin * pPlugin = pFunc(*this);
		if ( pPlugin == NULL )
			return false;


		PluginInfo info;
		info.pPlugin = pPlugin;
		info.hDll    = hDll;

		m_plugins.push_back(info);


		//MainFrame * pMainFrame = static_cast<MainFrame*>(::AfxGetMainWnd());
		//pMainFrame->AddExporter(pPlugin->GetExportName());

		return true;
	}
	return false;
}

void PluginManager::UnloadAll ( void )
{
    //MainFrame * pMainFrame = static_cast<MainFrame*>(::AfxGetMainWnd());

    Vector<PluginInfo>::iterator it;
    for (it=m_plugins.begin(); it!=m_plugins.end(); ++it)
    {
        PluginInfo & info = *it;
            
        //pMainFrame->RemoveExporter(info.pPlugin->GetExportName());

        delete info.pPlugin;
        ::FreeLibrary (info.hDll);
    }

    m_plugins.clear();
}

int PluginManager::GetNumPlugins ( void ) const
{
    return m_plugins.size();
}

IPlugin * PluginManager::GetPlugin ( int nIndex )
{
    if ( nIndex < 0 || nIndex >= GetNumPlugins() )
        return NULL;

    return m_plugins[nIndex].pPlugin;
}

