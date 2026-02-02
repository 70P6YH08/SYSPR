section .text
global main
main:
    mov ebp, esp; for correct debugging
    sub rsp, 3
    push byte[rsp+ 2], 1 ;[rsp + 10]
    push byte[rsp+ 1], 2 ;[rsp + 9]
    push byte[rsp], 3 ;[rsp + 8]
    
    call sum
    
    add rsp, 24 ;чистка стека
    
    xor rax, rax
    ret
    
sum:
    mov rax, [rsp + 10]
    add rax, [rsp + 9]
    add rax, [rsp + 8]