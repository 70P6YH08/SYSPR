section .text
global main
main:
    mov rbp, rsp; for correct debugging
    call pow2
    ret
    pow2:
    mov rax, 1
    shl rax, 2
    xor eax, eax