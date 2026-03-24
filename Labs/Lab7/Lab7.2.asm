section .data
    a dd 1
    b dd 2
    c dd 4
    d dd 3
    temp dd 0

section .text
global main
main:
    mov rbp, rsp; for correct debugging
    call virazhenie
    xor eax, eax
    ret
    virazhenie:
    fild dword [c]
    fild dword [d]
    
    fsub
    
    fild dword [a]
    fild dword [b]
    
    fadd
    fmul
    
    pop rbp
    ret
   