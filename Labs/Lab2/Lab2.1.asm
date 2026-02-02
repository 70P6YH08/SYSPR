%include "io.inc"
section .data
    x dd 0
    y dd 0

section .text
global main
main:
    ;Task 1
    mov ebp, esp; for correct debugging
    PRINT_STRING "Input x:"
    GET_DEC 4, x
    PRINT_STRING "Input y:"
    GET_DEC 4, y
    
    mov ax, [x]
    mov bx, [y]
    div bx
    movsx eax,  ax
    movsx ebx, dx
    PRINT_STRING "quotient:"
    PRINT_DEC 4, eax
    PRINT_STRING "remains:"
    PRINT_DEC 4,  ebx
    ret