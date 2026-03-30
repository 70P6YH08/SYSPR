%include "io.inc"

extern scanf
extern printf
extern MessageBoxA@16

section .data
    format_in db "%lf %lf", 0
    format_out db "%lf", 10
    msg_main db "Repeat?", 0
    msg_head db "Choice", 0
    
section .bss
    a resq 1
    b resq 1
    res resq 1
        
section .text
    global main

main:
    mov ebp, esp
start:
    push b
    push a
    push format_in
    call scanf
    add esp, 12
    
    fld qword [a]
    fmul st0, st0
    fld qword [b]
    fmul st0, st0
    faddp st1, st0
    fsqrt 
    fstp qword [res]
    
    push dword [res+4]
    push dword [res]
    push format_out
    call printf
    add esp, 12
    
    push 4
    push msg_head
    push msg_main
    push 0
    call MessageBoxA@16
    
    cmp eax, 6
    je start
    xor eax, eax
    ret