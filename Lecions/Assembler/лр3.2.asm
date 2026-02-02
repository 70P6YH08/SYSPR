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
    PRINT_STRING 'Input a: '
    GET_DEC 4, a
    
    PRINT_STRING 'Input x: '
    GET_DEC 4, x
    
    PRINT_STRING 'Input b: '
    GET_DEC 4, b
    
    PRINT_STRING 'Input c: '
    GET_DEC 4, c
    
    NEWLINE
    mov edx, [x]
    mov eax, [x]
    mov ebx, [a]
    mul edx
    mul ebx
    mov ecx,ebx
    
    mov dx,[x]
    mov ax,[b]
    mul dx
    
    PRINT_STRING 'result: '
    PRINT_DEC 4, eax
    
    xor eax, eax
    ret