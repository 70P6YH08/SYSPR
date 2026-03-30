%include "io.inc"

section .data
apple db "Apple",0

section .bss
input resb 2

section .text
global main
main:
    mov ebp, esp; for correct debugging
    GET_STRING input, 2
    cld
    mov edi, apple
    mov al, [input]
    mov esi, apple
    mov ecx, 256
    repne scasb
    sub edi, apple  
    PRINT_DEC 4, edi
    NEWLINE    
    ret