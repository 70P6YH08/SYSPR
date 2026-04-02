%include "io.inc"

section .data
apple db "Apple", 0

section .bss
copy resb 6 

section .text
global main
main:
    mov ebp, esp; for correct debugging
    
    PRINT_STRING 'Input position: '
    GET_DEC 4, eax
    
    NEWLINE
    
    PRINT_STRING 'Input Length: '
    GET_DEC 4, ebx
    
    mov esi, apple
    add esi, eax
    mov edi, copy
    add edi, eax
    mov ecx, ebx
    
    cld
    rep movsb

    xor eax, eax
    ret