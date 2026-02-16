section .text
global main
main:
    mov rbp, rsp; for correct debugging
    call pow2
    ret
    pow2:
    mov ecx, 4
    mov eax, 1
    shl eax, cl