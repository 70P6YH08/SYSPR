%include "io.inc"
section .data
    n dd 0
    num dd 0

section .text
global main

main:
    mov ebp, esp; for correct debugging

input_n:
    PRINT_STRING "Count equals zero!"
    NEWLINE
    PRINT_STRING "Input count: "
    GET_DEC 4, [n]
    PRINT_DEC 4, [n]
    NEWLINE
    mov ecx, [n]
    cmp ecx, 0
    jle input_n
    
    mov eax, 0
    mov ebx, 0

input_loop:
    GET_DEC 4, [num]
    mov edx, [num]
    add eax, edx
    jecxz exit
    loop input_loop ;;;;;
    
exit:
    PRINT_STRING "Sum equals: "
    PRINT_DEC 4, eax
    
    xor eax, eax
    ret