#include "pch.h"
#include "functions.h"


int add(int a, int b) {
	return a + b;
}

int average(const int* arr, int length) {
	int sum = 0;
	for (int i = 0; i < length; i++)
	{
		sum += arr[i];
	}
	return sum / length;
}
int struct_sum(Number a, Number b) {
	return a.num + b.num;
}
