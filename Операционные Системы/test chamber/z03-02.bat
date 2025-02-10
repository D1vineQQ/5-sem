@echo off
chcp 65001 > nul
mkdir TXT
move *.%1 TXT\
echo Все .%1 перемещены в папку TXT
pause