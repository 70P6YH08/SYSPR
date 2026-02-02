section .data
    nums dd 1,2,3,4,5
    numbers times 5 dd 2
section .bss
    buffer resd 5
section .text
global main
main:
    mov ebp, esp; for correct debugging
    xor eax, eax
    cld
    lea esi, nums
    lea edi, buffer
    mov ecx, 5
    
    lodsd
    
    stosd
    
    ;repe cmpsd
    ;rep movsd
    
    xor eax, eax
    ret