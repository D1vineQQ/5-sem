#include <iostream>
#include <windows.h>
#include <iomanip>
#include <TlHelp32.h>

using namespace std;

int main() {

    setlocale(LC_ALL, "ru");

    Sleep(10000);

    HANDLE snap = CreateToolhelp32Snapshot(TH32CS_SNAPALL, 0);
    PROCESSENTRY32 peProcessEntry;
    peProcessEntry.dwSize = sizeof(PROCESSENTRY32);

    try
    {
        if (!Process32First(snap, &peProcessEntry)) throw L"Process32First";
        do
        {
            wcout
                << "Name = " << peProcessEntry.szExeFile << endl
                << "PID = " << peProcessEntry.th32ProcessID
                << ", Parent PID = " << peProcessEntry.th32ParentProcessID << endl
                << "------------------------------------------------------" << endl;
        } while (Process32Next(snap, &peProcessEntry));
    }
    catch (char* msg) { cout << "ERROOOOOR: " << msg << endl; }
    return 0;
}