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
    setlocale(LC_ALL, "ru");
    WSADATA wsaData;
    try
    {
        if (WSAStartup(MAKEWORD(2, 0), &wsaData) != 0)
            throw  SetErrorMsgText("Startup:", WSAGetLastError());

        //cout << "WSAStartup succeeded. Winsock initialized." << endl;
        cout << "Client Console" << endl;






        //cout << "Socket open succeeded." << endl;


        //char obuf[50] = "Hello from ClientU";   //буфер вывода
        //int  lobuf = 0;                    //количество отправленных  

        //if ((lobuf = sendto(cS, obuf, strlen(obuf) + 1, NULL, (sockaddr*)&serv, sizeof(serv))) == SOCKET_ERROR)
        //    throw  SetErrorMsgText("send:", WSAGetLastError());



           // обнулить память
        //int ls = sizeof(serv);
        //char ibuf[50];                  //буфер ввода 
        //int  lb = 0;                    //количество принятых байт
        //if ((lb = recvfrom(cS, ibuf, sizeof(ibuf), NULL, (sockaddr*)&serv, &ls)) == SOCKET_ERROR)
        //    throw  SetErrorMsgText("Recieve:", WSAGetLastError());
        ////cout << "Recieve open succeeded." << endl;
        //cout << inet_ntoa(serv.sin_addr) << ": " << ibuf << endl;

        //memset(&serv, 0, sizeof(serv));

        int start = 30;
        int end = 70;
        int x = 0;

        SOCKET cS;
        if ((cS = socket(AF_INET, SOCK_DGRAM, NULL)) == INVALID_SOCKET)
            throw SetErrorMsgText("Socket:", WSAGetLastError());

        SOCKADDR_IN serv;              // параметры  сокета сервера
        serv.sin_family = AF_INET;    // используется ip-адресация  
        serv.sin_port = htons(2000);   // порт 2000  26.99.151.150
        serv.sin_addr.s_addr = inet_addr("127.0.0.1"); // адрес сервера  

        

        int ls = sizeof(serv);
        int lobuf = 0;
        string msg;
        char buf[50];
        //for (int i = 1; i <= 1000; i++) {
            x = rand() % (end - start + 1) + start;
            int res = snprintf(buf, sizeof(buf), "Hello");
            if (res >= 0 && res < sizeof(buf))
                msg = buf;
            if ((lobuf = sendto(cS, msg.c_str(), /*msg.length() + 1*/ x, NULL, (sockaddr*)&serv, sizeof(serv))) == SOCKET_ERROR)
                throw SetErrorMsgText("send: ", WSAGetLastError());
            Sleep(1);
            //if(x<=50){
                if (recvfrom(cS, buf, sizeof(buf), NULL, (sockaddr*)&serv, &ls) > 0) {
                    cout << buf << endl;
                }
            //}
        //}
        if ((lobuf = sendto(cS, "", 1, NULL, (sockaddr*)&serv, sizeof(serv))) == SOCKET_ERROR)
            throw SetErrorMsgText("send: ", WSAGetLastError());
        /////




        if (closesocket(cS) == SOCKET_ERROR)
            throw  SetErrorMsgText("Closesocket:", WSAGetLastError());
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