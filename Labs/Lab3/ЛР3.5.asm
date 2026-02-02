%include "io.inc"
section .data
    sum dd 0
    payment dd 0

section .text
global main
main:
    mov ebp, esp; for correct debugging
    PRINT_STRING "Input sum:"
    GET_DEC 4, sum
    PRINT_STRING "Input payment:"
    GET_DEC 4, payment
    
    mov eax, [sum]
    mov ebx, [payment]
    cmp eax, ebx
    je noChange
    jb more
    sub eax, ebx
    PRINT_STRING "There is not enough money "
    PRINT_DEC 4, eax
    jmp exit
noChange:
    PRINT_STRING "No change is required "
    jmp exit
    
more:
    sub ebx, eax
    PRINT_STRING "More money deposited "
    PRINT_DEC 4, ebx
    jmp exit
    
exit:
    ret