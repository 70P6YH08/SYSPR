%include "io.inc"
section .data
    x dd 0
    y dd 0

section .text
global main
main:
    mov ebp, esp; for correct debugging
    ;Task 1
    mov ebp, esp; for correct debugging
    GET_DEC 4, [x]
    PRINT_STRING "Input x: "
    PRINT_DEC 4, [x]
    
    
    NEWLINE
    
    GET_DEC 4, [y]
    PRINT_STRING "Input y: "
    PRINT_DEC 4, [y]
    
    NEWLINE
    
    mov ax, [x]
    mov bx, [y]
    xor dx,dx
    idiv bx
    PRINT_STRING "Quotient: "
    PRINT_DEC 4, ax
    
    NEWLINE
    
    PRINT_STRING "Remains: "
    PRINT_DEC 4,  dx
    ret