// dllmain.h: 模块类的声明。

class CLMSCOMModule : public ATL::CAtlDllModuleT< CLMSCOMModule >
{
public :
	DECLARE_LIBID(LIBID_LMSCOMLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_LMSCOM, "{7aaac6ce-eb62-407e-93fd-d5c073bd7fc0}")
};

extern class CLMSCOMModule _AtlModule;
