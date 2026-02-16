section .data
    a dq 1
    b dq 2
    c dq 3
    d dq 4

section .text
global main
main:
    mov rbp, rsp; for correct debugging
    sub rsp, 40
    call virazhenie
    ret
    virazhenie:           ; сохраняем ebp
    mov  ebp, esp          ; устанавливаем базу кадра
    mov  eax, [ebp+8]      ; a
    add  eax, [ebp+12]     ; a + b
    mov  ecx, [ebp+16]     ; c
    sub  ecx, [ebp+20]     ; c - d
    imul eax, ecx          ; (a+b)*(c-d)      ; восстанавливаем ebp
    ret 
   