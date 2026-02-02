section .data
    num dq 45
section .text
global main
main:
    mov rbp, rsp; for correct debugging
    mov rdi, 10
    call sum
    mov rbx, rax
    
    mov rcx, 3
    mov rdx, 2
    call sum1
    mov qword [num], rax
    ret
sum1:
    mov rax, rcx
    add rax, rdx
    ret
sum:
    push rdi
    push qword [num]
    mov qword [num], 7
    mov rax, 5
    add rax, [num]
    pop qword [num]
    pop rdi
    ret