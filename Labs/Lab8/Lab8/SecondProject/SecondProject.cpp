#include <iostream>

int main()
{
    setlocale(LC_ALL, "Russian");

    int counter, sum = 0;
    std::cout << "Введите последнее число: ";
    std::cin >> counter;

    for (size_t i = 1; i <= counter; i++)
    {
        sum += i;
    }
    std::cout << sum;
}