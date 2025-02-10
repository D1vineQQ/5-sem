#include <iostream>
#include <windows.h>

using namespace std;


int main() {

    setlocale(LC_ALL, "ru");

    for (int i = 0; i <= 1000; i++)
    {
        cout << "PID: " << GetCurrentProcessId()
        << "\nTID: " << GetCurrentThreadId() << "\n-----------\n";
        Sleep(1000);
    }

    return 0;
}