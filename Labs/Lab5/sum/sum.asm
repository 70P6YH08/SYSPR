global sum

section .text
;Сложение
;первый параметр - rcx
;второй параметр - rdx
;результат - rax
sum:
    mov rax, rcx
    add rax, rdx
    ret