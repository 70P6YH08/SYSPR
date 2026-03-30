%include "io.inc"

section .bss
    buffer resb 256

section .text
global main
str_len:
    push ebp 
    mov ebp, esp
    push esi
    mov esi, [ebp+8]
    xor eax, eax

.loop:
    mov cl, [esi]
    cmp cl, 0
    je .end
    cmp cl, 10
    je .end
    inc eax
    inc esi
    jmp .loop
    
.end:
    pop esi
    pop ebp
    ret

main:
    mov ebp, esp
    GET_STRING buffer, 256
    push buffer
    call str_len
    add esp, 4
    PRINT_STRING "Length: "
    PRINT_DEC 4, eax
    NEWLINE
    xor eax, eax
    ret