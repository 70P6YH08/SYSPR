%include "io.inc"

section .data
    x dw 0
    y dw 0
    msg db 'Input x: ', 0
    
section .text
global main
main:
    mov ebp, esp; for correct debugging
    PRINT_STRING msg
    GET_DEC 4, x
    
    PRINT_STRING 'Input y: '
    GET_DEC 4, y
    
    ;Task 1
    mov ax, [x]
    mov cx, [y]
    cbw
    div cx
    PRINT_STRING [y]
    ret
    
    