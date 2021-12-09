code segment
assume cs:code, ds:code, ss:code
org 100h
a db 00h
main proc
mov ax,cs
mov ds,ax
jmp hacer0


hacer0:
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
ADC al, 01h
mov a, al
mov al,a
mov bl, 0Ah
cmp al, bl 
JGE contHacer0 
jmp hacer0 


contHacer0:
mov ah,08
int 21h
main endp
code ends
end main