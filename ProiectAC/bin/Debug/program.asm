.DATA
.CODE
JMP START
START:
MOV R3,12
ADD R1,R3
OR R7,R3
INC R8
ASR R8
RLC R1
PUSH R8
BR ET1
MOV R4,2
SUB R11,5
ET1:
POP R10
SES
END