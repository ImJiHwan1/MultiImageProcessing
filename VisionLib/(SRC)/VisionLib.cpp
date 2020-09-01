#include "VisionLib.h"
#include <memory.h>

void RGB_TO_YUV(BYTE R, BYTE G, BYTE B, BYTE& Y, BYTE& U, BYTE& V)
{
	Y = (BYTE)limit(0.299 * R + 0.587 * G + 0.144 * B + 0.5);
	U = (BYTE)limit(-0.169 * R - 0.331 * G + 0.500 * B + 128 + 0.5);
	V = (BYTE)limit(0.500 * R - 0.419 * G - 0.081 * B + 128 + 0.5);
}

void YUV_TO_RGB(BYTE Y, BYTE U, BYTE V, BYTE& R, BYTE& G, BYTE& B)
{
	R = (BYTE)limit(Y + 1.4075 * (V - 128) + 0.5);
	G = (BYTE)limit(Y - 0.3455 * (U - 128) - 0.7169 * (V - 128) + 0.5);
	B = (BYTE)limit(Y + 1.7790 * (U - 128) + 0.5);
}

void SplitRGB_TO_YUV_channel(BYTE* Y, BYTE* U, BYTE* V, BYTE* col, int w,
	int h)
{
	BYTE y, u, v;

	for (int j = 0; j < h; j++)
		for (int i = 0; i < w; i++)
		{
			BYTE b = col[(3 * w) * (j)+(3 * i)];
			BYTE g = col[(3 * w) * (j)+(3 * i) + 1];
			BYTE r = col[(3 * w) * (j)+(3 * i) + 2];

			RGB_TO_YUV(r, g, b, y, u, v);

			Y[3 * ((w) * (j)+(i))] = y;
			Y[3 * ((w) * (j)+(i)) + 1] = y;
			Y[3 * ((w) * (j)+(i)) + 2] = y;

			U[3 * ((w) * (j)+(i))] = u;
			U[3 * ((w) * (j)+(i)) + 1] = u;
			U[3 * ((w) * (j)+(i)) + 2] = u;

			V[3 * ((w) * (j)+(i))] = v;
			V[3 * ((w) * (j)+(i)) + 1] = v;
			V[3 * ((w) * (j)+(i)) + 2] = v;
		}
}

void SplitYUV_TO_RGB_channel(BYTE* R, BYTE* G, BYTE* B, BYTE* col, int w,
	int h)
{
	BYTE r, g, b;

	for (int j = 0; j < h; j++)
		for (int i = 0; i < w; i++)
		{
			BYTE u = col[(3 * w) * (j)+(3 * i)];
			BYTE y = col[(3 * w) * (j)+(3 * i) + 1];
			BYTE v = col[(3 * w) * (j)+(3 * i) + 2];

			YUV_TO_RGB(y, u, v, r, g, b);

			R[3 * ((w) * (j)+(i))] = r;
			R[3 * ((w) * (j)+(i)) + 1] = r;
			R[3 * ((w) * (j)+(i)) + 2] = r;

			G[3 * ((w) * (j)+(i))] = g;
			G[3 * ((w) * (j)+(i)) + 1] = g;
			G[3 * ((w) * (j)+(i)) + 2] = g;

			B[3 * ((w) * (j)+(i))] = b;
			B[3 * ((w) * (j)+(i)) + 1] = b;
			B[3 * ((w) * (j)+(i)) + 2] = b;
		}
}

unsigned char* GrayImage(unsigned char* col, int w, int h)
{
	unsigned char* gry = new unsigned char[w * h];

	for (int y = 0; y < h; y++) {
		for (int x = 0; x < w; x++) {

			unsigned char  b = col[3 * ((w) * (y)+(x)) + 0];
			unsigned char  g = col[3 * ((w) * (y)+(x)) + 1];
			unsigned char  r = col[3 * ((w) * (y)+(x)) + 2];

			gry[(w) * (y)+(x)] = (unsigned char)(0.2 * r + 0.7 * g + 0.1 * b);
		}
	}

	return gry;
}

void Histogram(int histo[256], unsigned char* gry, int w, int h)
{
	// 히스토그램 계산
	int x, y, i, temp[256] = { 0, };

	for (y = 0; y < h; y++) {
		for (x = 0; x < w; x++) {
			histo[gry[(w) * (y)+(x)]]++;
		}
	}
	float area = (float)w * h;
	for (i = 0, i < 256; i++;) {
		histo[i] = temp[i] / area;
	}
}

void HistogramEqualization(unsigned char* ret, unsigned char* col,
	int w, int h)
{
	unsigned char* gry = GrayImage(col, w, h);

	//히스토그램 계산
	int hist[256] = { 0, };
	Histogram(hist, gry, w, h);

	//히스토그램 누적함수 계산
	int cdf[256] = { 0, };
	cdf[0] = hist[0];
	for (int i = 1; i < 256; i++)
		cdf[i] = cdf[i - 1] + hist[i];

	//히스토그램 균등화
	float N = (float)w * h;
	for (int y = 0; y < h; y++) {
		for (int x = 0; x < w; x++) {

			unsigned char g = (unsigned char)limit(cdf[gry[(w) * (y)+(x)]] * 255 / N);
			ret[3 * ((w) * (y)+(x)) + 0] = g;
			ret[3 * ((w) * (y)+(x)) + 1] = g;
			ret[3 * ((w) * (y)+(x)) + 2] = g;
		}
	}
	delete[] gry;
}