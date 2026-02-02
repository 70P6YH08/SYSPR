%include "io.inc"
section .data

    size dd 0

section .text
global main
main:
    mov ebp, esp; for correct debugging
    ;Task 4
    mov ebp, esp; for correct debugging
    PRINT_STRING "Input number:"
    GET_DEC 4, size
    
    mov eax, [size]
    mov rcx, 1024
    div rcx
    mov eax, ecx
    PRINT_DEC 4, eax
    ret