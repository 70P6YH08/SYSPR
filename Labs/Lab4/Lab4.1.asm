%include "io.inc"
section .data
    count dd 0
    count_j dd 1
    message db "#", 0, '$'
    enter_message db 0xA
section .text
global main
main:
    mov ebp, esp; for correct debugging
    ;Task 1
    GET_DEC 4, [count]
    mov eax, 0
    mov ebx, 0
    mov ecx, [count_j]
for_start_i:
    cmp eax, [count]
    jnl for_end
    
for_start_j:
    cmp ebx, ecx
    jnl for_i
    add ebx, 1
    PRINT_STRING message
    jmp for_start_j
    
for_i:
    PRINT_CHAR enter_message
    mov ebx, 0
    add ecx, 1
    add eax, 1
    jmp for_start_i
for_end:
    xor eax, eax
    ret