#pragma once
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include "WinSock2.h"
#pragma comment(lib, "ws2_32.lib")


#include <iostream>
#include <string>
#include <conio.h>

using namespace std;

string GetErrorMsgText(int code) {
    string msgText;
    switch (code) {
    case WSAEINTR:              msgText = "WSAEINTR: Interrupted function call"; break;
    case WSAEACCES:             msgText = "WSAEACCES: Permission denied"; break;
    case WSAEFAULT:             msgText = "WSAEFAULT: Bad address"; break;
    case WSAEINVAL:             msgText = "WSAEINVAL: Invalid argument"; break;
    case WSAEMFILE:             msgText = "WSAEMFILE: Too many open files"; break;
    case WSAEWOULDBLOCK:        msgText = "WSAEWOULDBLOCK: Resource temporarily unavailable"; break;
    case WSAEINPROGRESS:        msgText = "WSAEINPROGRESS: Operation now in progress"; break;
    case WSAEALREADY:           msgText = "WSAEALREADY: Operation already in progress"; break;
    case WSAENOTSOCK:           msgText = "WSAENOTSOCK: Socket operation on non-socket"; break;
    case WSAEDESTADDRREQ:       msgText = "WSAEDESTADDRREQ: Destination address required"; break;
    case WSAEMSGSIZE:           msgText = "WSAEMSGSIZE: Message too long"; break;
    case WSAEPROTOTYPE:         msgText = "WSAEPROTOTYPE: Protocol wrong type for socket"; break;
    case WSAENOPROTOOPT:        msgText = "WSAENOPROTOOPT: Bad protocol option"; break;
    case WSAEPROTONOSUPPORT:    msgText = "WSAEPROTONOSUPPORT: Protocol not supported"; break;
    case WSAESOCKTNOSUPPORT:    msgText = "WSAESOCKTNOSUPPORT: Socket type not supported"; break;
    case WSAEOPNOTSUPP:         msgText = "WSAEOPNOTSUPP: Operation not supported"; break;
    case WSAEPFNOSUPPORT:       msgText = "WSAEPFNOSUPPORT: Protocol family not supported"; break;
    case WSAEAFNOSUPPORT:       msgText = "WSAEAFNOSUPPORT: Address family not supported by protocol family"; break;
    case WSAEADDRINUSE:         msgText = "WSAEADDRINUSE: Address already in use"; break;
    case WSAEADDRNOTAVAIL:      msgText = "WSAEADDRNOTAVAIL: Cannot assign requested address"; break;
    case WSAENETDOWN:           msgText = "WSAENETDOWN: Network is down"; break;
    case WSAENETUNREACH:        msgText = "WSAENETUNREACH: Network is unreachable"; break;
    case WSAENETRESET:          msgText = "WSAENETRESET: Network dropped connection on reset"; break;
    case WSAECONNABORTED:       msgText = "WSAECONNABORTED: Software caused connection abort"; break;
    case WSAECONNRESET:         msgText = "WSAECONNRESET: Connection reset by peer"; break;
    case WSAENOBUFS:            msgText = "WSAENOBUFS: No buffer space available"; break;
    case WSAEISCONN:            msgText = "WSAEISCONN: Socket is already connected"; break;
    case WSAENOTCONN:           msgText = "WSAENOTCONN: Socket is not connected"; break;
    case WSAESHUTDOWN:          msgText = "WSAESHUTDOWN: Cannot send after socket shutdown"; break;
    case WSAETIMEDOUT:          msgText = "WSAETIMEDOUT: Connection timed out"; break;
    case WSAECONNREFUSED:       msgText = "WSAECONNREFUSED: Connection refused"; break;
    case WSAEHOSTDOWN:          msgText = "WSAEHOSTDOWN: Host is down"; break;
    case WSAEHOSTUNREACH:       msgText = "WSAEHOSTUNREACH: No route to host"; break;
    case WSASYSCALLFAILURE:     msgText = "WSASYSCALLFAILURE: System call failure"; break;
    default:                    msgText = "***ERROR***: Unknown error"; break;
    }
    return msgText;
}

string SetErrorMsgText(string msgText, int code) {
    return msgText + " " + GetErrorMsgText(code) + "\n";
}

SOCKET cS;
char buf[50];
int lb = 0;

bool getServer(char* call, short port, struct sockaddr* from, int* flen) {
    SOCKET sS;

    sS = socket(AF_INET, SOCK_DGRAM, NULL);
    if (sS == INVALID_SOCKET) {
        cout << "Socket creation failed: " << WSAGetLastError() << endl;
        return false; // Добавляем возврат false при ошибке создания сокета
    }

    SOCKADDR_IN broadcast_addr;
    broadcast_addr.sin_family = AF_INET;
    broadcast_addr.sin_port = htons(port);
    broadcast_addr.sin_addr.s_addr = INADDR_BROADCAST;

    int broadcast = 1;
    if (setsockopt(sS, SOL_SOCKET, SO_BROADCAST, (char*)&broadcast, sizeof(broadcast)) == SOCKET_ERROR) {
        cout << "Failed to set socket option: " << WSAGetLastError() << endl;
        closesocket(sS);
        return false;
    }

    int timeout = 20000;
    if (setsockopt(sS, SOL_SOCKET, SO_RCVTIMEO, (char*)&timeout, sizeof(timeout)) == SOCKET_ERROR) {
        cout << "Failed to set receive timeout: " << WSAGetLastError() << endl;
        closesocket(sS);
        return false;
    }

    int sent_len;
    char buffer[50];

    // Отправляем позывной
    if ((sent_len = sendto(sS, call, strlen(call), NULL, (sockaddr*)&broadcast_addr, sizeof(broadcast_addr))) == SOCKET_ERROR) {
        cout << "Ошибка sendto: " << WSAGetLastError() << endl;
        closesocket(sS);
        return false;
    }

    memset(buffer, 0, sizeof(buffer));

    // Ожидаем ответа от сервера
    int recv_len;
    if ((recv_len = recvfrom(sS, buffer, sizeof(buffer) - 1, NULL, from, flen)) == SOCKET_ERROR) {
        if (WSAGetLastError() == WSAETIMEDOUT) {
            cout << "Время ожидания ответа истекло." << endl;
            closesocket(sS);
            return false;
        }
        else {
            cout << "Ошибка recvfrom: " << WSAGetLastError() << endl;
            closesocket(sS);
            return false;
        }
    }

    buffer[recv_len] = '\0'; // Добавляем нуль-терминатор
    cout << "Полученный ответ: " << buffer << endl; // Для отладки

    // Проверяем, совпадает ли ответ с ожидаемым
    if (strcmp(buffer, "Hello") == 0) { // Ожидаемое сообщение
        closesocket(sS);
        return true; // Успешное совпадение
    }
    else {
        cout << "Ответ сервера не совпадает с ожидаемым: " << buffer << endl; // Выводим несоответствующий ответ
        closesocket(sS);
        return false;
    }
}

int main()
{
    setlocale(LC_ALL, "ru");
    WSADATA wsaData;

    try
    {
        if (WSAStartup(MAKEWORD(2, 0), &wsaData) != 0)
            throw  SetErrorMsgText("Startup:", WSAGetLastError());



        //cout << "WSAStartup succeeded. Winsock initialized." << endl;
        cout << "Client Console" << endl;

        char name[50] = "Hello";
        SOCKADDR_IN serv;
        int lserv = sizeof(serv);

        if (getServer(name, 2000, (sockaddr*)&serv, &lserv)) {
            cout << "Succes\n";
            cout << inet_ntoa(serv.sin_addr) << ":" << serv.sin_port <<"\n";
        }
        else {
            cout << "Failure\n";
        }

        if (WSACleanup() == SOCKET_ERROR)
            throw  SetErrorMsgText("Cleanup:", WSAGetLastError());
    }
    catch (string errorMsgText)
    {
        cout << endl << errorMsgText;
    }
    system("pause");
    return 0;
}