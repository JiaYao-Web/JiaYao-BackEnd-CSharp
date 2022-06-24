// dllmain.cpp : 此文件用于定义 DLL 应用程序的入口点。
#include "pch.h"
#include <time.h>

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

extern "C" _declspec(dllexport) char getPrefix(int code)
{
    char prefix[] = { 'U', 'M', 'I', 'T'};
    return prefix[code];
}
