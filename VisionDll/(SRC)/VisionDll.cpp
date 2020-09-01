#include "VisionDll.h"

extern "C" __declspec(dllexport) void EngineSplitYUVchannels(BYTE * Y, BYTE * U, BYTE * V,
	unsigned char* col, int w, int h)
{
	SplitRGB_TO_YUV_channel(Y, U, V, col, w, h);
}

extern "C" __declspec(dllexport) void EngineSplitRGBchannels(BYTE * R, BYTE * G, BYTE * B,
	unsigned char* col, int w, int h)
{
	SplitYUV_TO_RGB_channel(R, G, B, col, w, h);
}

extern "C" __declspec(dllexport) void EngineHorImageMirror(unsigned char* ret,
	unsigned char* col, int w, int h, int BitsPerPixel)
{
	if (BitsPerPixel != 24) return;
	// Horizonatal Mirroring------------------------------------------------------
	for (int y = 0; y < h; y++) {
		for (int x = 0; x < w; x++) {
			ret[(3 * w) * (y)+3 * (w - x - 1) + 0] = col[(3 * w) * (y)+(3 * x) + 0];
			ret[(3 * w) * (y)+3 * (w - x - 1) + 1] = col[(3 * w) * (y)+(3 * x) + 1];
			ret[(3 * w) * (y)+3 * (w - x - 1) + 2] = col[(3 * w) * (y)+(3 * x) + 2];
		}
	}
}

extern "C" __declspec(dllexport) void EngineVerImageMirror(unsigned char* ret,
	unsigned char* col, int w, int h, int BitsPerPixel)
{
	if (BitsPerPixel != 24) return;
	// Vertical Mirroring------------------------------------------------------
	for (int y = 0; y < h; y++) {
		for (int x = 0; x < w; x++) {
			ret[(3 * w) * (h - y - 1) + (3 * x) + 0] = col[(3 * w) * (y)+(3 * x) + 0]; // B (x는 x' = x 고정시키고, y는 y' = h - y - 1로 변환)
			ret[(3 * w) * (h - y - 1) + (3 * x) + 1] = col[(3 * w) * (y)+(3 * x) + 1]; // G ''
			ret[(3 * w) * (h - y - 1) + (3 * x) + 2] = col[(3 * w) * (y)+(3 * x) + 2]; // R ''
		}
	}
}

extern "C" __declspec(dllexport) void EngineImageEmbossing(unsigned char* ret,
	unsigned char* col, int w, int h)
{
	unsigned char* gry = new unsigned char[w * h];
	//  1. 컬러 영상을 명도(gray) 영상으로 변환.

	for (int y = 0; y < h; y++) {
		for (int x = 0; x < w; x++) {
			unsigned char b = col[(3 * w) * (y)+(3 * x) + 0];
			unsigned char g = col[(3 * w) * (y)+(3 * x) + 1];
			unsigned char r = col[(3 * w) * (y)+(3 * x) + 2];
			gry[(w) * (y)+(x)] = (unsigned char)(0.2 * r + 0.7 * g + 0.1 * b);
		}
	}
	//  2. 명도 영상을 아래의 엠보싱 필터와 컨볼루션 처리를 한다.
	int M[3][3] = // Embossing Filter
	{
		{-1, 0, 0},
		{0, 0, 0},
		{0, 0, 1},
	};
	/* 3. 컨볼루션 결과 영상의 필셀 값은 정수 값이 되니 저장될 버퍼는
			int형식의 배열로 선언. */
	int x, y, pel;
	int* tmp = new int[w * h];
	for (y = 1; y < h - 1; y++) {
		for (x = 1; x < w - 1; x++) {

			pel =
				gry[w * (y - 1) + (x - 1)] * M[0][0] + gry[w * (y - 1) + (x)] * M[0][1] +
				gry[w * (y - 1) + (x + 1)] * M[0][2] +
				gry[w * (y)+(x - 1)] * M[1][0] + gry[w * (y)+(x)] * M[1][1] +
				gry[w * (y)+(x + 1)] * M[1][2] +
				gry[w * (y + 1) + (x - 1)] * M[2][0] + gry[w * (y + 1) + (x)] * M[2][1] +
				gry[w * (y + 1) + (x + 1)] * M[2][2];
			tmp[w * (y)+(x)] = pel;
		}
	}
	/* 4.컨볼루션 결과는 정수값을 가지므로 결과 영상에 대해서 0 ~ 255 사이 unsigned
		  char 값으로 정규화 변환을 수행한다. */

		  // ------------------------------------------
		  // Get min & max value for pixle value normalization to 0 - 255
		  // ------------------------------------------
	int max = tmp[0];
	int min = tmp[0];
	// 5. 정수 영상의 최대 및 최소값을 구함.
	for (y = 1; y < h - 1; y++) {
		for (x = 1; x < w - 1; x++) {
			if (tmp[w * (y)+(x)] < min)min = tmp[w * (y)+(x)];
			if (tmp[w * (y)+(x)] > max)max = tmp[w * (y)+(x)];
		}
	}

	// 정수 영상을 다음 공식에 따라 unsigned char 영상으로 변환.
	// ------------------------------------------
	// Pixle value normalization to 0 - 255
	// ------------------------------------------
	for (y = 1; y < h - 1; y++) {
		for (x = 1; x < w - 1; x++) {
			pel = (tmp[w * (y)+(x)] - min) * 255 / (max - min);

			if (pel < 0) pel = 0;
			else if (pel > 255)pel = 255;

			ret[3 * ((w) * (y)+(x)) + 0] = pel;
			ret[3 * ((w) * (y)+(x)) + 1] = pel;
			ret[3 * ((w) * (y)+(x)) + 2] = pel;
		}
	}
}

extern "C" __declspec(dllexport) void EngineHistogramEqualization(
	unsigned char* ret, unsigned char* col, int w, int h)
{
	HistogramEqualization(ret, col, w, h);
}