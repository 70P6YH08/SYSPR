%include "io.inc"
section .data
    number dd 0

section .text
global main
main:
    mov ebp, esp; for correct debugging
    ;Task 3
    mov ebp, esp; for correct debugging
    PRINT_STRING "Input number:"
    GET_DEC 4, number
    
    
    PRINT_DEC 4, eax
    and eax, 1
    jmp even
    PRINT_STRING "odd"
    ret
 even:
    PRINT_STRING "even"
    ret