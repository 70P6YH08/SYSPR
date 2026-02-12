extern scanf, printf

global main

section .data
msg db "Size equals %d"
string db "Hello World",0
len equ $-string
elemSize equ 1
sizeString equ len / elemSize
lastPosition equ len - elemSize
 
section .bss
    size resd 0
section .text
main:
    mov rbp, rsp; for correct debugging
    mov rdi, msg
    xor rax, rax
    call printf
    
    mov rsi, string
    add rsi, lastPosition
    mov rdi, copy
    add rdi, lastPosition
    mov rcx, sizeString
 
    std
    rep movsb
    
    mov rax, 60
    syscall