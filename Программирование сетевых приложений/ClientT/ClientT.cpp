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

int main() {
    setlocale(0, "rus");

    WSADATA ws;
    try {
        if (WSAStartup(MAKEWORD(2, 2), &ws) != 0) {
            throw SetErrorMsgText("WSAStartup error:", WSAGetLastError());
        }
        cout << "WSAStartup succeeded. Winsock initialized." << endl;

        SOCKET s;
        if (INVALID_SOCKET == (s = socket(AF_INET, SOCK_STREAM, 0)))
        {
            throw SetErrorMsgText("ќшибка при создании сокета:", WSAGetLastError());
        }
        SOCKADDR_IN serv;
        serv.sin_addr.s_addr = inet_addr("127.0.0.1"); //127.0.0.1
        serv.sin_port = htons(2000);
        serv.sin_family = AF_INET;
        if (connect(s, (sockaddr*)&serv, sizeof(serv)) != 0)
            throw SetErrorMsgText("connect: ", WSAGetLastError());

        cout << "¬ведите число сообщений: ";
        int msgCount = 0;
        cin >> msgCount;

        int lobuf = 0;
        char buf[50];
        string msg;

        for (int i = 1; i <= msgCount; i++) {
            int res = snprintf(buf, sizeof(buf), "aboba %03d", i);
            if (res >= 0 && res < sizeof(buf))
                msg = buf;
            if ((lobuf = send(s, msg.c_str(), msg.length() + 1, 0)) == SOCKET_ERROR)
                throw SetErrorMsgText("send: ", WSAGetLastError());
            Sleep(1);
            if (recv(s, buf, sizeof(buf), 0) > 0) {
                cout << buf << endl;
            }
        }

        if (closesocket(s) != 0)
            throw SetErrorMsgText("Close socket: ", WSAGetLastError());
        if (WSACleanup() == SOCKET_ERROR)
            throw SetErrorMsgText("Cleanup: ", WSAGetLastError());
    }
    catch (string msgTxt) {
        cout << endl << msgTxt;
    }

    return 0;
}