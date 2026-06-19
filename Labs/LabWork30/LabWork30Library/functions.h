#pragma once

#ifdef  LIBDLL_EXPORTS
#define LIBDLL_API __declspec(dllexport)
#else
#define LIBDLL_API __declspec(dllexport)
#endif //  LIBDLL_EXPORTS

struct Point
{
	double x;
	double y;
};

extern "C" LIBDLL_API bool is_simple(int num);
extern "C" LIBDLL_API int count_simple_nums(int* arr, int length);
extern "C" LIBDLL_API double hypotenuse(Point f, Point s);
