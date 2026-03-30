%include "io.inc"

section .data
    password db "secret", 0
    len equ $-password - 1

section .bss
    input resb 256

section .text
global main
main:
    PRINT_STRING "Enter password"
    GET_STRING input, 256
    mov edi, input
    mov esi, edi
    xor al, al
    mov ecx, -1
    cld
    repne scasb
    sub edi, esi
    dec edi
    cmp edi, len
    jne .invalid
    mov esi, input
    mov edi, password
    mov ecx, len
    cld
    repe cmpsb
    jne .invalid
    PRINT_STRING "Access"
    jmp .end

.invalid: 
    PRINT_STRING "Invalid password"

.end:
    xor eax, eax
    ret