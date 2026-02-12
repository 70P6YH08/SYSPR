global main

section .data
string db "Hello World",0
len equ $-string
elemSize equ 1
sizeString equ len / elemSize
lastPosition equ len - elemSize
 
section .bss
copy resb sizeString
 
section .text
main:
    mov rbp, rsp; for correct debugging
    mov rsi, string
    add rsi, lastPosition
    mov rdi, copy
    add rdi, lastPosition
    mov rcx, sizeString
 
    std
    rep movsb
    
    mov rax, 60
    syscall