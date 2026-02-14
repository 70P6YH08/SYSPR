#include <iostream>
int main()
{
    setlocale(LC_ALL, "Russian");

    int num, sum = 0;
    std::cout << "Введите последнее число: ";
    std::cin >> num;

    int counter = 1;
    for (counter; counter <= num; counter++)
    {
        sum = sum + counter;
    }
    std::cout << sum;
}