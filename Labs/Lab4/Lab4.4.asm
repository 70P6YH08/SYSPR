%include "io.inc"
section .data
    percent dd 0
    deposit dd 0
section .text
global main

main:
    mov ebp, esp; for correct debugging
    
    PRINT_STRING "Input deposit: "
    GET_DEC 4, [deposit]
    PRINT_DEC 4, [deposit]
    mov eax, [deposit]
    
    NEWLINE
    
    PRINT_STRING "Input percent: "
    GET_DEC 4, [percent]
    PRINT_DEC 4, [percent]
    mov edx,[percent]
    
    mov edi, 0
    
    mov ecx, eax
    
    NEWLINE
    
    PRINT_STRING "Deposit: "
    PRINT_DEC 4, eax
while:
    cmp ecx, 1000000
    jge exit
    inc edi
    mov eax, ecx ; в EAX записываем сумму вклада
    imul eax, edx
    mov ebx, 100
    xor edx, edx
    div ebx ; проценты, начисленные на сумму в EAX за год
    add ecx, eax ; добавляет к сумме за год процент
    mov edx, [percent]
    NEWLINE
    PRINT_STRING "Percent of "
    PRINT_DEC 4, edi
    PRINT_STRING " year: "
    PRINT_DEC 4, ecx
    
    jmp while
    
    exit:
    
    NEWLINE
    
    PRINT_STRING "User will be a millionaire in "
    PRINT_DEC 4, edi
    PRINT_STRING " years"
    xor eax, eax
    ret