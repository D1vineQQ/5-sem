#include <iostream>
#include <windows.h>

using namespace std;


int main() {

    setlocale(LC_ALL, "ru");

    for (int i = 0; i <= 50; i++)
    {
        cout << "PID: " << GetCurrentProcessId() << endl;
        Sleep(1000);
    }

    return 0;
}