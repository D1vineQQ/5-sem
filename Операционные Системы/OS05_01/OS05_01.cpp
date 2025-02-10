#include <iostream>
#include <windows.h>
#include <bitset>
#include <thread>

using namespace std;



int main() {
    setlocale(LC_ALL, "ru");

    DWORD processId = GetCurrentProcessId();
    cout << "PID: \t\t\t\t" << processId << endl;

    DWORD threadId = GetCurrentThreadId();
    cout << "TID: \t\t\t\t" << threadId << endl;

    HANDLE hProcess = GetCurrentProcess();

    DWORD priorityClass = GetPriorityClass(hProcess);
    cout << "Process Priority Class: \t" << priorityClass << endl;

    HANDLE hThread = GetCurrentThread();
    int threadPriority = GetThreadPriority(hThread);
    cout << "Thread Priority: \t\t" << threadPriority << endl;

    DWORD_PTR processAffinityMask, systemAffinityMask;
    if (GetProcessAffinityMask(GetCurrentProcess(), &processAffinityMask, &systemAffinityMask)) {
        cout << "Affinity mask: \t\t\t"
            << bitset<sizeof(DWORD_PTR) * 8>(processAffinityMask) << endl;
    }
    else {
        cerr << "get mask error" << endl;
    }

    if (GetProcessAffinityMask(GetCurrentProcess(), &processAffinityMask, &systemAffinityMask)) {
        int count = 0;
        while (processAffinityMask) {
            count += processAffinityMask & 1; // Считаем установленные биты
            processAffinityMask >>= 1;
        }
        cout << "available processors for thread:" << count << endl;
    }
    else {
        cerr << "get mask error" << endl;
    }

    cout << "current processor of thread: \t" << GetCurrentProcessorNumber() << endl;

    // Закрываем дескрипторы
    CloseHandle(hProcess);
    CloseHandle(hThread);

    return 0;
}