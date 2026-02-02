
MB_DEFBUTTON EQU 100h
ID_NO EQU 7
MB_YESNO EQU 4

extern MessageBoxA
extern ExitProcess

section .data
    text db "Do you want to exit", 0
    caption db "Message", 0


section .text
global main
main:

    sub rsp, 40
    
    display_mb:
    xor rcx, rcx
    lea rdx, text
    lea r8, caption
    mov r9d, MB_YESNO | MB_DEFBUTTON; r9d - меньший размер
    
    call MessageBoxA
    ;write your code here
    cmp rax, ID_NO
    je display_mb
    add rsp, 40
    ret