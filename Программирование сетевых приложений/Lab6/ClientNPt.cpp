#include <windows.h>
#include <iostream>
#include <string>
#include <ctime>

using namespace std;

string GetErrorMsgText(int code) {
    string msgText;
    switch (code) {
    case ERROR_FILE_NOT_FOUND:      msgText = "ERROR_FILE_NOT_FOUND: Канал не найден";
        break;
    case ERROR_PIPE_BUSY:           msgText = "ERROR_PIPE_BUSY: Канал занят";
        break;
    case ERROR_ACCESS_DENIED:       msgText = "ERROR_ACCESS_DENIED: Доступ запрещен";
        break;
    case ERROR_BROKEN_PIPE:         msgText = "ERROR_BROKEN_PIPE: Соединение разорвано";
        break;
    default:                        msgText = "Неизвестная ошибка";
        break;
    }
    return msgText;
}

string SetErrorMsgText(string msgText, int code) {
    return msgText + GetErrorMsgText(code);
}

int main() {
    setlocale(LC_ALL, "rus");

    // Имя канала
    string pipeName = "\\\\N1ce\\pipe\\Tube"; 
    LPCWSTR pipePath = std::wstring(pipeName.begin(), pipeName.end()).c_str();

    // Подключение к каналу
    HANDLE hPipe = CreateFile(
        L"\\\\N1ce\\pipe\\Tube",  
        GENERIC_READ | GENERIC_WRITE,
        0,
        NULL,
        OPEN_EXISTING,
        0,
        NULL);

    if (hPipe == INVALID_HANDLE_VALUE) {
        cout << SetErrorMsgText("CreateFile: ", GetLastError()) << endl;
        return 1;
    }

    int numMessages;
    cout << "Введите количество сообщений для отправки: ";
    cin >> numMessages;

    clock_t start = clock();

    for (int i = 0; i < numMessages; i++) {
        string message = "Hello from Client " + to_string(i);
        DWORD bytesWritten, bytesRead;
        char buffer[128];

        // Используем TransactNamedPipe
        if (!TransactNamedPipe(hPipe,
            (LPVOID)message.c_str(),
            message.size(),
            buffer,
            sizeof(buffer),
            &bytesRead,
            NULL)) {
            cout << SetErrorMsgText("TransactNamedPipe: ", GetLastError()) << endl;
            /*CloseHandle(hPipe);*/
            //return 1;
            continue;
        }

        buffer[bytesRead] = '\0'; // Завершение строки
        cout << "Ответ от сервера: " << buffer << endl;
    }

    clock_t end = clock();
    double duration = double(end - start) / CLOCKS_PER_SEC;

    cout << "Время обмена " << numMessages << " сообщениями: " << duration << " секунд" << endl;

    CloseHandle(hPipe);
    cout << "Соединение закрыто." << endl;

    return 0;
}
///////////////////////////////////