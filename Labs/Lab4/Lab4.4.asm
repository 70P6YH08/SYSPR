%include "io.inc"
section .data
    percent dd 0
    deposit dd 0
section .text
global main

main:
    mov ebp, esp; for correct debugging
    PRINT_STRING "Input deposit: "
    GET_DEC 4, [deposit]
    PRINT_DEC 4, [deposit]
    mov ebx,[deposit]
    
    NEWLINE
    
    PRINT_STRING "Input percent: "
    GET_DEC 4, [percent]
    PRINT_DEC 4, [percent]
    mov edx,[percent]
    mov ax, 0
while:
    mov eax, 0
    cmp ebx, 1000000
    jge exit
    add ax, 1
    mov eax, ebx
    imul eax, edx
    mov ecx, 100
    div ecx
    add ebx, eax
    jmp while
    
    exit:
    PRINT_DEC 4, ax
    xor eax, eax
    ret