%include "io.inc"
section .data
    n dd 0
section .text
global main
main:
    mov ebp, esp; for correct debugging
    GET_DEC 4, [n]
    mov eax, [n]
    mov ecx, 1
    ;Task 2
while_start:
    mov ebx, ecx
    imul ebx, ebx
    cmp ebx, eax
    jnl while_end
    PRINT_DEC 4, ebx
    add ecx, 1
    jmp while_start
while_end:
    xor eax, eax
    ret