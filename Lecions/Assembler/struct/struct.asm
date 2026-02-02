global main

extern print_person

section .data
    name db "Bob", 0
    age dq 43
    id dq 111

section .text

main:
    sub rsp, 40 ; резервация стека
    mov rax, [id]
    mov [rsp], rax
    lea rax, name
    mov [rsp + 8], rax
    lea rax, [age]
    mov [rsp + 16], rax
    
    call print_person
    
    add rsp, 40 ;восстановление стека

    xor rax, rax
    ret