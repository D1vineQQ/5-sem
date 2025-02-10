#pragma once
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include "WinSock2.h"
#pragma comment(lib, "ws2_32.lib")


#include <iostream>
#include <string>

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
    return msgText + " " + GetErrorMsgText(code);
}

void handleClient(SOCKET cS) {
    char inBuff[20 * 1024];
    int ib = 0;
    clock_t start = clock();
    while (true) {
        if ((ib = recv(cS, &inBuff[0], sizeof(inBuff), 0)) == SOCKET_ERROR)
            throw SetErrorMsgText("recv: ", WSAGetLastError());
        
        if (ib > 0) {
            cout << endl << "Ответ от клиента: " << inBuff << endl;
            send(cS, inBuff, strlen(inBuff) + 1, 0);
        }
        if (ib == 0) {
            double duration = (clock() - start) / (double)CLOCKS_PER_SEC;
            cout << "Клиент отключился. Время обработки: " << duration << "с." << endl;
            break;
        }
    }
    closesocket(cS);
}

int main() {
    setlocale(LC_ALL, "ru");

    WSAData ws;
    try {
        // Инициализация winsocks
        if (WSAStartup(MAKEWORD(2, 2), &ws) != 0) {
            throw SetErrorMsgText("WSAStartup error:", WSAGetLastError());
        }
        cout << "WSAStartup succeeded. Winsock initialized." << endl;

        // Создание сокета ориентированного на поток соединения
        SOCKET s;
        if (INVALID_SOCKET == (s = socket(AF_INET, SOCK_STREAM, 0)))
        {
            throw SetErrorMsgText("Ошибка при создании сокета:", WSAGetLastError());
        }
        SOCKADDR_IN serv;
        serv.sin_family = AF_INET;
        serv.sin_addr.s_addr = INADDR_ANY;
        serv.sin_port = htons(2000);
        // Связывание сокета с параметрами
        if (bind(s, (LPSOCKADDR)&serv, sizeof(serv)) != 0)
            throw SetErrorMsgText("Bind socket: ", WSAGetLastError());

        if (listen(s, SOMAXCONN) != 0)
            throw SetErrorMsgText("listen error: ", WSAGetLastError());

        // Создаем новый сокет для обмена данными с клиентом
        while (true) {
            SOCKET cS;
            SOCKADDR_IN clnt;
            memset(&clnt, 0, sizeof(clnt));
            int lclnt = sizeof(clnt);
            if ((cS = accept(s, (sockaddr*)&clnt, &lclnt)) == INVALID_SOCKET)
                throw  SetErrorMsgText("accept:", WSAGetLastError());

            string clientIP = inet_ntoa(clnt.sin_addr);
            unsigned short clientPort = ntohs(clnt.sin_port);
            cout << "Подключен клиент " << clientIP << ":" << clientPort;

            handleClient(cS);
        }

        // Закрытие сокета и очистка winsocks
        if (closesocket(s) != 0)
            throw SetErrorMsgText("Close socket: ", WSAGetLastError());
        if (WSACleanup() == SOCKET_ERROR)
            throw SetErrorMsgText("Cleanup: ", WSAGetLastError());
    }
    catch (string errMsg) {
        cout << endl << errMsg;
    }

    return 0;
}
