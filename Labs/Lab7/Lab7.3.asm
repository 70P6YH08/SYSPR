%include "io.inc"

section .text
global main
calc:
    push ebp 
    mov ebp, esp
    sub esp, 4
    push ebx
    mov eax, [ebp+12]
    imul eax, eax
    mov [ebp-4], eax
    imul eax, ecx
    mov ebx, edx
    imul ebx, [ebp+12]
    add eax, ebx
    add eax, [ebp+8]
    pop ebx
    mov esp, ebp
    pop ebp
    ret

main:
    mov ebp, esp
    PRINT_STRING "Enter a: "
    GET_DEC 4, eax
    NEWLINE
    PRINT_STRING "Enter b: "
    GET_DEC 4, ebx
    NEWLINE
    PRINT_STRING "Enter c: "
    GET_DEC 4, ecx
    NEWLINE
    PRINT_STRING "Enter x: "
    GET_DEC 4, edx
    NEWLINE
    push edx
    push ecx
    mov edx, ebx
    mov ecx, eax
    call calc
    add esp, 8
    PRINT_STRING "Result: "
    PRINT_DEC 4, eax
    NEWLINE
    xor eax, eax
    ret