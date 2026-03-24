section .data
    a dq 0.0
    b dq 0.0
    hypot dq 0.0
    fmt_in db "%lf %lf", 0
    fmt_out db "Hypotenuse: %f", 0
    msg_title db "Result", 0
    msg_prompt db "Repeat? Yes/No", 0
    buffer db 100 dup(0)          ; buffer for formatted output

section .text
global main
extern scanf, printf, MessageBoxA, ExitProcess

main:
    mov rbp, rsp; for correct debugging
    sub rsp, 40          ; allocate shadow space and align stack
start:
    ; read a and b
    lea rcx, [fmt_in]
    lea rdx, [a]
    lea r8, [b]
    sub rsp, 32          ; shadow space for scanf
    call scanf
    add rsp, 32

    ; compute hypot = sqrt(a*a + b*b)
    fld qword [a]
    fld qword [a]
    fmul
    fld qword [b]
    fld qword [b]
    fmul
    fadd
    fsqrt
    fstp qword [hypot]

    ; format result into buffer
    lea rcx, [buffer]    ; destination buffer
    lea rdx, [fmt_out]   ; format string
    movsd xmm2, qword [hypot] ; double value (second argument for sprintf)
    sub rsp, 32          ; shadow space for sprintf
    call printf
    add rsp, 32

    ; show result in MessageBox
    xor rcx, rcx         ; hWnd = NULL
    lea rdx, [buffer]    ; message text
    lea r8, [msg_title]  ; caption
    mov r9d, 0           ; MB_OK
    sub rsp, 32          ; shadow space for MessageBoxA
    call MessageBoxA
    add rsp, 32

    ; ask user to repeat
    xor rcx, rcx
    lea rdx, [msg_prompt]
    lea r8, [msg_title]
    mov r9d, 4           ; MB_YESNO
    sub rsp, 32
    call MessageBoxA
    add rsp, 32
    cmp eax, 6           ; IDYES = 6
    je start

    ; exit
    xor rcx, rcx
    call ExitProcess