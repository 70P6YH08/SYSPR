#pragma once

#ifdef  LIBDLL_EXPORTS
#define LIBDLL_API __declspec(dllexport)
#else
#define LIBDLL_API __declspec(dllexport)
#endif //  LIBDLL_EXPORTS

struct Number
{
	int num;
};

extern "C" LIBDLL_API int add(int a, int b);
extern "C" LIBDLL_API int average(const int* arr, int length);
extern "C" LIBDLL_API int struct_sum(Number a, Number b);
