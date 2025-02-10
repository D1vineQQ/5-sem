#include <iostream>
#include <chrono>
#include <thread>
#include <vector>
#include <string>
#include <cstdlib>

// Function to check if a number is prime
bool isPrime(int number) {
    if (number < 2) return false;
    for (int i = 2; i * i <= number; ++i) {
        if (number % i == 0) return false;
    }
    return true;
}

int main(int argc, char* argv[]) {
    if (argc < 2) {
        std::cerr << "Error: duration in seconds must be provided." << std::endl;
        return 1;
    }

    // Duration in seconds
    int duration = std::stoi(argv[1]);

    auto startTime = std::chrono::steady_clock::now();
    int number = 2; // Start checking numbers from 2
    int counter = 1; // Prime number counter

    while (true) {
        if (isPrime(number)) {
            std::cout << "Prime #" << counter << ": " << number << std::endl;
            ++counter;
        }

        ++number;

        // Check if the duration has elapsed
        auto currentTime = std::chrono::steady_clock::now();
        auto elapsedSeconds = std::chrono::duration_cast<std::chrono::seconds>(currentTime - startTime).count();
        if (elapsedSeconds >= duration) {
            std::cout << "Time is up. Process is exiting." << std::endl;
            break;
        }

        // Reduce CPU load
        std::this_thread::sleep_for(std::chrono::milliseconds(1));
    }

    return 0;
}
