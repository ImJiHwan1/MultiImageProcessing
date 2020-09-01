#pragma once
typedef unsigned char BYTE;

template<typename T>
inline T limit(const T& value)
{
	return((value > 255) ? 255 : ((value < 0) ? 0 : value));
}
void SplitRGB_TO_YUV_channel(BYTE* Y, BYTE* U, BYTE* V,
	BYTE* col, int w, int h);
void SplitYUV_TO_RGB_channel(BYTE* R, BYTE* G, BYTE* B,
	BYTE* col, int w, int h);
unsigned char* GrayImage(unsigned char* col, int w, int h);
void Histogram(int histo[256], unsigned char* gry, int w, int h);
void HistogramEqualization(unsigned char* ret, unsigned char* col, int w, int h);
