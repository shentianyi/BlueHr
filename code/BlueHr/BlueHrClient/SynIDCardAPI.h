// SynIDCardAPI.h : SynIDCardAPI DLL ����ͷ�ļ�
//

#pragma once

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// ������


// CSynIDCardAPIApp
// �йش���ʵ�ֵ���Ϣ������� SynIDCardAPI.cpp
//

class CSynIDCardAPIApp : public CWinApp
{
public:
	CSynIDCardAPIApp();

// ��д
public:
	virtual BOOL InitInstance();

	DECLARE_MESSAGE_MAP()
};
