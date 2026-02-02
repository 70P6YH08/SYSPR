global main

extern printf

section .data
    message db "Hello, I`m %s, I`m %d years old, I work in %s for %d dollars", 0
    name db "Roman",0
    age dq 22
    company db "AKT",0
    salary dq 1234
section .text

main:
    mov rbp, rsp; for correct debugging
    sub rsp, 40
    lea rcx, message ; 1
    lea rdx, name ; 2
    mov r8, qword[age] ; 3
    mov r9, company ; 4
    mov r10, [salary]
    mov qword[rsp + 32],r10 ; 5 
    call printf
    add rsp, 40
    xor rax, rax
    ret