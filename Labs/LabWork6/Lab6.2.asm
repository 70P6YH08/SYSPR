%include "io.inc"

extern scanf, printf

section .data`
apple db "Hello World", 0
len equ $ - apple

format_n db "String length: %d", 0

section .bss
n resd 1
copy resb 0

section .text
global main
main:
    mov ebp, esp; for correct debugging
    
    sub esp, 40
    
    lea edi, [rel format_n]
    lea esi, [n]
    call printf
    
    lea edi, [rel format_n]
    lea esi, [n]
    call scanf
    
    std
    rep movsb
    
    add esp, 40
    xor eax, eax
    ret