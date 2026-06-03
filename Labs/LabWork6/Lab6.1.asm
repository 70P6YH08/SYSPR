%include "io.inc"

section .data
apple db "Hello World", 0
len equ $ - apple
elemSize equ 1
lastPosition equ len - elemSize

section .bss
copy resb len

section .text
global main
main:
    mov rbp, rsp; for correct debugging
    mov rsi, apple
    add rsi, lastPosition
    mov rdi, copy
    add rdi, lastPosition
    mov rcx, len
    
    std
    rep movsb
    
    xor eax, eax
    ret