%include "io.inc"

section .text
global main
pow:
    push ebp 
    mov ebp, esp
    push ecx
    mov ecx, eax
    mov eax, 1
    shl eax, cl
    pop ecx
    mov esp, ebp
    pop ebp
    ret

main:
    mov ebp, esp
    PRINT_STRING "Enter x: "
    GET_DEC 4, eax
    call pow
    PRINT_STRING "Result: "
    PRINT_DEC 4, eax
    NEWLINE
    xor eax, eax
    ret