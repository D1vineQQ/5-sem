﻿#include <Windows.h>
#include <iostream>

using namespace std;

int main()
{
    PROCESS_INFORMATION pi1, pi2;
    DWORD pId = GetCurrentProcessId();
    LPCWSTR an1 = L"C:\\Laboratory\\Операционные Системы\\Debug\\OS06_05A.exe";
    LPCWSTR an2 = L"C:\\Laboratory\\Операционные Системы\\Debug\\OS06_05B.exe";
    HANDLE he = CreateEvent(NULL, FALSE, FALSE, L"smwEvent");
    {
        STARTUPINFO si;
        ZeroMemory(&si, sizeof(STARTUPINFO));
        si.cb = sizeof(STARTUPINFO);

        if (CreateProcess(an1, NULL, NULL, NULL, FALSE, CREATE_NEW_CONSOLE, NULL, NULL, &si, &pi1))
        {
            cout << "--Process OS06_05A created\n";
        }
        else
        {
            cout << "--Process OS06_05A not created\n";
        }
    }
    {
        STARTUPINFO si;
        ZeroMemory(&si, sizeof(STARTUPINFO));
        si.cb = sizeof(STARTUPINFO);

        if (CreateProcess(an2, NULL, NULL, NULL, FALSE, CREATE_NEW_CONSOLE, NULL, NULL, &si, &pi2))
        {
            cout << "--Process OS06_05B created\n";
        }
        else
        {
            cout << "--Process OS06_05B not created\n";
        }

        for (int i = 1; i <= 90; i++)
        {
            if (i == 15)
            {
                SetEvent(he);
            }
            cout << "PID = " << pId << ", Main Thread: " << i << endl;
            Sleep(1000);

        }

        WaitForSingleObject(pi1.hProcess, INFINITE);
        WaitForSingleObject(pi2.hProcess, INFINITE);
        CloseHandle(he);
        CloseHandle(pi1.hProcess);
        CloseHandle(pi2.hProcess);

        return 0;
    }
}