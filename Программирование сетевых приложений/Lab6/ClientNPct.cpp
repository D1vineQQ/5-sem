#include <windows.h>
#include <iostream>
#include <string>
#include <thread>
#include <chrono>

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

string SetErrorMsgText(const string& msgText, int code) {
    return msgText + " " + GetErrorMsgText(code);
}

int main() {
    setlocale(LC_ALL, "Rus");
    int messageCount;
    cout << "Введите количество сообщений для отправки: ";
    cin >> messageCount;

    for (int i = 1; i <= messageCount; ++i) {
        string message = "Hello from client " + to_string(i);
        char response[128];
        DWORD bytesRead;

        if (!CallNamedPipe(
            L"\\\\N1ce\\pipe\\Tube",
            (LPVOID)message.c_str(),
            message.length(),
            response,
            sizeof(response) - 1,
            &bytesRead,
            NMPWAIT_WAIT_FOREVER)) {
            cerr << SetErrorMsgText("Ошибка call named pipe.", GetLastError()) << endl;
            continue;
        }

        response[bytesRead] = '\0';
        cout << "Отправлено: " << message << "\nПолучено от сервера: " << response << endl;

        this_thread::sleep_for(chrono::milliseconds(100));
    }

    string endMessage = "END";
    char endResponse[128];
    DWORD endBytesRead;

    if (!CallNamedPipe(
        L"\\\\N1ce\\pipe\\Tube",
        (LPVOID)endMessage.c_str(),
        endMessage.length(),
        endResponse,
        sizeof(endResponse) - 1,
        &endBytesRead,
        NMPWAIT_WAIT_FOREVER)) {
        cerr << SetErrorMsgText("Не удалось отправить конечное сообщение в канал.", GetLastError()) << endl;
    }

    cout << "Соединение прошло успешно." << endl;
    return 0;
}