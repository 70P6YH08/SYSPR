%include "io.inc"
section .data
    size dd 0
section .text
global main
main:
    mov ebp, esp; for correct debugging
    ;Task 4
    GET_DEC 4, size
    PRINT_STRING "Size in bytes: "
    PRINT_DEC 4, size
    
    NEWLINE
    
    mov eax, [size]
    shr eax, 10
    PRINT_STRING "Size in kilobites: "
    PRINT_DEC 4, eax
    ret