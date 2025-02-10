#include <iostream>
#include <windows.h>
#include <bitset>

int main() {
    // Получаем идентификатор текущего процесса
    DWORD processId = GetCurrentProcessId();
    std::cout << "PID: \t\t\t\t" << processId << std::endl;

    // Получаем идентификатор текущего потока
    DWORD threadId = GetCurrentThreadId();
    std::cout << "MAIN TID: \t\t\t" << threadId << std::endl;

    // Получаем дескриптор текущего процесса
    HANDLE hProcess = GetCurrentProcess();

    // Получаем приоритетный класс текущего процесса
    DWORD priorityClass = GetPriorityClass(hProcess);
    std::cout << "Current Thread Priority Class: \t" << priorityClass << std::endl;

    // Получаем приоритет текущего потока
    HANDLE hThread = GetCurrentThread();
    int threadPriority = GetThreadPriority(hThread);
    std::cout << "MAIN TID Priority: \t\t" << threadPriority << std::endl;

    // Получаем маску доступных процессоров
    DWORD_PTR processAffinityMask, systemAffinityMask;
    GetProcessAffinityMask(hProcess, &processAffinityMask, &systemAffinityMask);
    std::cout << "Affinity Mask: \t\t\t"
        << std::bitset<sizeof(DWORD_PTR) * 8>(processAffinityMask) << std::endl;

    // Получаем количество доступных процессоров
    SYSTEM_INFO sysInfo;
    GetSystemInfo(&sysInfo);
    std::cout << "Number Of Processors: \t\t" << sysInfo.dwNumberOfProcessors << std::endl;

    // Получаем процессор, назначенный текущему потоку
    DWORD_PTR threadAffinityMask = SetThreadAffinityMask(hThread, processAffinityMask);
    std::cout << "Processor of current thread: \t"
        << std::bitset<sizeof(DWORD_PTR) * 8>(threadAffinityMask) << std::endl;

    // Восстанавливаем маску процессоров для потока
    SetThreadAffinityMask(hThread, threadAffinityMask);

    // Закрываем дескрипторы
    CloseHandle(hProcess);
    CloseHandle(hThread);

    return 0;
}