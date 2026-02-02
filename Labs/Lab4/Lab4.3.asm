%include "io.inc"
section .data
    n dd 0
section .text
global main
main:
    mov ebp, esp; for correct debugging
    mov eax, 2
    ;Task 3
do_while_start:
    PRINT_STRING "input number: "
    GET_DEC 4, [n]
    mov ebx, [n]
    PRINT_DEC 4, ebx
    
    cmp ebx, eax
    jg less
    jb more
    PRINT_STRING " You win"
    jmp do_while_end
    
less:
    PRINT_STRING " Input less!"
    jmp do_while_start
    
more:
    PRINT_STRING " Input more!"
    jmp do_while_start
    
do_while_end:
    xor eax, eax
    ret