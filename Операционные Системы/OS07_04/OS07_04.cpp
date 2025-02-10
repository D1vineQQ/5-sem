#include <windows.h>
#include <iostream>
#include <string>

// Function to launch a child process
void runChildProcess(const std::wstring& duration, const std::wstring& processName) {
    STARTUPINFO si = { sizeof(STARTUPINFO) };
    PROCESS_INFORMATION pi;
    std::wstring command = processName + L" " + duration;


    if (!CreateProcessW(
        NULL,
        &command[0],
        NULL,
        NULL,
        FALSE,
        0,
        NULL,
        NULL,
        &si,
        &pi)) {
        std::wcerr << L"Failed to start the child process. Error code: " << GetLastError() << std::endl;
        return;
    }

    std::wcout << L"Child process started: " << processName << L" with duration " << duration << L" seconds." << std::endl;

    WaitForSingleObject(pi.hProcess, INFINITE);

    CloseHandle(pi.hProcess);
    CloseHandle(pi.hThread);
}

int main() {
    std::wcout << L"Launching two child processes OS07_04_X..." << std::endl;

    // Launch the first child process for 60 seconds
    runChildProcess(L"60", L"C:\\Laboratory\\Операционные Системы\\Debug\\OS07_04x.exe");

    // Launch the second child process for 120 seconds
    runChildProcess(L"120", L"C:\\Laboratory\\Операционные Системы\\Debug\\OS07_04x.exe");

    std::wcout << L"Both child processes finished. Main process is exiting." << std::endl;

    return 0;
}
