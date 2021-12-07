code segment
assume cs:code, ds:code, ss:code
org 100h
num1 db 0Eh
num2 db 0Eh
msg1 db 0DH,0AH, "Son iguales",'$'
msg2 db 0DH,0AH, "No son iguales",'$'
main proc
mov ax,cs
mov ds,ax
mov al,num1
mov bl, num2
cmp al, bl 
JE si0 
JNE sino0 


si0:
lea dx,msg1
mov ah,09h
int 21h
jmp cont0



sino0:
lea dx,msg2
mov ah,09h
int 21h
jmp cont0

cont0:
mov ah,08
int 21h
main endp
code ends
end main