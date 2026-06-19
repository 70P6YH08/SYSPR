%include "io.inc"

section .text
global main
calc:
    push ebp 
    mov ebp, esp
    push ebx
    mov eax, [ebp+8]
    add eax, [ebp+12]
    mov ebx, [ebp+16]
    sub ebx, [ebp+20]
    imul eax, ebx
    pop ebx
    pop ebp
    ret

main:
    mov ebp, esp
    GET_DEC 4, eax
    GET_DEC 4, ebx
    GET_DEC 4, ecx
    GET_DEC 4, edx
    NEWLINE
    push edx
    push ecx
    push ebx
    push eax
    call calc
    add esp, 16
    PRINT_STRING "Result: "
    PRINT_DEC 4, eax
    NEWLINE
    xor eax, eax
    ret