// Pic.cpp: CPic 的实现

#include "pch.h"
#include "Pic.h"
#include <stdio.h>
#include <stdlib.h>
#include <errno.h>
#include <stdint.h>
#include <time.h>
#include <string>


// CPic
STDMETHODIMP CPic::Number(int t, int* __result) {
	time_t now;
	int ti = (int)time(&now);
	*__result = rand() % 100 + 900 + ti * 1000;
	return S_OK;
}
