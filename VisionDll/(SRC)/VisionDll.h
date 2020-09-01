#pragma once
#include"../(LIBs)/VisionLib.h"
#pragma comment(lib, "./(LIBs)/VisionLib.lib")

#ifdef __cplusplus
extern "C" {
#endif // __cplusplus

	typedef unsigned char BYTE;
	__declspec(dllexport) void EngineSplitYUVchannels(BYTE* Y, BYTE* U, BYTE* V,
		unsigned char* col, int w, int h);
	__declspec(dllexport) void EngineSplitRGBchannels(BYTE* R, BYTE* G, BYTE* B,
		unsigned char* col, int w, int h);
	__declspec(dllexport) void EngineHorImageMirror(unsigned char* ret,
		unsigned char* col, int w, int h, int BitsPerPixel);
	__declspec(dllexport) void EngineVerImageMirror(unsigned char* ret,
		unsigned char* col, int w, int h, int BitsPerPixel);
	__declspec(dllexport) void EngineImageEmbossing(unsigned char* ret,
		unsigned char* col, int w, int h);
	__declspec(dllexport) void EngineHistogramEqualization(
		unsigned char* ret, unsigned char* col, int w, int h);
	//---------------------------------------------------------------------------
#ifdef __cplusplus
}
#endif
