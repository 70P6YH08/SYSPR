section .text
global main
main:
    mov ebp, esp; for correct dubugging
    mov ecx, 0
    mov eax, 0
mainloop:
    JECXZ exit
    ADD eax, 2
    LOOP mainloop
exit:
    xor eax, eax
    ret