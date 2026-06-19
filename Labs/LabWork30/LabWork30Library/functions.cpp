#include "pch.h"
#include "functions.h"
#include <cmath>


bool is_simple(int num) {
	if (num < 2)
		return false;

	if (num == 2)
		return true;

	int count = 2;
	while (count < num) {
		if (num % count == 0) {
			return false;
		}
		count++;
	}
	return true;
}

int count_simple_nums(int* arr, int length) {
	int countSimple = 0;

	for (int i = 0; i < length; i++)
	{
		if (is_simple(arr[i]))
			countSimple++;
	}
	return countSimple;
}
double hypotenuse(Point f, Point s) {
	double xLen = abs(f.x - s.x);
	double yLen = abs(f.y - s.y);

	return sqrt(pow(xLen, 2) + pow(yLen, 2));
}
