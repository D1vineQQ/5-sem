#include <iostream>
#include <windows.h>
#include <bitset>

int main() {
    // �������� ������������� �������� ��������
    DWORD processId = GetCurrentProcessId();
    std::cout << "PID: \t\t\t\t" << processId << std::endl;

    // �������� ������������� �������� ������
    DWORD threadId = GetCurrentThreadId();
    std::cout << "MAIN TID: \t\t\t" << threadId << std::endl;

    // �������� ���������� �������� ��������
    HANDLE hProcess = GetCurrentProcess();

    // �������� ������������ ����� �������� ��������
    DWORD priorityClass = GetPriorityClass(hProcess);
    std::cout << "Current Thread Priority Class: \t" << priorityClass << std::endl;

    // �������� ��������� �������� ������
    HANDLE hThread = GetCurrentThread();
    int threadPriority = GetThreadPriority(hThread);
    std::cout << "MAIN TID Priority: \t\t" << threadPriority << std::endl;

    // �������� ����� ��������� �����������
    DWORD_PTR processAffinityMask, systemAffinityMask;
    GetProcessAffinityMask(hProcess, &processAffinityMask, &systemAffinityMask);
    std::cout << "Affinity Mask: \t\t\t"
        << std::bitset<sizeof(DWORD_PTR) * 8>(processAffinityMask) << std::endl;

    // �������� ���������� ��������� �����������
    SYSTEM_INFO sysInfo;
    GetSystemInfo(&sysInfo);
    std::cout << "Number Of Processors: \t\t" << sysInfo.dwNumberOfProcessors << std::endl;

    // �������� ���������, ����������� �������� ������
    DWORD_PTR threadAffinityMask = SetThreadAffinityMask(hThread, processAffinityMask);
    std::cout << "Processor of current thread: \t"
        << std::bitset<sizeof(DWORD_PTR) * 8>(threadAffinityMask) << std::endl;

    // ��������������� ����� ����������� ��� ������
    SetThreadAffinityMask(hThread, threadAffinityMask);

    // ��������� �����������
    CloseHandle(hProcess);
    CloseHandle(hThread);

    return 0;
}