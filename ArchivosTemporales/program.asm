code segment
assume cs:code, ds:code, ss:code
org 100h
a db 05h
main proc
mov ax,cs
mov ds,ax
mov al, a
aam
add ax, 3030h
push ax
mov dl, ah
mov ah, 02h
int 21h
pop dx
mov ah, 02h
int 21h
 MOV DL,13
INT 21h
MOV DL,10
 INT 21h
mov al, a
ADC al, 05h
mov a, al
mov al, a
aam
add ax, 3030h
push ax
mov dl, ah
mov ah, 02h
int 21h
pop dx
mov ah, 02h
int 21h
 MOV DL,13
INT 21h
MOV DL,10
 INT 21h
mov al, a
SBB al, 05h
mov a, al
mov al, a
aam
add ax, 3030h
push ax
mov dl, ah
mov ah, 02h
int 21h
pop dx
mov ah, 02h
int 21h
 MOV DL,13
INT 21h
MOV DL,10
 INT 21h
mov al, a
mov bl, 03h
MUL bl 
mov a, al
mov al, a
aam
add ax, 3030h
push ax
mov dl, ah
mov ah, 02h
int 21h
pop dx
mov ah, 02h
int 21h
 MOV DL,13
INT 21h
MOV DL,10
 INT 21h
mov al, a
mov bl, 03h
DIV bl 
mov a, al
mov al, a
aam
add ax, 3030h
push ax
mov dl, ah
mov ah, 02h
int 21h
pop dx
mov ah, 02h
int 21h
 MOV DL,13
INT 21h
MOV DL,10
 INT 21h
mov ah,08
int 21h
main endp
code ends
end main