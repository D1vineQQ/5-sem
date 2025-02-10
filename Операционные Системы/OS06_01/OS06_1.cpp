#include <iostream>
#include <Windows.h>
#include <ctime>

using namespace std;
int mutex = 0;
DWORD WINAPI Thread();
int main() {

	HANDLE hChild = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)Thread, NULL, 0, NULL);
	HANDLE hChild2 = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)Thread, NULL, 0, NULL);

	ResumeThread(hChild);
	ResumeThread(hChild2);

	WaitForSingleObject(hChild, INFINITE);
	WaitForSingleObject(hChild2, INFINITE);

	CloseHandle(hChild);
	CloseHandle(hChild2);

	system("pause");
}

void start_critical_section(void)
{
	__asm {
	SpinLoop:
		lock bts mutex, 0;
		jc SpinLoop
	}
}

void exit_critical_section(void)
{
	__asm lock btr mutex, 0
}

DWORD WINAPI Thread()
{
	DWORD tid = GetCurrentThreadId();
	int firrtTime = clock();
	start_critical_section();
	for (int i = 1; i < 91; i++)
	{
		cout << "Thread id: " << tid << "  iteration:" << i << endl;
	}
	exit_critical_section();
	return 0;
}