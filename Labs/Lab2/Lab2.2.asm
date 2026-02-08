%include "io.inc"
section .data
    a dd 0
    x dd 0
    b dd 0
    c dd 0

section .text
global main
main:
    mov ebp, esp; for correct debugging
    ;Task 2
    mov ebp, esp; for correct debugging
    
    GET_DEC 4, a
    PRINT_STRING "Input a:"
    PRINT_DEC 4, a
    
    NEWLINE
    
    GET_DEC 4, x
    PRINT_STRING "Input x:"
    PRINT_DEC 4, x
    
    NEWLINE
    
    GET_DEC 4, b
    PRINT_STRING "Input b:"
    PRINT_DEC 4, b
    
    NEWLINE
    
    GET_DEC 4, c
    PRINT_STRING "Input c:"
    PRINT_DEC 4, c
    
    NEWLINE
    
    mov ecx, [x]
    mov edx, [x]
    imul edx, edx
    PRINT_STRING "Output x^2:"
    PRINT_DEC 4, edx
    
    NEWLINE
    
    mov eax, [a]
    imul eax, edx
    PRINT_STRING "Output ax^2:"
    PRINT_DEC 4, eax
    
    NEWLINE
    
    mov ebx, [b]
    imul ebx,  ecx
    PRINT_STRING "Output bx:"
    PRINT_DEC 4, ebx
    
    NEWLINE
    
    add eax, ebx
    
    mov ecx, [c]
    add eax, ecx
    PRINT_STRING "Output ax^2+bx+c:"
    PRINT_DEC 4, eax
    ret