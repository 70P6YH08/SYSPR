%include "io.inc"
section .data
    number dd 0
section .text
global main
main:
    mov ebp, esp; for correct debugging
    ;Task 3
    mov ebp, esp; for correct debugging
    
    GET_DEC 4, number
    PRINT_STRING "Input number: "
    PRINT_DEC 4, number
    
    NEWLINE
    
    mov eax, [number]
    mov ebx, 2
    xor edx, edx
    idiv ebx
    PRINT_DEC 4, edx
    ret