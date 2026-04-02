%include "io.inc"

extern scanf
extern printf
extern MessageBoxA@16

section .data
    format_in db "%lf %lf", 0
    format_out db "%lf", 10
    msg_main db "Repeat?", 0
    msg_head db "Message", 0
    
section .bss
    a resq 1
    b resq 1
    res resq 1
        
section .text
    global main

main:
    mov ebp, esp
start:
    sub esp, 40
    push b
    push a
    push format_in
    call scanf
    add esp, 40
    
    fld qword [a]
    fmul st0, st0
    fld qword [b]
    fmul st0, st0
    faddp st1, st0
    fsqrt 
    fstp qword [res]
    
    sub esp, 40
    push dword [res+4]
    push dword [res]
    push format_out
    call printf
    add esp, 40
    
    sub esp, 40
    push 4
    push msg_head
    push msg_main
    push 0
    call MessageBoxA@16
    add esp, 40
    
    cmp eax, 6
    je start
    xor eax, eax
    ret