#include <iostream>
#include <windows.h>
#include <thread>

using namespace std;

DWORD pid = GetCurrentProcessId();

void OS04_03_T1() {
    DWORD tid = GetCurrentThreadId();
    for (int i = 0; i <= 50; i++)
    {
        cout << "PID: " << pid << ", Child 1 Thread: " << tid << " I: " << i << "\n";
        Sleep(1000);
    }
}

void OS04_03_T2() {
    DWORD tid = GetCurrentThreadId();
    for (int i = 0; i <= 125; i++)
    {
        cout << "PID: " << pid << ", Child 2 Thread: " << tid << " I: " << i << "\n";
        Sleep(1000);
    }
}

int main() {

    setlocale(LC_ALL, "ru");

    DWORD
        tid = GetCurrentThreadId(),
        Child_1_ID = NULL,
        Child_2_ID = NULL;


    HANDLE
        Child_1_Thread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)OS04_03_T1, NULL, 0, &Child_1_ID),
        Child_2_Thread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)OS04_03_T2, NULL, 0, &Child_2_ID);

    
    for (int i = 0; i <= 100; i++)
    {
        switch (i) {
        case 20:
            SuspendThread(Child_1_Thread);
            cout << "Suspend Child 1\n";
            break;
        case 40:
            SuspendThread(Child_2_Thread);
            cout << "Suspend Child 2\n";
            break;
        case 60:
            ResumeThread(Child_1_Thread);
            cout << "Resume Child 1\n";
            break;
        }

        cout << "PID: " << pid << ", TID: " << tid << " I: " << i << "\n";
        Sleep(1000);
    }
    ResumeThread(Child_2_Thread);
    cout << "Resume Child 2\n";

    WaitForSingleObject(Child_1_Thread, INFINITE);
    WaitForSingleObject(Child_2_Thread, INFINITE);

    CloseHandle(Child_1_Thread);
    CloseHandle(Child_2_Thread);

    return 0;
}