

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0622 */
/* at Tue Jan 19 11:14:07 2038
 */
/* Compiler settings for LMSCOM.idl:
    Oicf, W1, Zp8, env=Win64 (32b run), target_arch=AMD64 8.01.0622 
    protocol : all , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */



/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 500
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif /* __RPCNDR_H_VERSION__ */

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __LMSCOM_i_h__
#define __LMSCOM_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IPic_FWD_DEFINED__
#define __IPic_FWD_DEFINED__
typedef interface IPic IPic;

#endif 	/* __IPic_FWD_DEFINED__ */


#ifndef __Pic_FWD_DEFINED__
#define __Pic_FWD_DEFINED__

#ifdef __cplusplus
typedef class Pic Pic;
#else
typedef struct Pic Pic;
#endif /* __cplusplus */

#endif 	/* __Pic_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"
#include "shobjidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IPic_INTERFACE_DEFINED__
#define __IPic_INTERFACE_DEFINED__

/* interface IPic */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IPic;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("17d537ee-660f-4cb0-a4a3-61d5fcebba02")
    IPic : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE Number( 
            /* [in] */ int t,
            /* [retval][out] */ int *__result) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IPicVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IPic * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IPic * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IPic * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IPic * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IPic * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IPic * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IPic * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *Number )( 
            IPic * This,
            /* [in] */ int t,
            /* [retval][out] */ int *__result);
        
        END_INTERFACE
    } IPicVtbl;

    interface IPic
    {
        CONST_VTBL struct IPicVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IPic_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IPic_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IPic_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IPic_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IPic_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IPic_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IPic_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IPic_Number(This,t,__result)	\
    ( (This)->lpVtbl -> Number(This,t,__result) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IPic_INTERFACE_DEFINED__ */



#ifndef __LMSCOMLib_LIBRARY_DEFINED__
#define __LMSCOMLib_LIBRARY_DEFINED__

/* library LMSCOMLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_LMSCOMLib;

EXTERN_C const CLSID CLSID_Pic;

#ifdef __cplusplus

class DECLSPEC_UUID("8fd0a715-45ca-4956-9496-f56545be4388")
Pic;
#endif
#endif /* __LMSCOMLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


