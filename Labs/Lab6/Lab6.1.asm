%include "io.inc"

section .data
apple db "Hello World",0
len equ $-nums
elSize equ 1
count equ len / elSize
lastPosition equ len - elSize

section .bss
copy resb 6 

section .text
global main
main:
    mov ebp, esp; for correct debugging
    mov esi, apple
    add esi, lastPosition
    mov edi, copy
    add edi, lastPosition
    mov ecx, count
    
    std
    rep movsb

    xor eax, eax
    ret