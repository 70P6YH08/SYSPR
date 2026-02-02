%include "io.inc"
section .data
    x dd 0
    y dd 0
    
    number dd 10
    
    
section .text
global main
main:
    mov ebp, esp; for correct debugging
    PRINT_STRING "input x:"
    GET_DEC 4, x
    
    PRINT_STRING "input y: "
    GET_DEC 4, y
    
    PRINT_STRING "input number: "
    GET_DEC 4, number
    
    ;Task 1
    mov eax, [x]
    mov ebx, [y]
    cmp eax, ebx
    je equal
    PRINT_STRING "not equal"
    jmp task2
equal:
    PRINT_STRING "equal"
    jmp task2
    
task2:
    ;Task 2
    mov eax, [number]
    cmp eax, 4
    jg next
    PRINT_STRING "out of range"
    jmp exit
next:
    cmp eax, 14
    jl next1
    PRINT_STRING "out of range"
    jmp exit
next1:
    PRINT_STRING "within range (4..14)"
    jmp exit
    
exit:
    xor eax, eax
    ret
    