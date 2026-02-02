section .data
    text db "hello world", 0
    target db "o"
    pos dd -1
    length dd 12
section .text
global main
main:
    mov ebp, esp; for correct debugging
    cld
    lea edi, text
    mov esi, edi
    mov al, [target]
    mov ecx, [length]
    repne scasb ; " world"
    jne not_found
    sub edi, esi ; 
    dec edi; edi = edi - 1
    mov [pos], edi
not_found:
    ret