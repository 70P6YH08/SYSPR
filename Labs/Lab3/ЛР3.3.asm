%include "io.inc"
section .data

    a dd 0
    b dd 0
    c dd 0
    
section .text
global main
main:
    mov ebp, esp; for correct debugging
    
    PRINT_STRING "input a:"
    GET_DEC 4, a
    
    PRINT_STRING "input b: "
    GET_DEC 4, b
    
    PRINT_STRING "input c: "
    GET_DEC 4, c
    ;Task3
    mov eax, [a]
    mov ebx, [b]
    mov ecx, [c]
    
    cmp eax, ebx
    jg aOver
    jmp bOver
aOver:
    cmp eax, ecx
    jg aMax
    jmp cMax
aMax:
    PRINT_STRING "a is max"
    jmp exit
bOver:
    cmp ebx, ecx
    jg bMax
    jmp cMax
bMax:
    PRINT_STRING "b is max"
    jmp exit
cMax:
    PRINT_STRING "c is max"
    jmp exit
    
exit:
    xor eax, eax
    ret
    