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
			ret[(3 * w) * (h - y - 1) + (3 * x) + 0] = col[(3 * w) * (y)+(3 * x) + 0]; // B (x�� x' = x ������Ű��, y�� y' = h - y - 1�� ��ȯ)
			ret[(3 * w) * (h - y - 1) + (3 * x) + 1] = col[(3 * w) * (y)+(3 * x) + 1]; // G ''
			ret[(3 * w) * (h - y - 1) + (3 * x) + 2] = col[(3 * w) * (y)+(3 * x) + 2]; // R ''
		}
	}
}

extern "C" __declspec(dllexport) void EngineImageEmbossing(unsigned char* ret,
	unsigned char* col, int w, int h)
{
	unsigned char* gry = new unsigned char[w * h];
	//  1. �÷� ������ ��(gray) �������� ��ȯ.

	for (int y = 0; y < h; y++) {
		for (int x = 0; x < w; x++) {
			unsigned char b = col[(3 * w) * (y)+(3 * x) + 0];
			unsigned char g = col[(3 * w) * (y)+(3 * x) + 1];
			unsigned char r = col[(3 * w) * (y)+(3 * x) + 2];
			gry[(w) * (y)+(x)] = (unsigned char)(0.2 * r + 0.7 * g + 0.1 * b);
		}
	}
	//  2. �� ������ �Ʒ��� ������ ���Ϳ� ������� ó���� �Ѵ�.
	int M[3][3] = // Embossing Filter
	{
		{-1, 0, 0},
		{0, 0, 0},
		{0, 0, 1},
	};
	/* 3. ������� ��� ������ �ʼ� ���� ���� ���� �Ǵ� ����� ���۴�
			int������ �迭�� ����. */
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
	/* 4.������� ����� �������� �����Ƿ� ��� ���� ���ؼ� 0 ~ 255 ���� unsigned
		  char ������ ����ȭ ��ȯ�� �����Ѵ�. */

		  // ------------------------------------------
		  // Get min & max value for pixle value normalization to 0 - 255
		  // ------------------------------------------
	int max = tmp[0];
	int min = tmp[0];
	// 5. ���� ������ �ִ� �� �ּҰ��� ����.
	for (y = 1; y < h - 1; y++) {
		for (x = 1; x < w - 1; x++) {
			if (tmp[w * (y)+(x)] < min)min = tmp[w * (y)+(x)];
			if (tmp[w * (y)+(x)] > max)max = tmp[w * (y)+(x)];
		}
	}

	// ���� ������ ���� ���Ŀ� ���� unsigned char �������� ��ȯ.
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