# x y z r g b순으로 구조가 되어있는 asc파일에서 rgb를 추출하여 R의 숫자별로 분류
# (기능) 전체 출력, R별로 길이 분포도 출력, R을 입력시 원하는 R에 속해있는 모음들을 한꺼번에 보는 모음
import sys

rgb_set = [[] for i in range(256)]
rgb_l = []

with open('test2.asc','r') as f:
    lines = f.readlines()
    for line in lines:
        tmp = line.rstrip().split(" ")
        rgb = [int(tmp[3]),int(tmp[4]),int(tmp[5])]
        if not rgb in rgb_set[int(tmp[3])]:
            rgb_set[int(tmp[3])].append(rgb)



# 전체 출력 or 정렬
for i in range(len(rgb_set)):
    if(len(rgb_set[i])!=0):
        rgb_set[i].sort()
        #print(rgb_set[i])
    rgb_l.append(len(rgb_set[i]))
    #print("\n")

'''
# 길이 분포도 출력
print("\n")
for i in range(len(rgb_l)):
    print("R : ",i,"->","Len : ",rgb_l[i])
'''

sel_index = int(input("출력할 첫번쨰 R숫자를 입력하시요 > "))
rgb_set[sel_index].sort()
print(rgb_set[sel_index])