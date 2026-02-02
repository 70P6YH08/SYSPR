%include "io.inc"
section .data
    x dd 0
    a dd 0

section .text
global main
main:
    mov ebp, esp; for correct debugging
    PRINT_STRING "Input a:"
    GET_DEC 4, a
    PRINT_STRING "Input x:"
    GET_DEC 4, x
    
    mov eax, [a]
    mov ebx, [x]
    cmp ebx, -10
    jl ax2
    cmp ebx, 10
    jge aMinx
    jmp aModxNext
ax2:
    imul ebx, ebx
    imul eax, ebx
    PRINT_DEC 4, eax
    jmp exit
aMinx:
    sub eax, ebx
    PRINT_DEC 4, eax
    jmp exit
    
aModxNext:
    cmp ebx, 0
    jl modul
    jmp aModx
modul:
    imul eax, -1
    jmp aModx
aModx:
    imul eax, ebx
    PRINT_DEC 4, eax
    jmp exit
exit:
    ret