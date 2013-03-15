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

void PluginManager::LoadPlugins ( const StringTableEntry & strDir )
{
    Vector<StringTableEntry> filenames;
    GetFilenames(strDir, filenames);

	Vector<StringTableEntry>::const_iterator it;
    for (it = filenames.begin(); it != filenames.end(); ++it)
    {
        const StringTableEntry & filename = *it;
        StringBuffer & fullName = strDir;
		fullName.append("\\");
		fullName.append(filename);
		StringTableEntry tempName = fullName;
        LoadPlugin (fullName);
    }
}

void PluginManager::GetFilenames ( const StringTableEntry & dir, 
                                  Vector<StringTableEntry> & filenames ) const
{
    StringTableEntry mask = dir + "\\*.plug";

    struct _finddata_t fileinfo;
    long handle = ::_findfirst(mask.c_str(), &fileinfo);
    long file   = handle;

	StringTableEntry tempName;
	while (file >= 0)
	{
		tempName = fileinfo.name;
        filenames.push_back(tempName);
        file = ::_findnext(handle, &fileinfo);
	}

    ::_findclose(handle);
}

bool PluginManager::LoadPlugin ( const StringTableEntry & filename )
{
    HMODULE hDll = ::LoadLibrary ((LPTSTR)filename);
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

