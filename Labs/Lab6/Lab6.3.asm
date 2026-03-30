%include "io.inc"

section .bss
string resb 256 

section .text
global main
main:
    mov ebp, esp; for correct debugging
    GET_STRING string, 256
    cld
    mov edi, string
    mov al, 0
    mov ecx, 256
    repne scasb
    sub edi, string
    dec edi
    PRINT_DEC 4, edi
    NEWLINE    
    ret