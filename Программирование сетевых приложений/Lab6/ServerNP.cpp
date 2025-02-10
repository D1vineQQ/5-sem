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
    while (true) {
        HANDLE hPipe;
        LPCWSTR pipeName = L"\\\\.\\pipe\\Tube";

        hPipe = CreateNamedPipe(
            pipeName,
            PIPE_ACCESS_DUPLEX,
            PIPE_TYPE_MESSAGE | PIPE_READMODE_MESSAGE | PIPE_WAIT,
            PIPE_UNLIMITED_INSTANCES,
            512, 512,
            0, NULL);

        if (hPipe == INVALID_HANDLE_VALUE) {
            cout << SetErrorMsgText("CreateNamedPipe: ", GetLastError()) << endl;
            return 1;
        }

        cout << "Ожидание подключения клиента..." << endl;

        if (!ConnectNamedPipe(hPipe, NULL)) {
            cout << SetErrorMsgText("ConnectNamedPipe: ", GetLastError()) << endl;
            CloseHandle(hPipe);
            return 1;
        }

        cout << "Клиент подключен." << endl;

        char buffer[128];
        DWORD bytesRead, bytesWritten;

        while (true) {
            if (!ReadFile(hPipe, buffer, sizeof(buffer), &bytesRead, NULL)) {
                cout << SetErrorMsgText("ReadFile: ", GetLastError()) << endl;
                break;
            }

            buffer[bytesRead] = '\0';  // Завершение строки
            cout << "Получено сообщение: " << buffer << endl;

            if (bytesRead == 0) {
                cout << "Получено сообщение нулевой длины. Завершение соединения." << endl;
                break;
            }

            if (!WriteFile(hPipe, buffer, bytesRead, &bytesWritten, NULL)) {
                cout << SetErrorMsgText("WriteFile: ", GetLastError()) << endl;
                break;
            }
        }

        DisconnectNamedPipe(hPipe);
        CloseHandle(hPipe);
        cout << "Соединение закрыто. Ожидание нового подключения..." << endl;
    }

    return 0;
}
