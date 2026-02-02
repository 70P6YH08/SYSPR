%include "io.inc"
section .data
    n dd 0
    num dd 0

section .text
global main

main:
    mov ebp, esp; for correct debugging

input_n:
    PRINT_STRING " input count: "
    GET_DEC 4, [n]
    mov ecx, [n]
    jl input_n
    jecxz input_n
    
    mov eax, 0
    mov ebx, 0

input_loop:
    PRINT_STRING " input number: "
    GET_DEC 4, [num]
    mov edx, [num]
    
    add eax, edx
    
    loop input_loop
    
    PRINT_STRING " Sum equals: "
    PRINT_DEC 4, [eax]
    
    xor rdi, rdi
    ret