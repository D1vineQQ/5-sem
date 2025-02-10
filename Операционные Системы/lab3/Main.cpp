#include <iostream>
#include <windows.h>

using namespace std;

PROCESS_INFORMATION CreateChildProcess(LPCWSTR path) {
    STARTUPINFO si;
    PROCESS_INFORMATION pi;
    ZeroMemory(&si, sizeof(si));
    si.cb = sizeof(si);
    ZeroMemory(&pi, sizeof(pi));

    bool bRes = CreateProcessW(path,
        NULL, NULL, NULL, FALSE, 0, NULL, NULL, &si, &pi);
    if (!bRes) {
        printf("Error %d\n", GetLastError());
    }

    CloseHandle(pi.hProcess);
    CloseHandle(pi.hThread);

    return pi;
}


int main() {

    setlocale(LC_ALL, "ru");

    PROCESS_INFORMATION pi1, pi2;

    pi1 = CreateChildProcess(L"D:\\Laboratory\\Операционные Системы\\x64\\Debug\\lab3_1.exe");
    //Sleep(333);
    pi2 = CreateChildProcess(L"D:\\Laboratory\\Операционные Системы\\x64\\Debug\\lab3_2.exe");
    //Sleep(333);

    WaitForSingleObject(pi1.hProcess, INFINITE);
    WaitForSingleObject(pi2.hProcess, INFINITE);

    for(int i=0;i<=100;i++)
    {
        cout << "PID: " << GetCurrentProcessId() << endl;
        Sleep(1000);
    }

    return 0;
}