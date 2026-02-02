#include <stdio.h>

struct person{int id; char* name; int age;};

void print_person(struct person pers){
    printf("Id: %d", pers.id);
    printf("Name: %s", pers.name);
    printf("Age: %d", pers.age);
}