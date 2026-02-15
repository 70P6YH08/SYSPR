MB_DEFBUTTON EQU 100h
ID_NO EQU 7
MB_YESNO EQU 4

extern printf, scanf, MessageBoxA, ExitProcess

section .data
    message db "Input 2 numbers %lf, %lf: ", 0
    num1 dd 0
    num2 dd 0
    c dd 0
    msg_c db "c = %lf"
    msg_text db "Repeat?"
global main
section .text
main:
    mov rbp, rsp; for correct debugging

repeat:
    push num2
    push num1
    push message
    call scanf
    add esp, 12 
    
    fld qword [num1]
    fmul st0, st0
    fld qword [num2]
    fmul st0, st0
    faddp
    fsqrt
    fstp qword [c]
    
    sub esp, 8
    fld qword [c]
    fstp qword [esp]
    push msg_c
    call printf
    add esp, 12
    
    mov r9d, MB_YESNO | MB_DEFBUTTON
    call MessageBoxA
    cmp rax, ID_NO
    je repeat
    
    call ExitProcess