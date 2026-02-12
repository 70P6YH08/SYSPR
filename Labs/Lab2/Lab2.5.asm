%include "io.inc"
section .data
    red dd 0
    green dd 0
    blue dd 0
section .text
global main
main:
    mov ebp, esp; for correct debugging
    ;Task 4
    mov ebp, esp; for correct debugging
    GET_DEC 4, red
    PRINT_STRING "Red: "
    PRINT_DEC 4, red
    
    NEWLINE
    
    GET_DEC 4, green
    PRINT_STRING "Green: "
    PRINT_DEC 4, green
    
    NEWLINE
    
    GET_DEC 4, blue
    PRINT_STRING "Blue: "
    PRINT_DEC 4, blue
    
    NEWLINE
    
    mov eax, [red]
    mov ebx, [green]
    mov ecx, [blue]
    
    imul eax, ebx
    imul eax, ecx
    
    PRINT_STRING "Color as a number: "
    PRINT_HEX 4, eax
    ret