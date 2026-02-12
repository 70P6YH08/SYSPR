%include "io.inc"
section .data
    x dd 0
    y dd 0

section .text
global main
main:
    mov ebp, esp; for correct debugging
    ;Task 1
    GET_DEC 4, [x]
    PRINT_STRING "Input x: "
    PRINT_DEC 4, [x]
    
    
    NEWLINE
    
    GET_DEC 4, [y]
    PRINT_STRING "Input y: "
    PRINT_DEC 4, [y]
    
    NEWLINE
    
    mov eax, [x]
    mov ebx, [y]
    cdq
    div ebx
    PRINT_STRING "Quotient: "
    PRINT_DEC 4, eax
    
    NEWLINE
    
    PRINT_STRING "Remains: "
    PRINT_DEC 4, edx
    ret