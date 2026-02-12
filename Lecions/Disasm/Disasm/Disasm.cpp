#define _CRT_SECURE_NO_WARNINGS

#include <iostream>

int main()
{
    int x = 0;
    const char* hello = "Hello Roman";

    scanf("%d", &x);

    int result = x * x;

    printf("%d", result);
}